﻿namespace GridMapper
{
	partial class GridWindow
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( GridWindow ) );
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aRPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dNSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timeOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TimeoutSelected = new System.Windows.Forms.ToolStripTextBox();
			this.maximumTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
			this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.fastScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.portScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iPScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.LoadScan = new System.Windows.Forms.ToolStripButton();
			this.SaveScan = new System.Windows.Forms.ToolStripButton();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.FromIP = new IPAddressControlLib.IPAddressControl();
			this.ToIP = new IPAddressControlLib.IPAddressControl();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ProgressScan = new System.Windows.Forms.ProgressBar();
			this.timer1 = new System.Windows.Forms.Timer( this.components );
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1} );
			this.statusStrip1.Location = new System.Drawing.Point( 0, 442 );
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size( 686, 22 );
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size( 52, 17 );
			this.toolStripStatusLabel1.Text = "Progress";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.startToolStripMenuItem1} );
			this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size( 686, 24 );
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem} );
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
			this.fileToolStripMenuItem.Text = "File";
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size( 100, 22 );
			this.startToolStripMenuItem.Text = "Start";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size( 100, 22 );
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler( this.saveToolStripMenuItem_Click );
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size( 100, 22 );
			this.loadToolStripMenuItem.Text = "Load";
			this.loadToolStripMenuItem.Click += new System.EventHandler( this.loadToolStripMenuItem_Click );
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size( 100, 22 );
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
			// 
			// optionToolStripMenuItem
			// 
			this.optionToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem,
            this.aRPToolStripMenuItem,
            this.dNSToolStripMenuItem,
            this.portToolStripMenuItem,
            this.timeOutToolStripMenuItem,
            this.maximumTaskToolStripMenuItem} );
			this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
			this.optionToolStripMenuItem.Size = new System.Drawing.Size( 56, 20 );
			this.optionToolStripMenuItem.Text = "Option";
			// 
			// pingToolStripMenuItem
			// 
			this.pingToolStripMenuItem.Checked = true;
			this.pingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
			this.pingToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.pingToolStripMenuItem.Text = "Ping (cannot be changed)";
			// 
			// aRPToolStripMenuItem
			// 
			this.aRPToolStripMenuItem.Checked = true;
			this.aRPToolStripMenuItem.CheckOnClick = true;
			this.aRPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.aRPToolStripMenuItem.Name = "aRPToolStripMenuItem";
			this.aRPToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.aRPToolStripMenuItem.Text = "ARP";
			// 
			// dNSToolStripMenuItem
			// 
			this.dNSToolStripMenuItem.Checked = true;
			this.dNSToolStripMenuItem.CheckOnClick = true;
			this.dNSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.dNSToolStripMenuItem.Name = "dNSToolStripMenuItem";
			this.dNSToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.dNSToolStripMenuItem.Text = "DNS";
			// 
			// portToolStripMenuItem
			// 
			this.portToolStripMenuItem.Checked = true;
			this.portToolStripMenuItem.CheckOnClick = true;
			this.portToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.portToolStripMenuItem.Name = "portToolStripMenuItem";
			this.portToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.portToolStripMenuItem.Text = "Port";
			// 
			// timeOutToolStripMenuItem
			// 
			this.timeOutToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.TimeoutSelected} );
			this.timeOutToolStripMenuItem.Name = "timeOutToolStripMenuItem";
			this.timeOutToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.timeOutToolStripMenuItem.Text = "Timeout";
			// 
			// TimeoutSelected
			// 
			this.TimeoutSelected.Name = "TimeoutSelected";
			this.TimeoutSelected.Size = new System.Drawing.Size( 100, 23 );
			// 
			// maximumTaskToolStripMenuItem
			// 
			this.maximumTaskToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2} );
			this.maximumTaskToolStripMenuItem.Name = "maximumTaskToolStripMenuItem";
			this.maximumTaskToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.maximumTaskToolStripMenuItem.Text = "Maximum Task";
			// 
			// toolStripTextBox2
			// 
			this.toolStripTextBox2.Name = "toolStripTextBox2";
			this.toolStripTextBox2.Size = new System.Drawing.Size( 100, 23 );
			// 
			// startToolStripMenuItem1
			// 
			this.startToolStripMenuItem1.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fastScanToolStripMenuItem,
            this.portScanToolStripMenuItem,
            this.iPScanToolStripMenuItem} );
			this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
			this.startToolStripMenuItem1.Size = new System.Drawing.Size( 43, 20 );
			this.startToolStripMenuItem1.Text = "Start";
			// 
			// fastScanToolStripMenuItem
			// 
			this.fastScanToolStripMenuItem.Name = "fastScanToolStripMenuItem";
			this.fastScanToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.fastScanToolStripMenuItem.Text = "Fast scan";
			this.fastScanToolStripMenuItem.Click += new System.EventHandler( this.fastScanToolStripMenuItem_Click_1 );
			// 
			// portScanToolStripMenuItem
			// 
			this.portScanToolStripMenuItem.Name = "portScanToolStripMenuItem";
			this.portScanToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.portScanToolStripMenuItem.Text = "Port scan";
			// 
			// iPScanToolStripMenuItem
			// 
			this.iPScanToolStripMenuItem.Name = "iPScanToolStripMenuItem";
			this.iPScanToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
			this.iPScanToolStripMenuItem.Text = "IP scan";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.LoadScan,
            this.SaveScan} );
			this.toolStrip1.Location = new System.Drawing.Point( 0, 24 );
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size( 686, 25 );
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// LoadScan
			// 
			this.LoadScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LoadScan.Image = ( (System.Drawing.Image)( resources.GetObject( "LoadScan.Image" ) ) );
			this.LoadScan.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LoadScan.Name = "LoadScan";
			this.LoadScan.Size = new System.Drawing.Size( 23, 22 );
			this.LoadScan.Text = "LoadScan";
			this.LoadScan.Click += new System.EventHandler( this.toolStripButton1_Click );
			// 
			// SaveScan
			// 
			this.SaveScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveScan.Image = ( (System.Drawing.Image)( resources.GetObject( "SaveScan.Image" ) ) );
			this.SaveScan.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveScan.Name = "SaveScan";
			this.SaveScan.Size = new System.Drawing.Size( 23, 22 );
			this.SaveScan.Text = "SaveScan";
			this.SaveScan.Click += new System.EventHandler( this.SaveScan_Click );
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point( 0, 49 );
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size( 686, 393 );
			this.dataGridView1.TabIndex = 3;
			// 
			// FromIP
			// 
			this.FromIP.AllowInternalTab = false;
			this.FromIP.AutoHeight = true;
			this.FromIP.BackColor = System.Drawing.SystemColors.Window;
			this.FromIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.FromIP.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.FromIP.Location = new System.Drawing.Point( 129, 20 );
			this.FromIP.Margin = new System.Windows.Forms.Padding( 2 );
			this.FromIP.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.FromIP.Name = "FromIP";
			this.FromIP.ReadOnly = false;
			this.FromIP.Size = new System.Drawing.Size( 114, 20 );
			this.FromIP.TabIndex = 4;
			this.FromIP.Text = "0.0.0.0";
			this.FromIP.Click += new System.EventHandler( this.ipAddressControl1_Click );
			// 
			// ToIP
			// 
			this.ToIP.AllowInternalTab = false;
			this.ToIP.AutoHeight = true;
			this.ToIP.BackColor = System.Drawing.SystemColors.Window;
			this.ToIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ToIP.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ToIP.Location = new System.Drawing.Point( 283, 20 );
			this.ToIP.Margin = new System.Windows.Forms.Padding( 2 );
			this.ToIP.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ToIP.Name = "ToIP";
			this.ToIP.ReadOnly = false;
			this.ToIP.Size = new System.Drawing.Size( 114, 20 );
			this.ToIP.TabIndex = 5;
			this.ToIP.Text = "0.0.0.0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 63, 23 );
			this.label1.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 62, 13 );
			this.label1.TabIndex = 6;
			this.label1.Text = "Range from";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 256, 23 );
			this.label2.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 16, 13 );
			this.label2.TabIndex = 7;
			this.label2.Text = "to";
			// 
			// ProgressScan
			// 
			this.ProgressScan.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.ProgressScan.Location = new System.Drawing.Point( 65, 444 );
			this.ProgressScan.Margin = new System.Windows.Forms.Padding( 2 );
			this.ProgressScan.Name = "ProgressScan";
			this.ProgressScan.Size = new System.Drawing.Size( 612, 20 );
			this.ProgressScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressScan.TabIndex = 8;
			// 
			// timer1
			// 
			this.timer1.Interval = 50;
			this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
			// 
			// GridWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 686, 464 );
			this.Controls.Add( this.ProgressScan );
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.ToIP );
			this.Controls.Add( this.FromIP );
			this.Controls.Add( this.dataGridView1 );
			this.Controls.Add( this.toolStrip1 );
			this.Controls.Add( this.statusStrip1 );
			this.Controls.Add( this.menuStrip1 );
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "GridWindow";
			this.Text = "Grid Mapper";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.GridWindow_FormClosed );
			this.Load += new System.EventHandler( this.Form1_Load );
			this.statusStrip1.ResumeLayout( false );
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout( false );
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout( false );
			this.toolStrip1.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton LoadScan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private IPAddressControlLib.IPAddressControl FromIP;
        private IPAddressControlLib.IPAddressControl ToIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar ProgressScan;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SaveScan;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fastScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portScanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iPScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox TimeoutSelected;
        private System.Windows.Forms.ToolStripMenuItem maximumTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
		private System.Windows.Forms.Timer timer1;
	}
}

