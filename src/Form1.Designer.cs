namespace armsim
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RunBtn = new System.Windows.Forms.ToolStripButton();
            this.Step = new System.Windows.Forms.ToolStripButton();
            this.Stop = new System.Windows.Forms.ToolStripButton();
            this.Reset = new System.Windows.Forms.ToolStripButton();
            this.BreakBtn = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleBreakpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.terminalBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.memBtn = new System.Windows.Forms.Button();
            this.memBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.memoryView = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Byte4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.disassemblyView = new System.Windows.Forms.DataGridView();
            this.AddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineLangColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssemblyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.fileNamePanel = new System.Windows.Forms.StatusBarPanel();
            this.checkSumPanel = new System.Windows.Forms.StatusBarPanel();
            this.opModePanel = new System.Windows.Forms.StatusBarPanel();
            this.BreakPanel = new System.Windows.Forms.StatusBarPanel();
            this.TracePanel = new System.Windows.Forms.StatusBarPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.RegPanel = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.flagView = new System.Windows.Forms.DataGridView();
            this.ZFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegisterView = new System.Windows.Forms.DataGridView();
            this.Registers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackTab = new System.Windows.Forms.TabPage();
            this.stackView = new System.Windows.Forms.DataGridView();
            this.AddressCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackValCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.disassemblyView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileNamePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opModePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BreakPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TracePanel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.RegPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterView)).BeginInit();
            this.StackTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Highlight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunBtn,
            this.Step,
            this.Stop,
            this.Reset,
            this.BreakBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1501, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RunBtn
            // 
            this.RunBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunBtn.Image = ((System.Drawing.Image)(resources.GetObject("RunBtn.Image")));
            this.RunBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(24, 24);
            this.RunBtn.Text = "toolStripButton1";
            this.RunBtn.ToolTipText = "Run";
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // Step
            // 
            this.Step.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Step.Image = ((System.Drawing.Image)(resources.GetObject("Step.Image")));
            this.Step.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Step.Name = "Step";
            this.Step.Size = new System.Drawing.Size(24, 24);
            this.Step.Text = "toolStripButton1";
            this.Step.ToolTipText = "Step";
            this.Step.Click += new System.EventHandler(this.Step_Click);
            // 
            // Stop
            // 
            this.Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(24, 24);
            this.Stop.Text = "Stop";
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Reset
            // 
            this.Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Reset.Image = ((System.Drawing.Image)(resources.GetObject("Reset.Image")));
            this.Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(24, 24);
            this.Reset.ToolTipText = "Reset";
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // BreakBtn
            // 
            this.BreakBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BreakBtn.Image = ((System.Drawing.Image)(resources.GetObject("BreakBtn.Image")));
            this.BreakBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BreakBtn.Name = "BreakBtn";
            this.BreakBtn.Size = new System.Drawing.Size(24, 24);
            this.BreakBtn.Text = "toolStripButton1";
            this.BreakBtn.ToolTipText = "Break";
            this.BreakBtn.Click += new System.EventHandler(this.BreakBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1501, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.traceToolStripMenuItem,
            this.toggleBreakpointsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // traceToolStripMenuItem
            // 
            this.traceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.traceToolStripMenuItem.Name = "traceToolStripMenuItem";
            this.traceToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.traceToolStripMenuItem.Text = "Trace";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(105, 26);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(105, 26);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // toggleBreakpointsToolStripMenuItem
            // 
            this.toggleBreakpointsToolStripMenuItem.Name = "toggleBreakpointsToolStripMenuItem";
            this.toggleBreakpointsToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.toggleBreakpointsToolStripMenuItem.Text = "Toggle Breakpoints";
            this.toggleBreakpointsToolStripMenuItem.Click += new System.EventHandler(this.toggleBreakpointsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(972, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(2, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 99.22028F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.779727F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 128);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.5679F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.30556F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 647);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.terminalBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 460);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(963, 184);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Terminal";
            // 
            // terminalBox
            // 
            this.terminalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.terminalBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.terminalBox.Location = new System.Drawing.Point(6, 21);
            this.terminalBox.Multiline = true;
            this.terminalBox.Name = "terminalBox";
            this.terminalBox.Size = new System.Drawing.Size(939, 151);
            this.terminalBox.TabIndex = 0;
            this.terminalBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.terminalBox_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.memBtn);
            this.groupBox1.Controls.Add(this.memBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.memoryView);
            this.groupBox1.Location = new System.Drawing.Point(3, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(963, 228);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Memory";
            // 
            // memBtn
            // 
            this.memBtn.Location = new System.Drawing.Point(305, 15);
            this.memBtn.Name = "memBtn";
            this.memBtn.Size = new System.Drawing.Size(97, 23);
            this.memBtn.TabIndex = 16;
            this.memBtn.Text = "Filter";
            this.memBtn.UseVisualStyleBackColor = true;
            this.memBtn.Click += new System.EventHandler(this.memBtn_Click);
            // 
            // memBox
            // 
            this.memBox.Location = new System.Drawing.Point(199, 15);
            this.memBox.Name = "memBox";
            this.memBox.Size = new System.Drawing.Size(100, 22);
            this.memBox.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Input Display Location (hex):";
            // 
            // memoryView
            // 
            this.memoryView.AllowUserToAddRows = false;
            this.memoryView.AllowUserToDeleteRows = false;
            this.memoryView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.memoryView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.memoryView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.memoryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memoryView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Byte1,
            this.Byte2,
            this.Byte3,
            this.Byte4});
            this.memoryView.Location = new System.Drawing.Point(6, 43);
            this.memoryView.Name = "memoryView";
            this.memoryView.RowTemplate.Height = 24;
            this.memoryView.Size = new System.Drawing.Size(939, 180);
            this.memoryView.TabIndex = 13;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // Byte1
            // 
            this.Byte1.HeaderText = "0 bit Offset";
            this.Byte1.Name = "Byte1";
            // 
            // Byte2
            // 
            this.Byte2.HeaderText = "4 Bit Offset";
            this.Byte2.Name = "Byte2";
            // 
            // Byte3
            // 
            this.Byte3.HeaderText = "8 Bit Offset";
            this.Byte3.Name = "Byte3";
            // 
            // Byte4
            // 
            this.Byte4.HeaderText = "12 Bit Offset";
            this.Byte4.Name = "Byte4";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.disassemblyView);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 217);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dissasembly";
            // 
            // disassemblyView
            // 
            this.disassemblyView.AllowUserToAddRows = false;
            this.disassemblyView.AllowUserToDeleteRows = false;
            this.disassemblyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disassemblyView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.disassemblyView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.disassemblyView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.disassemblyView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.disassemblyView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddressColumn,
            this.MachineLangColumn,
            this.AssemblyColumn});
            this.disassemblyView.Location = new System.Drawing.Point(3, 21);
            this.disassemblyView.Name = "disassemblyView";
            this.disassemblyView.RowTemplate.Height = 24;
            this.disassemblyView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.disassemblyView.Size = new System.Drawing.Size(942, 190);
            this.disassemblyView.TabIndex = 2;
            // 
            // AddressColumn
            // 
            this.AddressColumn.HeaderText = "Address";
            this.AddressColumn.Name = "AddressColumn";
            // 
            // MachineLangColumn
            // 
            this.MachineLangColumn.HeaderText = "Machine Instruction";
            this.MachineLangColumn.Name = "MachineLangColumn";
            // 
            // AssemblyColumn
            // 
            this.AssemblyColumn.HeaderText = "Assembly";
            this.AssemblyColumn.Name = "AssemblyColumn";
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 830);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.fileNamePanel,
            this.checkSumPanel,
            this.opModePanel,
            this.BreakPanel,
            this.TracePanel});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(1501, 32);
            this.statusBar1.TabIndex = 12;
            this.statusBar1.Text = "File Loaded:";
            // 
            // fileNamePanel
            // 
            this.fileNamePanel.Name = "fileNamePanel";
            this.fileNamePanel.Width = 200;
            // 
            // checkSumPanel
            // 
            this.checkSumPanel.Name = "checkSumPanel";
            this.checkSumPanel.Width = 200;
            // 
            // opModePanel
            // 
            this.opModePanel.Name = "opModePanel";
            this.opModePanel.Text = "Operating Mode: None";
            this.opModePanel.Width = 300;
            // 
            // BreakPanel
            // 
            this.BreakPanel.Name = "BreakPanel";
            this.BreakPanel.Text = "Breakpoints: On";
            this.BreakPanel.Width = 110;
            // 
            // TracePanel
            // 
            this.TracePanel.Name = "TracePanel";
            this.TracePanel.Text = "Trace Mode: On";
            this.TracePanel.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(525, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.RegPanel);
            this.tabControl1.Controls.Add(this.StackTab);
            this.tabControl1.Location = new System.Drawing.Point(1019, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(482, 697);
            this.tabControl1.TabIndex = 16;
            // 
            // RegPanel
            // 
            this.RegPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.RegPanel.Controls.Add(this.label3);
            this.RegPanel.Controls.Add(this.flagView);
            this.RegPanel.Controls.Add(this.RegisterView);
            this.RegPanel.Location = new System.Drawing.Point(4, 25);
            this.RegPanel.Name = "RegPanel";
            this.RegPanel.Padding = new System.Windows.Forms.Padding(3);
            this.RegPanel.Size = new System.Drawing.Size(474, 668);
            this.RegPanel.TabIndex = 0;
            this.RegPanel.Text = "Registers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 550);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Flags";
            // 
            // flagView
            // 
            this.flagView.AllowUserToAddRows = false;
            this.flagView.AllowUserToDeleteRows = false;
            this.flagView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.flagView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.flagView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.flagView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ZFlag,
            this.CFlag,
            this.FFlag,
            this.NFlag,
            this.IRQ});
            this.flagView.Location = new System.Drawing.Point(3, 570);
            this.flagView.Name = "flagView";
            this.flagView.RowTemplate.Height = 24;
            this.flagView.Size = new System.Drawing.Size(468, 77);
            this.flagView.TabIndex = 1;
            // 
            // ZFlag
            // 
            this.ZFlag.HeaderText = "Zero";
            this.ZFlag.Name = "ZFlag";
            // 
            // CFlag
            // 
            this.CFlag.HeaderText = "Car";
            this.CFlag.Name = "CFlag";
            // 
            // FFlag
            // 
            this.FFlag.HeaderText = "Over";
            this.FFlag.Name = "FFlag";
            // 
            // NFlag
            // 
            this.NFlag.HeaderText = "Neg";
            this.NFlag.Name = "NFlag";
            // 
            // IRQ
            // 
            this.IRQ.HeaderText = "IRQ";
            this.IRQ.Name = "IRQ";
            // 
            // RegisterView
            // 
            this.RegisterView.AllowUserToAddRows = false;
            this.RegisterView.AllowUserToDeleteRows = false;
            this.RegisterView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RegisterView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RegisterView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.RegisterView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RegisterView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegisterView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Registers,
            this.Value});
            this.RegisterView.Location = new System.Drawing.Point(3, 3);
            this.RegisterView.Name = "RegisterView";
            this.RegisterView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.RegisterView.RowTemplate.Height = 24;
            this.RegisterView.Size = new System.Drawing.Size(471, 515);
            this.RegisterView.TabIndex = 0;
            // 
            // Registers
            // 
            this.Registers.HeaderText = "Register";
            this.Registers.Name = "Registers";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // StackTab
            // 
            this.StackTab.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.StackTab.Controls.Add(this.stackView);
            this.StackTab.Location = new System.Drawing.Point(4, 25);
            this.StackTab.Name = "StackTab";
            this.StackTab.Padding = new System.Windows.Forms.Padding(3);
            this.StackTab.Size = new System.Drawing.Size(474, 668);
            this.StackTab.TabIndex = 2;
            this.StackTab.Text = "Stack";
            // 
            // stackView
            // 
            this.stackView.AllowUserToAddRows = false;
            this.stackView.AllowUserToDeleteRows = false;
            this.stackView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.stackView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.stackView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stackView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddressCol,
            this.StackValCol});
            this.stackView.Location = new System.Drawing.Point(0, 0);
            this.stackView.Name = "stackView";
            this.stackView.RowTemplate.Height = 24;
            this.stackView.Size = new System.Drawing.Size(362, 388);
            this.stackView.TabIndex = 0;
            // 
            // AddressCol
            // 
            this.AddressCol.HeaderText = "Address";
            this.AddressCol.Name = "AddressCol";
            // 
            // StackValCol
            // 
            this.StackValCol.HeaderText = "Value";
            this.StackValCol.Name = "StackValCol";
            // 
            // modeBox
            // 
            this.modeBox.Location = new System.Drawing.Point(142, 92);
            this.modeBox.Name = "modeBox";
            this.modeBox.ReadOnly = true;
            this.modeBox.Size = new System.Drawing.Size(100, 22);
            this.modeBox.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Processor Mode:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1501, 862);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.modeBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Prototype";
            this.Shown += new System.EventHandler(this.opener);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.disassemblyView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileNamePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opModePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BreakPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TracePanel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.RegPanel.ResumeLayout(false);
            this.RegPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterView)).EndInit();
            this.StackTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stackView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RunBtn;
        private System.Windows.Forms.ToolStripButton Step;
        private System.Windows.Forms.ToolStripButton Stop;
        private System.Windows.Forms.ToolStripButton Reset;
        private System.Windows.Forms.ToolStripButton BreakBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView memoryView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox terminalBox;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.DataGridView disassemblyView;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineLangColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssemblyColumn;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.StatusBarPanel fileNamePanel;
        private System.Windows.Forms.StatusBarPanel checkSumPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusBarPanel opModePanel;
        private System.Windows.Forms.TextBox memBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button memBtn;
        private System.Windows.Forms.StatusBarPanel BreakPanel;
        private System.Windows.Forms.ToolStripMenuItem toggleBreakpointsToolStripMenuItem;
        private System.Windows.Forms.StatusBarPanel TracePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Byte4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RegPanel;
        private System.Windows.Forms.DataGridView RegisterView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.TabPage StackTab;
        private System.Windows.Forms.DataGridView stackView;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackValCol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView flagView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn FFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRQ;
        private System.Windows.Forms.TextBox modeBox;
        private System.Windows.Forms.Label label4;
    }
}

