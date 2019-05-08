using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace armsim
{
    class Computer
    {
        //models a CPU 
        public CPU cpu;
        //models a RAM array
        public Memory RAM;
        //models a set of registers
        public Memory Registers;
        //determines the size of RAM
        int size;
        //shows the start of instructions in RAM
        uint instart;
        //determines if user hits the stop button
        bool stop = false;
        //determines current trace step
        int TraceStep;
        //determines the current file
        string fileName;
        //writes to trace.log
        StreamWriter sw;
        //determines if trace is enabled
        bool trace;
        public bool endrun;
        //tells if computer is running or not
        bool tr;
        bool run;
        //tells computer to stop due to user loading new file or app quitting.
        bool interrupt;
        //constructor for the computer class
        public bool halt;
        //SVC/IRQ steps will not be printed if false.
        public bool exectrace;
        //forbids computer from running if execution has already ended
        public bool dontrun;
        public Computer(int size1)
        {
            
            size = size1;
            TraceStep = 1;
            interrupt = false;
            chars = '0';
            tr = false;
        }
        //tracks the latest char from the console.
        char chars;
        //handles a run cycle for the computer
        public void Run()
        {
            if (dontrun)
            {
                CompRunFinishedEventArgs arg1 = new CompRunFinishedEventArgs();
                arg1.end = 4;
                onFinishExecution(arg1);
                return;
            }
            run = true;
            interrupt = false;
            endrun = false;
            Loader.Log("Computer: Executing Run Cycle", false, false);
            uint val=9001;
            try
            {
                //if (!trace) { trace = true; sw = new StreamWriter("trace.log"); }
                while (val != 0 && stop == false && interrupt == false && halt == false)
                {
                    
                    val = cpu.fetch();
                    uint insval = Registers.getPC();
                    if (insval == RAM.begin2)
                    {
                        tr = true;
                    }
                    Registers.setRegister(15, Registers.getPC() + 4);
                    Computer x = this;
                    Instruction ins = cpu.decode(val, ref x);
                    if (val != uint.MaxValue)
                        cpu.execute(ins);
                    else
                    {
                        Registers.setRegister(15, Registers.getPC() - 4);
                        stop = true;
                    }
                    if (trace && val != uint.MaxValue && val != 0)
                    {
                        try
                        {
                            writeTrace(insval);
                        }
                        catch
                        {

                        }
                    }
                    TraceStep++;
                    if (cpu.irq == 1 && !Memory.TestBit(7, Registers.cpsr))
                    {//toggle the IRQ bit on and process input
                        Registers.swapRegisters(2);
                        cpu.irq = 0;
                        
                        Memory.SetBitinInt(Registers.cpsr, 7, true);
                    }
                }
                if (val != uint.MaxValue)
                    Registers.setRegister(15, Registers.getPC() - 4);
                CompRunFinishedEventArgs arg = new CompRunFinishedEventArgs();
                if (interrupt)
                {

                    arg.end = 4;


                }
                if (!interrupt)
                {
                    if (val == uint.MaxValue)
                    {
                        Loader.Log("Computer: Breakpoint hit", false, false);
                        stop = false;
                        arg.end = 1;
                    }
                    else
                    {
                        arg.end = 0;
                        endrun = true;
                        halt = false;
                        if (stop)
                        {
                            Loader.Log("Computer: Stopping at user's command.", false, false);
                            arg.end = 2;
                            stop = false;
                        }
                        if (arg.end == 0)
                            dontrun = true;
                    }
                }
                interrupt = false;
                run = false;
                onFinishExecution(arg);
            }
            catch
            {
                interrupt = false;
                run = false;
                stop = false;
                CompRunFinishedEventArgs arg = new CompRunFinishedEventArgs();
                arg.end = 5;
                onFinishExecution(arg);
            }
        }
        //handles a step cycle for the computer
        public void Step()
        {
            if (dontrun)
            {
                CompRunFinishedEventArgs arg1 = new CompRunFinishedEventArgs();
                arg1.end = 4;
                onFinishExecution(arg1);
                return;
            }
            run = true;
            //if (!trace) trace = true;
            Loader.Log("Computer: Executing Step Cycle", false, false);
            uint val = cpu.fetch();
            uint insval = Registers.getPC();
            Registers.setRegister(15, Registers.getPC() + 4);
            Computer x = this;
            Instruction ins = cpu.decode(val, ref x);
            cpu.execute(ins);
           
            CompRunFinishedEventArgs arg = new CompRunFinishedEventArgs();
            arg.end = 3;
            if (trace)
            {
                try
                {
                    writeTrace(insval);
                }
                catch
                {

                }
                
            }
            TraceStep++;
            onFinishExecution(arg);
            run = false;
        }
        //sets a file for the loader
        public void setFileName(string file)
        {
            fileName = file;
        }
        //send char to console.
        public void SendChar(char c)
        {
            SendToConsoleEventArgs e = new SendToConsoleEventArgs();
            e.c = c;
            onOutputDetected(e);
        }
        public void togInteruppt()
        {
            interrupt = true;
        }
        //resets the computer model when the computer resets
        public int resetComputer()
        {
            dontrun = false;
            Loader.Log("Computer: Resetting Computer", false, false);
            RAM = new Memory(size,null);
            Registers = new Memory(0, null);
            TraceStep = 1;
            
            if (File.Exists("trace.log")&&trace)
            {
                sw.Close();
                File.Delete("trace.log");
                sw = new StreamWriter("trace.log");
            }
           
            
            //load file

            Loader.Read(fileName, RAM, false, false,Registers);
            if (cpu != null)
            {
                var x = cpu.getBreak();
                cpu = new CPU(ref RAM, ref Registers);
                foreach (var y in x)
                {
                    cpu.addBreak(y.Key);
                }
            }
            else
            {
                cpu = new CPU(ref RAM, ref Registers);
            }
            //interrupt = false;
           // Registers.setRegister(13, 0x7000);
            return RAM.Checksum();
        }
        //reset the computer
        public void resetComputer(int i)
        {

            Loader.Log("Computer: Resetting Computer", false, false);
            RAM = new Memory(size, null);
            Registers = new Memory(0, null);
            TraceStep = 1;

            if (File.Exists("trace.log") && trace)
            {
                sw.Close();
                File.Delete("trace.log");
                sw = new StreamWriter("trace.log");
            }


    
            if (cpu != null)
            {
                var x = cpu.getBreak();
                cpu = new CPU(ref RAM, ref Registers);
                foreach (var y in x)
                {
                    cpu.addBreak(y.Key);
                }
            }
            else
            {
                cpu = new CPU(ref RAM, ref Registers);
            }
            //interrupt = false;
            Registers.setRegister(13, 0x7000);
          
        }
        //returns a value from the register specified by <regnum>
        public uint getReg(int regNum)
        {
            return Registers.getRegister(regNum);
        }
        //returns the portion of RAM where things are loaded
        public byte[] getRam()
        {
            uint addr = RAM.begin;
            uint end = RAM.end;
            byte[] bytes = new byte[end];
            uint x = addr;

            while (x < end)
            {
                bytes[x] = RAM.ReadByte(x);
                x++;
            }
            return bytes;
        }
        //handles a stop signal from the parent form
        public void onStopClick(EventArgs e)
        {
                if(run)
                stop = true;
            
        }
        //resets the program counter register back to the entry point
        public void resetCounter()
        {
            Registers.setRegister(15, instart);
          
        }
        //sets instart to <addr>
        public void setCounter(uint addr)
        {
            instart = addr;
        }
        //sets up the signal to the main thread that this thread is finished
        public void onFinishExecution(CompRunFinishedEventArgs e)
        {
            EventHandler<CompRunFinishedEventArgs> handler = CompRunFinished;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        //sends output to the Form1 model class using Event mechanism
        public void onOutputDetected(SendToConsoleEventArgs e)
        {
            EventHandler<SendToConsoleEventArgs> handler = ConsoleChar;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        //toggles tracing on or off
        public void setTrace()
        {
            if (!trace)
            {
                configureTrace();
                trace = true;
                
                sw = new StreamWriter("trace.log");
            }
            else
            {
                sw.Flush();
                sw.Close();
                trace = false;
            }
        }
        //flushes the current trace.log file
        public void configureTrace()
        {
            if (File.Exists("trace.log"))
            {
                File.Delete("trace.log");
            }
           //File.Create("trace.log");
         
        }
        //writes output to trace.log
        public void writeTrace(uint insval)
        {
            if (!tr) { TraceStep--; return; }
            string line = "";
            if (TraceStep == 342)
            {
                int ff = 32;
            }
            line += TraceStep.ToString("D6")+" ";
            line += insval.ToString("X8") + " ";
            line += RAM.Checksum().ToString("X8") + " ";
            int fi = 1;
            while (fi < 5)
            {
                bool x = Memory.TestBit(32 - fi, Registers.cpsr);
                if (x) line += "1";
                else line += "0";
                fi++;
            }
            line += " ";
            if (this.Registers.modetrack == 0)
                line += "SYS";
            else if (this.Registers.modetrack == 1)
                line += "SVC";
            else
                line += "IRQ";
            if (!exectrace && this.Registers.modetrack != 0)
                return;
            int index = 0;
            while (index < 15)
            {
                line += " "+ index.ToString() +"="+getReg(index).ToString("X8");
                index++;
            }
            line.Remove(line.LastIndexOf(" "));

            sw.Write(line+"\r\n");
                sw.Flush();
            
        }
        //add a char ot the internal input variable and set the irq flag to 0;
        public void addChar(char c)
        {
            chars = c;
            cpu.irq = 1;
        }
        public char getChar()
        {
            return chars;
        }
        public event EventHandler<CompRunFinishedEventArgs> CompRunFinished;
        public event EventHandler<SendToConsoleEventArgs> ConsoleChar;
      
    }
    //used to handle the computer finishing execution
    public class CompRunFinishedEventArgs : EventArgs
    {
        public int end { get; set; }
    }
    public class SendToConsoleEventArgs: EventArgs
    {
        public char c { get; set; }
    }
}

