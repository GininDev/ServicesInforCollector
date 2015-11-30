using ServicesInforCollector.Core.Components;
using ServicesInforCollector.UiControls;

namespace ServicesInforCollector
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem tsmiServiceStart;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbWmi = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpGrid = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new ServicesInforCollector.Core.Components.SortableGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSkip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiServiceStop = new System.Windows.Forms.ToolStripMenuItem();
            this.startModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chageToManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.change2AutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.change2DisableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiServiceDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.explorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpText = new System.Windows.Forms.TabPage();
            this.rtbOut = new System.Windows.Forms.RichTextBox();
            this.tpTree = new System.Windows.Forms.TabPage();
            this.jsonTree2 = new ServicesInforCollector.UiControls.JsonTree();
            this.wmiSumInfo1 = new ServicesInforCollector.UiControls.WmiSumInfo();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tbExclude = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tbConfigNameFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cbStartMode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbDs = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpTextOut = new System.Windows.Forms.TabPage();
            this.tbDirectoryOut = new System.Windows.Forms.TextBox();
            this.tpDiagram = new System.Windows.Forms.TabPage();
            this.jsonTree1 = new ServicesInforCollector.UiControls.JsonTree();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tbDirecotry = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.btnFolderView = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnDirectoryWatcher = new System.Windows.Forms.Button();
            this.ofdJson = new System.Windows.Forms.OpenFileDialog();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.fbdPros = new System.Windows.Forms.FolderBrowserDialog();
            tsmiServiceStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tbWmi.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tpText.SuspendLayout();
            this.tpTree.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tbDs.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpTextOut.SuspendLayout();
            this.tpDiagram.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmiServiceStart
            // 
            tsmiServiceStart.AutoSize = false;
            tsmiServiceStart.Name = "tsmiServiceStart";
            tsmiServiceStart.Size = new System.Drawing.Size(152, 22);
            tsmiServiceStart.Tag = "2";
            tsmiServiceStart.Text = "Start Service";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbWmi);
            this.tabControl1.Controls.Add(this.tbDs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(852, 454);
            this.tabControl1.TabIndex = 6;
            // 
            // tbWmi
            // 
            this.tbWmi.Controls.Add(this.panel2);
            this.tbWmi.Controls.Add(this.panel3);
            this.tbWmi.Location = new System.Drawing.Point(4, 22);
            this.tbWmi.Name = "tbWmi";
            this.tbWmi.Padding = new System.Windows.Forms.Padding(3);
            this.tbWmi.Size = new System.Drawing.Size(844, 428);
            this.tbWmi.TabIndex = 0;
            this.tbWmi.Text = "WmiQuery";
            this.tbWmi.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel19);
            this.panel2.Controls.Add(this.panel13);
            this.panel2.Controls.Add(this.panel10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(838, 395);
            this.panel2.TabIndex = 7;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.tcMain);
            this.panel19.Controls.Add(this.wmiSumInfo1);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(0, 50);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(838, 345);
            this.panel19.TabIndex = 2;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpGrid);
            this.tcMain.Controls.Add(this.tpText);
            this.tcMain.Controls.Add(this.tpTree);
            this.tcMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.RightToLeftLayout = true;
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(658, 345);
            this.tcMain.TabIndex = 5;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpGrid
            // 
            this.tpGrid.Controls.Add(this.dataGridView1);
            this.tpGrid.Location = new System.Drawing.Point(4, 22);
            this.tpGrid.Name = "tpGrid";
            this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrid.Size = new System.Drawing.Size(650, 319);
            this.tpGrid.TabIndex = 1;
            this.tpGrid.Text = "Grid";
            this.tpGrid.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(644, 313);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSkip,
            this.toolStripSeparator1,
            this.tsmiServiceStop,
            tsmiServiceStart,
            this.startModeToolStripMenuItem,
            this.tsmiServiceDelete,
            this.toolStripSeparator2,
            this.explorerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 148);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // tsmiSkip
            // 
            this.tsmiSkip.Name = "tsmiSkip";
            this.tsmiSkip.Size = new System.Drawing.Size(178, 22);
            this.tsmiSkip.Tag = "0";
            this.tsmiSkip.Text = "Add Name to Filter";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // tsmiServiceStop
            // 
            this.tsmiServiceStop.ForeColor = System.Drawing.Color.Red;
            this.tsmiServiceStop.Name = "tsmiServiceStop";
            this.tsmiServiceStop.Size = new System.Drawing.Size(178, 22);
            this.tsmiServiceStop.Tag = "1";
            this.tsmiServiceStop.Text = "Stop Service";
            // 
            // startModeToolStripMenuItem
            // 
            this.startModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chageToManualToolStripMenuItem,
            this.change2AutoToolStripMenuItem,
            this.change2DisableToolStripMenuItem});
            this.startModeToolStripMenuItem.Name = "startModeToolStripMenuItem";
            this.startModeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.startModeToolStripMenuItem.Text = "StartMode";
            this.startModeToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.startModeToolStripMenuItem_DropDownItemClicked);
            // 
            // chageToManualToolStripMenuItem
            // 
            this.chageToManualToolStripMenuItem.Name = "chageToManualToolStripMenuItem";
            this.chageToManualToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.chageToManualToolStripMenuItem.Tag = "41";
            this.chageToManualToolStripMenuItem.Text = "Change2Manual";
            // 
            // change2AutoToolStripMenuItem
            // 
            this.change2AutoToolStripMenuItem.Name = "change2AutoToolStripMenuItem";
            this.change2AutoToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.change2AutoToolStripMenuItem.Tag = "42";
            this.change2AutoToolStripMenuItem.Text = "Change2Auto";
            // 
            // change2DisableToolStripMenuItem
            // 
            this.change2DisableToolStripMenuItem.Name = "change2DisableToolStripMenuItem";
            this.change2DisableToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.change2DisableToolStripMenuItem.Tag = "43";
            this.change2DisableToolStripMenuItem.Text = "Change2Disable";
            // 
            // tsmiServiceDelete
            // 
            this.tsmiServiceDelete.ForeColor = System.Drawing.Color.Red;
            this.tsmiServiceDelete.Name = "tsmiServiceDelete";
            this.tsmiServiceDelete.Size = new System.Drawing.Size(178, 22);
            this.tsmiServiceDelete.Tag = "5";
            this.tsmiServiceDelete.Text = "Delete Service !!!";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // explorerToolStripMenuItem
            // 
            this.explorerToolStripMenuItem.Name = "explorerToolStripMenuItem";
            this.explorerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.explorerToolStripMenuItem.Tag = "3";
            this.explorerToolStripMenuItem.Text = "Explorer to...";
            // 
            // tpText
            // 
            this.tpText.Controls.Add(this.rtbOut);
            this.tpText.Location = new System.Drawing.Point(4, 22);
            this.tpText.Name = "tpText";
            this.tpText.Padding = new System.Windows.Forms.Padding(3);
            this.tpText.Size = new System.Drawing.Size(650, 319);
            this.tpText.TabIndex = 0;
            this.tpText.Text = "Text";
            this.tpText.UseVisualStyleBackColor = true;
            // 
            // rtbOut
            // 
            this.rtbOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOut.Location = new System.Drawing.Point(3, 3);
            this.rtbOut.Name = "rtbOut";
            this.rtbOut.Size = new System.Drawing.Size(644, 313);
            this.rtbOut.TabIndex = 3;
            this.rtbOut.Text = "";
            // 
            // tpTree
            // 
            this.tpTree.Controls.Add(this.jsonTree2);
            this.tpTree.Location = new System.Drawing.Point(4, 22);
            this.tpTree.Name = "tpTree";
            this.tpTree.Padding = new System.Windows.Forms.Padding(3);
            this.tpTree.Size = new System.Drawing.Size(650, 319);
            this.tpTree.TabIndex = 2;
            this.tpTree.Text = "Tree";
            this.tpTree.UseVisualStyleBackColor = true;
            // 
            // jsonTree2
            // 
            this.jsonTree2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTree2.Location = new System.Drawing.Point(3, 3);
            this.jsonTree2.Name = "jsonTree2";
            this.jsonTree2.Size = new System.Drawing.Size(644, 313);
            this.jsonTree2.TabIndex = 0;
            // 
            // wmiSumInfo1
            // 
            this.wmiSumInfo1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wmiSumInfo1.Location = new System.Drawing.Point(658, 0);
            this.wmiSumInfo1.Name = "wmiSumInfo1";
            this.wmiSumInfo1.Size = new System.Drawing.Size(180, 345);
            this.wmiSumInfo1.TabIndex = 4;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel7);
            this.panel13.Controls.Add(this.panel6);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 27);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(838, 23);
            this.panel13.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tbExclude);
            this.panel7.Controls.Add(this.btnClearFilter);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(427, 23);
            this.panel7.TabIndex = 2;
            // 
            // tbExclude
            // 
            this.tbExclude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExclude.Location = new System.Drawing.Point(58, 0);
            this.tbExclude.Name = "tbExclude";
            this.tbExclude.Size = new System.Drawing.Size(341, 21);
            this.tbExclude.TabIndex = 28;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearFilter.Location = new System.Drawing.Point(399, 0);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(28, 23);
            this.btnClearFilter.TabIndex = 26;
            this.btnClearFilter.Text = "X";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 25;
            this.label6.Text = "Filter:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tbConfigNameFilter);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(427, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(411, 23);
            this.panel6.TabIndex = 1;
            // 
            // tbConfigNameFilter
            // 
            this.tbConfigNameFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConfigNameFilter.Location = new System.Drawing.Point(111, 0);
            this.tbConfigNameFilter.Name = "tbConfigNameFilter";
            this.tbConfigNameFilter.Size = new System.Drawing.Size(300, 21);
            this.tbConfigNameFilter.TabIndex = 24;
            this.tbConfigNameFilter.Text = ".cfg|.config|.exe.config|.xml";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 23);
            this.label5.TabIndex = 23;
            this.label5.Text = "ConfigNameFilter:";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel4);
            this.panel10.Controls.Add(this.panel1);
            this.panel10.Controls.Add(this.panel8);
            this.panel10.Controls.Add(this.panel9);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(838, 27);
            this.panel10.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbDesc);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(261, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 27);
            this.panel4.TabIndex = 4;
            // 
            // tbDesc
            // 
            this.tbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDesc.Location = new System.Drawing.Point(43, 0);
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(223, 21);
            this.tbDesc.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 27);
            this.label8.TabIndex = 22;
            this.label8.Text = "desc:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 27);
            this.panel1.TabIndex = 3;
            // 
            // tbName
            // 
            this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbName.Location = new System.Drawing.Point(58, 0);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(203, 21);
            this.tbName.TabIndex = 17;
            this.tbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyUp);
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 27);
            this.label7.TabIndex = 16;
            this.label7.Text = "name:";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cbStartMode);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(527, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(175, 27);
            this.panel8.TabIndex = 2;
            // 
            // cbStartMode
            // 
            this.cbStartMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStartMode.FormattingEnabled = true;
            this.cbStartMode.Items.AddRange(new object[] {
            "Auto",
            "Disabled",
            "Manual"});
            this.cbStartMode.Location = new System.Drawing.Point(70, 0);
            this.cbStartMode.Name = "cbStartMode";
            this.cbStartMode.Size = new System.Drawing.Size(105, 20);
            this.cbStartMode.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 27);
            this.label9.TabIndex = 19;
            this.label9.Text = "startMode:";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.cbState);
            this.panel9.Controls.Add(this.label10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(702, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(136, 27);
            this.panel9.TabIndex = 1;
            // 
            // cbState
            // 
            this.cbState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbState.FormattingEnabled = true;
            this.cbState.Items.AddRange(new object[] {
            "Stopped",
            "Running"});
            this.cbState.Location = new System.Drawing.Point(41, 0);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(95, 20);
            this.cbState.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 27);
            this.label10.TabIndex = 17;
            this.label10.Text = "state:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLoadFromFile);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 398);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(838, 27);
            this.panel3.TabIndex = 4;
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(671, 3);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFromFile.TabIndex = 4;
            this.btnLoadFromFile.Text = "Load";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(88, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(752, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(7, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Query";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbDs
            // 
            this.tbDs.Controls.Add(this.tabControl2);
            this.tbDs.Controls.Add(this.panel11);
            this.tbDs.Location = new System.Drawing.Point(4, 22);
            this.tbDs.Name = "tbDs";
            this.tbDs.Padding = new System.Windows.Forms.Padding(3);
            this.tbDs.Size = new System.Drawing.Size(844, 428);
            this.tbDs.TabIndex = 1;
            this.tbDs.Text = "DirectorySearch";
            this.tbDs.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpTextOut);
            this.tabControl2.Controls.Add(this.tpDiagram);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 32);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(838, 393);
            this.tabControl2.TabIndex = 2;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tpTextOut
            // 
            this.tpTextOut.Controls.Add(this.tbDirectoryOut);
            this.tpTextOut.Location = new System.Drawing.Point(4, 22);
            this.tpTextOut.Name = "tpTextOut";
            this.tpTextOut.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextOut.Size = new System.Drawing.Size(830, 367);
            this.tpTextOut.TabIndex = 0;
            this.tpTextOut.Text = "Text";
            this.tpTextOut.UseVisualStyleBackColor = true;
            // 
            // tbDirectoryOut
            // 
            this.tbDirectoryOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDirectoryOut.Location = new System.Drawing.Point(3, 3);
            this.tbDirectoryOut.Multiline = true;
            this.tbDirectoryOut.Name = "tbDirectoryOut";
            this.tbDirectoryOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDirectoryOut.Size = new System.Drawing.Size(824, 361);
            this.tbDirectoryOut.TabIndex = 4;
            this.tbDirectoryOut.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbDirectoryOut_KeyUp);
            // 
            // tpDiagram
            // 
            this.tpDiagram.Controls.Add(this.jsonTree1);
            this.tpDiagram.Location = new System.Drawing.Point(4, 22);
            this.tpDiagram.Name = "tpDiagram";
            this.tpDiagram.Padding = new System.Windows.Forms.Padding(3);
            this.tpDiagram.Size = new System.Drawing.Size(830, 367);
            this.tpDiagram.TabIndex = 1;
            this.tpDiagram.Text = "Diagram";
            this.tpDiagram.UseVisualStyleBackColor = true;
            // 
            // jsonTree1
            // 
            this.jsonTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTree1.Location = new System.Drawing.Point(3, 3);
            this.jsonTree1.Name = "jsonTree1";
            this.jsonTree1.Size = new System.Drawing.Size(824, 361);
            this.jsonTree1.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.tbDirecotry);
            this.panel11.Controls.Add(this.label11);
            this.panel11.Controls.Add(this.panel14);
            this.panel11.Controls.Add(this.panel5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(3, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(838, 29);
            this.panel11.TabIndex = 0;
            // 
            // tbDirecotry
            // 
            this.tbDirecotry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDirecotry.Location = new System.Drawing.Point(89, 0);
            this.tbDirecotry.Name = "tbDirecotry";
            this.tbDirecotry.Size = new System.Drawing.Size(593, 21);
            this.tbDirecotry.TabIndex = 3;
            this.tbDirecotry.Text = "H:\\系统备份\\深圳系统备份\\20151117\\I_app1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "Search Folder:";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.btnFolderView);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(682, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(37, 29);
            this.panel14.TabIndex = 1;
            // 
            // btnFolderView
            // 
            this.btnFolderView.Location = new System.Drawing.Point(4, 1);
            this.btnFolderView.Name = "btnFolderView";
            this.btnFolderView.Size = new System.Drawing.Size(31, 23);
            this.btnFolderView.TabIndex = 0;
            this.btnFolderView.Text = "...";
            this.btnFolderView.UseVisualStyleBackColor = true;
            this.btnFolderView.Click += new System.EventHandler(this.btnFolderView_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnConfig);
            this.panel5.Controls.Add(this.btnDirectoryWatcher);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(719, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(119, 29);
            this.panel5.TabIndex = 0;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(62, 1);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(51, 23);
            this.btnConfig.TabIndex = 6;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnDirectoryWatcher
            // 
            this.btnDirectoryWatcher.Location = new System.Drawing.Point(4, 1);
            this.btnDirectoryWatcher.Name = "btnDirectoryWatcher";
            this.btnDirectoryWatcher.Size = new System.Drawing.Size(51, 23);
            this.btnDirectoryWatcher.TabIndex = 4;
            this.btnDirectoryWatcher.Text = "Watch";
            this.btnDirectoryWatcher.UseVisualStyleBackColor = true;
            this.btnDirectoryWatcher.Click += new System.EventHandler(this.btnDirectoryWatcher_Click);
            // 
            // ofdJson
            // 
            this.ofdJson.DefaultExt = "*.json";
            this.ofdJson.RestoreDirectory = true;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.panel18);
            this.panel15.Controls.Add(this.panel17);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel15.Location = new System.Drawing.Point(277, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(413, 28);
            this.panel15.TabIndex = 0;
            // 
            // panel18
            // 
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(136, 28);
            this.panel18.TabIndex = 2;
            // 
            // panel17
            // 
            this.panel17.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel17.Location = new System.Drawing.Point(136, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(152, 28);
            this.panel17.TabIndex = 1;
            // 
            // panel16
            // 
            this.panel16.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel16.Location = new System.Drawing.Point(288, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(125, 28);
            this.panel16.TabIndex = 0;
            // 
            // fbdPros
            // 
            this.fbdPros.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbdPros.ShowNewFolderButton = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(852, 454);
            this.Controls.Add(this.tabControl1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Windows Services Information Collector  Ver 0.1.122";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbWmi.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpText.ResumeLayout(false);
            this.tpTree.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tbDs.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tpTextOut.ResumeLayout(false);
            this.tpTextOut.PerformLayout();
            this.tpDiagram.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbWmi;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TabPage tbDs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSkip;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.OpenFileDialog ofdJson;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiServiceStop;
        private System.Windows.Forms.ToolStripMenuItem explorerToolStripMenuItem;

        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel17;

        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox tbConfigNameFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox cbStartMode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnDirectoryWatcher;
        private System.Windows.Forms.Button btnFolderView;
        private System.Windows.Forms.TextBox tbDirecotry;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FolderBrowserDialog fbdPros;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.ToolStripMenuItem startModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chageToManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem change2AutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem change2DisableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiServiceDelete;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpTextOut;
        private System.Windows.Forms.TextBox tbDirectoryOut;
        private System.Windows.Forms.TabPage tpDiagram;
        private System.Windows.Forms.TextBox tbExclude;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpGrid;
        private System.Windows.Forms.TabPage tpText;
        private WmiSumInfo wmiSumInfo1;
        private SortableGridView dataGridView1;
        private JsonTree jsonTree1;
        private System.Windows.Forms.RichTextBox rtbOut;
        private System.Windows.Forms.TabPage tpTree;
        private JsonTree jsonTree2;
    }
}

