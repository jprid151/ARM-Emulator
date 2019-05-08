//Form1.cs
//currently used as a place for output. Will grow in later phases. Defines the form used for the app.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace armsim
{
    //Defines the Windows Form used by the app.
    public partial class Form1 : Form
    {
        //holds command line arguments used for Options processing.
        string[] args;
        //Computer used to control CPU and Memory
        Computer comp;
        //holds value of first instruction on dissasemblyView
        int firstinstructAddress;

        //constructor for Form1.
        public Form1(string[] a)
        {
            InitializeComponent();
            args = a;
            byte[] bytes = Encoding.ASCII.GetBytes("Enter your name:");
            string x = "";
            foreach (byte b in bytes)
            {
                x += b.ToString("X");
            }
            this.KeyPreview = true;


        }
        //event handler for the Show event. Kicks things off by calling Options and then reading the file. Basically the program's real Main method.
        private void opener(object sender, EventArgs e)
        {
            disassemblyView.Font = new Font("Courier New", 9);
            memoryView.Font = new Font("Courier New", 9);
            RegisterView.Font = new Font("Courier New", 9);
            stackView.Font = new Font("Courier New", 9);
            flagView.Font = new Font("Courier New", 9);
            RunBtn.Image = new Bitmap("resources\\run.png");
            Reset.Image = new Bitmap("resources\\reset.png");
            Stop.Image = new Bitmap("resources\\stop.png");
            BreakBtn.Image = new Bitmap("resources\\break.png");
            Step.Image = new Bitmap("resources\\step.png");
            
           
            Options opts = new Options(args);
            opts.ParseOptions();
            if (opts.log) { Loader.logg = opts.log; Trace.Listeners.Add(new TextWriterTraceListener("mylogfile.txt", "myListener")); };
            Loader.Log("Form1: Opening form", false, false);
            
            if (opts.test)
            {
                MessageBox.Show("Starting test run. Please see testlog.txt for results");

                TestRam.RunTests();

                MessageBox.Show("Tests Complete.");
            }
            comp = new Computer(opts.mem);
            comp.setTrace();
            comp.exectrace = opts.traceall;
            int index = 0;
            while (index < 16)
            {
                string regstr = "r";
                int rindex = RegisterView.Rows.Add();
                if (rindex == 15)
                {
                    regstr = "PC";
                    RegisterView.Rows[rindex].Cells["Registers"].Value = regstr;
                }
                else
                {
                    RegisterView.Rows[rindex].Cells["Registers"].Value = regstr + rindex;
                }
                RegisterView.Rows[rindex].Cells["Value"].Value = 0;
                index++;
               
            }
            if (opts.filename != "" && opts.filename != null) { OnFileSelect(e, opts.filename); }
            comp.CompRunFinished += EndRun;
            comp.ConsoleChar += UpdateConsole;
        }

        //open a file dialog to allow the user to pick a file
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openDialog.Filter = "Executable Files|*.exe|All Files|*.*";
            openDialog.Title = "Select a file.";
            openDialog.FileName = "";
            

            //openDialog.ShowDialog();
            try
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openDialog.FileName;
                    OnFileSelect(e, filename);

                }
            }
            catch
            {
               
            }

        }
        //load the file specified by <name> and ready the Disassembly etc. panels
        protected void OnFileSelect(EventArgs e, string name)
        {
            Loader.Log("Form1: Loading File", false, false);
            terminalBox.Clear();
            int fl = flagView.Rows.Add();
            var flrow = flagView.Rows[fl];
            flagView.ForeColor = Color.Black;
            flrow.Cells[0].Value = "0";
            flrow.Cells[1].Value = "0";
            flrow.Cells[2].Value = "0";
            flrow.Cells[3].Value = "0";
            flrow.Cells[4].Value = "0";
            comp.setFileName(name);
           
            disassemblyView.Rows.Clear();
            
            stackView.Rows.Clear();
            memoryView.Rows.Clear();
            name = name.Substring(name.LastIndexOf("\\") + 1);
            try
            {
                int sum = comp.resetComputer();
                comp.cpu.clearBreak();
                fileNamePanel.Text = "Filename: "+name;
                checkSumPanel.Text = "Checksum: "+sum.ToString();
                int index = 0;
                byte[] memory = comp.getRam();
                while (index < 16)
                {
                    uint entry1 = comp.getReg(index);
                    if (index == 15)
                    {
                        entry1 -= 4;
                    }
                    setRegister(index, entry1);
                    index++;
                }
                uint addr = comp.RAM.begin;

                int byteindex = 0;
                bool instruct = false;
                StreamReader rd = new StreamReader("sample.txt");
                bool endins = false;
                Loader.Log("Form1: Setting Memory", false, false);
                while (addr < comp.RAM.end)
                {

                    int row = memoryView.Rows.Add();
                    DataGridViewRow r1 = memoryView.Rows[row];
                    r1.Height = (memoryView.ClientRectangle.Height - memoryView.ColumnHeadersHeight) / memoryView.Rows.Count;
                    r1.Cells["Address"].Value = "0x" + addr.ToString("X8");
                    DataGridViewCell c1 = memoryView.Rows[row].Cells["Address"];
                    int readindex = 0;
                    while (readindex < 4)
                    {
                        string bytestr = "";
                        while (byteindex < 4)
                        {
                            if (addr >= comp.RAM.end)
                            {
                                break;
                            }
                            byte b = memory[addr + byteindex];
                            bytestr += b.ToString("X2") + " ";
                            byteindex++;
                        }
                        memoryView.Rows[row].Cells[readindex + 1].Value = bytestr;
                        byteindex = 0;
                        addr += 4;
                        readindex++;
                    }
                    //memoryView.Rows.Insert()
                }
                DataGridViewRow lrow = memoryView.Rows[memoryView.Rows.Count - 1];
                int cindex = 1;
                while (cindex < 5)
                {
                    if ((string)lrow.Cells[cindex].Value == "")
                    {
                        lrow.Cells[cindex].Value = "00 00 00 00";
                    }
                    cindex++;
                }
                addr = 0;
                comp.setCounter(comp.Registers.getPC());
                Loader.Log("Form1: Loading Disassembly", false, false);
               
                addr = 0;
                var re = comp.Registers.getPC();
                if (comp.RAM.ReadWord(0) == 0)
                {
                    addr = comp.RAM.begin;
                    instruct = true;
                }
                while (addr < comp.RAM.end)
                {
                    string x = addr.ToString("X");
                    DataGridViewRow r2;
                    int row2;
                    comp.Registers.setRegister(15, addr + 4);
                    if (addr == 0 || instruct)
                    {
                        if (comp.RAM.ReadWord(addr) != 0)
                        {
                            if (!endins)
                            {
                                row2 = disassemblyView.Rows.Add();
                                r2 = disassemblyView.Rows[row2];

                                r2.Cells["AddressColumn"].Value = "0x" + x;
                                r2.Cells["MachineLangColumn"].Value = "0x" + addPlace(comp.RAM.ReadWord(addr));

                                try
                                {
                                    Instruction x1 = Instruction.Factory(comp.RAM.ReadWord(addr), ref comp);
                                    if (x1.GetType() == typeof(BranchInstruction))
                                    {
                                        BranchInstruction bran = (BranchInstruction)x1;

                                        r2.Cells["AssemblyColumn"].Value=(bran.ToAssembly());
                                    }
                                    else
                                    {
                                        r2.Cells["AssemblyColumn"].Value = x1.ToAssembly();

                                    }
                                }
                                catch
                                {
                                    r2.Cells["AssemblyColumn"].Value = "";
                                }
                                instruct = true;
                            }
                        }

                    }
                    addr += 4;

                }
                comp.Registers.setRegister(15, re);
                uint entry = comp.Registers.getPC();
                foreach (DataGridViewRow y in disassemblyView.Rows)
                {
                    if (y.Cells[0].Value == null)
                    {
                        continue;
                    }
                    string iii = (string)y.Cells[0].Value;
                    var nupoint = UInt32.Parse(iii.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    if (nupoint == re)
                    {

                        if (y.Index > 2)
                            disassemblyView.FirstDisplayedScrollingRowIndex = y.Index - 2;
                        else
                            disassemblyView.FirstDisplayedScrollingRowIndex = y.Index;
                        y.Selected = true;
                       
                        firstinstructAddress = y.Index;
                        disassemblyView.Update();
                        break;
                    }
                    
                    disassemblyView.Rows[0].Selected = true;
                    disassemblyView.Enabled = false;
                    rd.Close();
                }
                addStack();
                if (comp.Registers.modetrack == 0)
                {
                    modeBox.Text = "SYS";
                }
                else
                {
                    modeBox.Text = "SVC";
                }
            }
            catch
            {
                fileNamePanel.Text = "None";
                checkSumPanel.Text = "Load Error";
                disassemblyView.Rows.Clear();
                memoryView.Rows.Clear();
                flagView.Rows.Clear();
                stackView.Rows.Clear();
                MessageBox.Show("Error: File could not load.");
            }

            disassemblyView.AutoResizeColumns();
            memoryView.AutoResizeColumns();
            flagView.AutoResizeColumns();
            disassemblyView.CellBorderStyle= DataGridViewCellBorderStyle.None;
            memoryView.CellBorderStyle= DataGridViewCellBorderStyle.None; 
        }
        //set the prog mode indicator box.
        void SetProgMode()
        {
            if (comp.Registers.modetrack==1) { modeBox.Text = "SVC"; }
            if (comp.Registers.modetrack == 0) { modeBox.Text = "SYS"; }
            if (comp.Registers.modetrack == 2) { modeBox.Text = "IRQ"; }
        }
        //load the stack panel.
        void addStack()
        {
            stackView.Rows.Clear();
            Loader.Log("Loading Stack", false, false);
            int index = stackView.Rows.Add(new DataGridViewRow());
            var x = stackView.Rows[index];
            uint stackaddr = comp.getReg(13)+16;
            x.Cells[0].Value = (comp.getReg(13)+20).ToString("X8");
            x.Cells[1].Value = comp.RAM.ReadWord(comp.getReg(13)+20).ToString("X8");
            int stackindex = 0;
           
            while (stackindex > -64)
            {
                stackindex -= 4;
                index = stackView.Rows.Add(new DataGridViewRow());
                x = stackView.Rows[index];
                x.Cells[0].Value = stackaddr.ToString("X8");
                x.Cells[1].Value = comp.RAM.ReadWord(stackaddr).ToString("X8");

                stackaddr -= 4;
            }
            //stackView.Refresh();
        }
        //convert <value> to a hexidecimal string and make into an 8 digit string if its not already
        String addPlace(uint value)
        {
            string result = value.ToString("X4");
            int len = result.Length;
            while (len < 8)
            {
                result = result.Insert(0, "0");
                len++;
            }
            return result;
        }
        //reset the memory display on a reload.s
        private void resetMemory(uint dsp)
        {
            memoryView.Rows.Clear();
            uint addr = comp.Registers.getPC() - 28;
            if (dsp > 0&&dsp!=uint.MaxValue)
            {
                addr = dsp - 28;
            }
            if(dsp == 0&&dsp != uint.MaxValue)
            {
                addr = 0;
            }
            uint addr2 = addr+104;
            byte[] memory = comp.getRam();
            uint byteindex = 0;
            bool instruct = false;
            StreamReader rd = new StreamReader("sample.txt");
            bool endins = false;
            Loader.Log("Form1: Setting Memory", false, false);
            while (addr < addr2)
            {

                int row = memoryView.Rows.Add();
                DataGridViewRow r1 = memoryView.Rows[row];
                r1.Height = (memoryView.ClientRectangle.Height - memoryView.ColumnHeadersHeight) / memoryView.Rows.Count;
                r1.Cells["Address"].Value = "0x" + addr.ToString("X8");
                DataGridViewCell c1 = memoryView.Rows[row].Cells["Address"];
                int readindex = 0;
                while (readindex < 4)
                {
                    string bytestr = "";
                    while (byteindex < 4)
                    {
                       
                        byte b = comp.RAM.ReadByte(addr+byteindex);
                        bytestr += b.ToString("X2") + " ";
                        byteindex++;
                    }
                    memoryView.Rows[row].Cells[readindex + 1].Value = bytestr;
                    byteindex = 0;
                    addr += 4;
                    readindex++;
                }
                //memoryView.Rows.Insert()
            }
            /*DataGridViewRow lrow = memoryView.Rows.
            int cindex = 1;
            while (cindex < 5)
            {
                if ((string)lrow.Cells[cindex].Value == "")
                {
                    lrow.Cells[cindex].Value = "00 00 00 00";
                }
                cindex++;
            }*/
        }
        //handles keyboard shortcuts when prompted by user.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Loader.Log("Form1: Keyboard Shortcut detected", false, false);
            if (keyData == (Keys.Control | Keys.B))
            {
                toggleBreakpointsToolStripMenuItem_Click(this, null);
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                loadToolStripMenuItem_Click(this, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                Stop_Click(this, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.T))
            {
                comp.setTrace();
                if(TracePanel.Text == "Trace Mode: On")
                {
                    TracePanel.Text = "Trace Mode: Off";
                }
                else
                {
                    TracePanel.Text = "Trace Mode: On";
                }

            }
            if (keyData == (Keys.Control | Keys.R))
            {
                Reset_Click(this, null);
                return true;
            }
            if (keyData == (Keys.F10))
            {
                Step_Click(this, null);
                return true;
            }
            if (keyData == (Keys.F5))
            {
                RunBtn_Click(this, null);
                return true;
            }
            return false;
        }
        public void UpdateConsole(SendToConsoleEventArgs e)
        {
            terminalBox.AppendText(e.c.ToString());
        }
        //set a register a <index> with <value> in the RegisterView
        public void setRegister(int index, uint value1)
        {
            string value = value1.ToString("X");

            int len = value.Length;
            
                /*while (len < 8)
                {
                    value = value.Insert(len, "0");
                    len++;
                }*/
            value = "0x" + value;
            switch (index)
            {
                case 0:
                    RegisterView.Rows[0].Cells["Value"].Value = value;
                    return;
                case 1:
                    RegisterView.Rows[1].Cells["Value"].Value = value;
                    return;
                case 2:
                    RegisterView.Rows[2].Cells["Value"].Value = value;
                    return;
                case 3:
                    RegisterView.Rows[3].Cells["Value"].Value = value;
                    return;
                case 4:
                    RegisterView.Rows[4].Cells["Value"].Value = value;
                    return;
                case 5:
                    RegisterView.Rows[5].Cells["Value"].Value = value;
                    return;
                case 6:
                    RegisterView.Rows[6].Cells["Value"].Value = value;
                    return;
                case 7:
                    RegisterView.Rows[7].Cells["Value"].Value = value;
                    return;
                case 8:
                    RegisterView.Rows[8].Cells["Value"].Value = value;
                    return;
                case 9:
                    RegisterView.Rows[9].Cells["Value"].Value = value;
                    return;
                case 10:
                    RegisterView.Rows[10].Cells["Value"].Value = value;
                    return;
                case 11:
                    RegisterView.Rows[11].Cells["Value"].Value = value;
                    return; ;
                case 12:
                    RegisterView.Rows[12].Cells["Value"].Value = value;
                    return;
                case 13:
                    RegisterView.Rows[13].Cells["Value"].Value = value;
                    return;
                case 14:
                    RegisterView.Rows[14].Cells["Value"].Value = value;
                    return;
                case 15:
                    RegisterView.Rows[15].Cells["Value"].Value = value;
                    return;
                default:
                    return;
            }
        }
        //execute one Fetch-Decode-Execute cycle on the computer
        private void Step_Click(object sender, EventArgs e)
        {

            if (fileNamePanel.Text != "")
            {

                uint test = UInt32.Parse(((string)(disassemblyView.Rows[disassemblyView.Rows.Count - 1].Cells[0].Value)).Substring(2), System.Globalization.NumberStyles.HexNumber);
                if (comp.Registers.getPC() > test)
                {
                   // MessageBox.Show("Illegal step operation. Please hit reset and try again.");
                   //return;
                }
                ThreadStart nuThreadstart = new ThreadStart(CompStep);
                opModePanel.Text = "Operating Mode: Running";
                Thread nuThread = new Thread(nuThreadstart);
                nuThread.Start();

            }
            else
            {
                MessageBox.Show("You have not chosen a file. Please load one into memory.");
                return;
            }
        }
        //set up event for computer
        private delegate void RunBtnClickDelegate();
        //set up event for when computer finishes running
        private delegate void EndRunDelegate(CompRunFinishedEventArgs e);
        private delegate void ConsoleUpdateDelegate(SendToConsoleEventArgs e);
        //begin a run event for the Computer
        private void RunBtn_Click(object sender, EventArgs e)
        {

            if (fileNamePanel.Text != "")
            {
                uint test = UInt32.Parse(((string)(disassemblyView.Rows[disassemblyView.Rows.Count - 1].Cells[0].Value)).Substring(2), System.Globalization.NumberStyles.HexNumber);
                if (comp.Registers.getPC() >test )
                {
                   // MessageBox.Show("Illegal run operation. Please hit reset and try again.");
                   // return;
                }
                // comp.onStopClick(e);
                RunBtn.Enabled = false;
                ThreadStart nuThreadstart = new ThreadStart(CompRun);
                opModePanel.Text = "Operating Mode: Running";
                Thread nuThread = new Thread(nuThreadstart);
                nuThread.Start();


            }
            else
            {
                MessageBox.Show("You have not chosen a file. Please load one into memory.");
                return;
            }

        }
        //used for multithreading
        private void CompRun()
        {
            try
            {
                comp.Run();
            }
            catch
            {
                MessageBox.Show("Run failed!");
           //     RunBtn.Enabled = true;
            }

        }
        //used for multithreading
        private void CompStep()
        {
            try
            {
                comp.Step();
            }
            catch
            {
                MessageBox.Show("Step failed!");
            }

        }
        //returns control of the main thread to the form.
        private void EndRun(object sender, CompRunFinishedEventArgs e)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EndRunDelegate(EndRun), e);
                return;
            }

        }//display a char form the Computer class.
        private void UpdateConsole(object sender, SendToConsoleEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ConsoleUpdateDelegate(UpdateConsole), e);
                return;
            }

        }
        //update the panels on the main window. <e> contains a status code that will help determine which message to show the user, based on how execution endec
        //i.e. clicking on stop, hitting breakpoints etc.
        private void EndRun(CompRunFinishedEventArgs e)
        {
            if (e.end == 4)
            {
                RunBtn.Enabled = true;
                MessageBox.Show("Error: Illegal step/run performed. The program has already executed. Please hit reset and try again.");
                return;
            }
            Loader.Log("Form1: Step/Execute cycle finished.", false, false);
            uint entry = comp.Registers.getPC();
            setRegister(15, entry);
            SetProgMode();
            int x1 = 0;
            int fi = 1;
            while (fi < 5)
            {
                uint flag = comp.Registers.cpsr;
                bool fl = Memory.TestBit(32 - fi, flag);
                if (fl == true)
                {
                    flagView.Rows[0].Cells[fi - 1].Value = "1";
                }
                else
                {
                    flagView.Rows[0].Cells[fi - 1].Value = "0";
                }

                fi++;
            }
            if (Memory.TestBit(7, comp.Registers.cpsr)) flagView.Rows[0].Cells[4].Value = "1";
            else flagView.Rows[0].Cells[4].Value = "0";
            while (x1 < 15)
            {
                uint val = comp.getReg(x1);
                setRegister(x1, val);
                x1++;
            }
            var x = disassemblyView.Rows;
            disassemblyView.ClearSelection();
            bool selected = false;
            foreach (DataGridViewRow y in x)
            {
                if (y.Cells[0].Value == null)
                {
                    continue;
                }
                string iii = (string)y.Cells[0].Value;
                var nupoint = UInt32.Parse(iii.Substring(2), System.Globalization.NumberStyles.HexNumber);
                if (nupoint == entry)
                {

                    int rindex = y.Index;
                    if (rindex > 2)
                    {
                        
                        selected = true;
                        if(rindex!=disassemblyView.Rows[disassemblyView.Rows.Count-1].Index)
                        disassemblyView.FirstDisplayedScrollingRowIndex = rindex - 2;
                        else
                        {
                            disassemblyView.FirstDisplayedScrollingRowIndex = disassemblyView.Rows.Count - 4;
                        }
                    }
                    else
                    {
                        selected = true;
                        
                    }
                    y.Selected = true;
                    disassemblyView.Update();
                    break;
                }

            }
            if (!selected)
            {
                disassemblyView.FirstDisplayedScrollingRowIndex = disassemblyView.Rows.Count - 4;
                disassemblyView.Rows[disassemblyView.Rows.Count - 1].Selected = true;
            }
            if (e.end == 0)
            {
                MessageBox.Show("Execution complete");
                opModePanel.Text = "Operating Mode: None";

                comp.resetCounter();
            }
            else if (e.end == 1) { MessageBox.Show("Breakpoint Reached");
                opModePanel.Text = "Operating Mode: Breakpoint";
            }
            else if (e.end == 2) { MessageBox.Show("Execution stopped by user");
                opModePanel.Text = "Operating Mode: User Stop";
            }
            else if (e.end == 3) { opModePanel.Text = "Operating Mode: None"; }
            else if (e.end == 4) { MessageBox.Show("Error: Illegal step/run performed. The program has already executed. Please hit reset and try again."); }
            else if (e.end == 5) { MessageBox.Show("Internal simulator error. Run aborted."); }
            addStack();
            resetMemory(entry);
            RunBtn.Enabled = true;
        }
        //same as above endrun, but handles the end of a step event instead
        private void EndStep()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new RunBtnClickDelegate(CompStep));
                return;
            }
            uint entry = comp.Registers.getPC();
            setRegister(15, entry);

        }

        //handles clicking on the reset button
        private void Reset_Click(object sender, EventArgs e)
        {
            if (fileNamePanel.Text == "")
            {
                MessageBox.Show("You have not chosen a file. Please load one into memory.");
                return;

            }
            comp.togInteruppt();
            terminalBox.Clear();
           
            comp.cpu.resetBreak();
            comp.resetComputer();
            SetProgMode();
            int x = 0;
            while (x<15)
            {
                setRegister(x, comp.getReg(x));
                x++;
            }
            setRegister(15, comp.Registers.getPC());
            disassemblyView.ClearSelection();
            disassemblyView.Rows[firstinstructAddress].Selected = true;
            if (firstinstructAddress > 2)
                disassemblyView.FirstDisplayedScrollingRowIndex = firstinstructAddress - 2;
            else
                disassemblyView.FirstDisplayedScrollingRowIndex = firstinstructAddress;
        }
        //enables user to add a brekapoint to the program.
        private void BreakBtn_Click(object sender, EventArgs e)
        {
            if (fileNamePanel.Text == "")
            {
                MessageBox.Show("You did not select a file to load. Please load a file before adding breakpoints.");
                return;
            }
            using (Form BreakForm = new Form())
            {
                BreakForm.Text = "Add Breakpoint";
                Label l1 = new Label();
                l1.Font = label1.Font;
                l1.Text = "Please enter a breakpoint address in base 16 (hex):";
                l1.Width = 300;
                BreakForm.Width = 1000;
                BreakForm.Height = 300;

                Point nupoint = new Point();
                nupoint.X = 30;
                nupoint.Y = 50;
                l1.Location = nupoint;

                TextBox tx1 = new TextBox();
                nupoint.X = 400;
                nupoint.Y = 50;
                tx1.Location = nupoint;
                BreakForm.Controls.Add(l1);
                BreakForm.Controls.Add(tx1);

                Button b1 = new Button();
                nupoint.X = 450;
                nupoint.Y = 80;
                b1.Location = nupoint;
                b1.Enabled = true;
                b1.Text = "Add";
                b1.Click += new EventHandler(delegate (Object o, EventArgs a)
                {
                    string nupoint1 = tx1.Text;
                    uint breakpoint = UInt32.Parse(nupoint1, System.Globalization.NumberStyles.HexNumber);
                    if (breakpoint < 0 || breakpoint > comp.RAM.bsize)
                    {
                        MessageBox.Show("Invalid breakpoint. Falls outside of the range of memory.");
                        return;
                    }
                    comp.cpu.addBreak(breakpoint);
                    Loader.Log("Form1: Breakpoint at address " + breakpoint + " added.", false, false);
                    BreakForm.Close();
                });
                BreakForm.Controls.Add(b1);
                BreakForm.ShowDialog();
            }
        }
        //handles clicking on the stop button
        private void Stop_Click(object sender, EventArgs e)
        {
            if (fileNamePanel.Text == "")
            {
                MessageBox.Show("You have not chosen a file. Please load one into memory.");
                return;
            }
            comp.onStopClick(e);
        }
        //sets memory panel to display a user-chosen address
        private uint getMem()
        {
            uint result=0;
             Loader.Log("Form1: Memory display location changed.", false, false);
                    string nupoint1 = memBox.Text;
                    uint breakpoint = UInt32.Parse(nupoint1, System.Globalization.NumberStyles.HexNumber);
                    if (breakpoint < 0 || breakpoint > comp.RAM.bsize)
                    {
                        MessageBox.Show("Invalid memoryaddress. Falls outside of the range of memory.");
                        return 0;
                    }
            result = breakpoint;

            resetMemory(breakpoint);
                   
                    
            return result;
        }
        //toggles traceing on or off
        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Log("Form1:Tracing set to off.", false, false);
            if (TracePanel.Text == "Trace Mode: On")
            {
                return;
            }
            comp.setTrace();
            TracePanel.Text = "Trace Mode: On";
        }
        //toggles tracing
        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Log("Form1: Tracing set to off.", false, false);
            if (TracePanel.Text == "Trace Mode: Off")
            {
                return;
            }
            comp.setTrace();
            TracePanel.Text = "Trace Mode: Off";
        }

        //prevents an error if the user has not loaded a file and selects a memory location
        private void memBtn_Click(object sender, EventArgs e)
        {
            if (fileNamePanel.Text == "")
            {
                MessageBox.Show("You did not select a file to load. Please load a file before adding breakpoints.");
                return;
            }
            getMem();
        }
        //close application
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //allows user to toggle breakpoints
        private void toggleBreakpointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Log("Form1: Breakpoints toggled.", false, false);
            bool b = comp.cpu.toggleBreak();
            if (b)
            {
                BreakPanel.Text = "Breakpoints: On";
            }
            else
            {
                BreakPanel.Text = "Breakpoints: Off";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //handle input into consle. By setting e.handled to true, blocks char from echoing. The method then sends the char
        //to the Comp class.
        private void terminalBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                char x = e.KeyChar;
                e.Handled = true;
                comp.addChar(x);
            }
        }
    }
   
}
