namespace GridMapper
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.ProgressScan = new System.Windows.Forms.ProgressBar();
			this.timer1 = new System.Windows.Forms.Timer( this.components );
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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.advancedOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.fastScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.portScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iPScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.typeOfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cRIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanButton = new System.Windows.Forms.Button();
			this.LoadScan = new System.Windows.Forms.ToolStripButton();
			this.SaveScan = new System.Windows.Forms.ToolStripButton();
			this.ipRangeUserControl1 = new GridMapper.IPRangeUserControl();
			this.toolStrip1.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.toolStrip1.CanOverflow = false;
			this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.LoadScan,
            this.SaveScan} );
			this.toolStrip1.Location = new System.Drawing.Point( 0, 24 );
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size( 414, 25 );
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridView1.Location = new System.Drawing.Point( 0, 49 );
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size( 414, 337 );
			this.dataGridView1.TabIndex = 3;
			// 
			// ProgressScan
			// 
			this.ProgressScan.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressScan.Location = new System.Drawing.Point( 0, 384 );
			this.ProgressScan.Margin = new System.Windows.Forms.Padding( 2 );
			this.ProgressScan.Name = "ProgressScan";
			this.ProgressScan.Size = new System.Drawing.Size( 414, 21 );
			this.ProgressScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressScan.TabIndex = 8;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler( this.timer1_Tick );
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
			this.startToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size( 100, 22 );
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler( this.startToolStripMenuItem_Click );
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
            this.toolStripSeparator1,
            this.advancedOptionToolStripMenuItem} );
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
			this.pingToolStripMenuItem.Visible = false;
			// 
			// aRPToolStripMenuItem
			// 
			this.aRPToolStripMenuItem.Checked = true;
			this.aRPToolStripMenuItem.CheckOnClick = true;
			this.aRPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.aRPToolStripMenuItem.Name = "aRPToolStripMenuItem";
			this.aRPToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.aRPToolStripMenuItem.Text = "ARP";
			this.aRPToolStripMenuItem.Visible = false;
			// 
			// dNSToolStripMenuItem
			// 
			this.dNSToolStripMenuItem.Checked = true;
			this.dNSToolStripMenuItem.CheckOnClick = true;
			this.dNSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.dNSToolStripMenuItem.Name = "dNSToolStripMenuItem";
			this.dNSToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.dNSToolStripMenuItem.Text = "DNS";
			this.dNSToolStripMenuItem.Visible = false;
			// 
			// portToolStripMenuItem
			// 
			this.portToolStripMenuItem.Checked = true;
			this.portToolStripMenuItem.CheckOnClick = true;
			this.portToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.portToolStripMenuItem.Name = "portToolStripMenuItem";
			this.portToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.portToolStripMenuItem.Text = "Port";
			this.portToolStripMenuItem.Visible = false;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size( 208, 6 );
			// 
			// advancedOptionToolStripMenuItem
			// 
			this.advancedOptionToolStripMenuItem.Name = "advancedOptionToolStripMenuItem";
			this.advancedOptionToolStripMenuItem.Size = new System.Drawing.Size( 211, 22 );
			this.advancedOptionToolStripMenuItem.Text = "Advanced options";
			this.advancedOptionToolStripMenuItem.Click += new System.EventHandler( this.advancedOptionToolStripMenuItem_Click );
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
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.startToolStripMenuItem1,
            this.typeOfToolStripMenuItem} );
			this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size( 414, 24 );
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// typeOfToolStripMenuItem
			// 
			this.typeOfToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.rangeToolStripMenuItem,
            this.cRIDToolStripMenuItem,
            this.stringToolStripMenuItem} );
			this.typeOfToolStripMenuItem.Name = "typeOfToolStripMenuItem";
			this.typeOfToolStripMenuItem.Size = new System.Drawing.Size( 59, 20 );
			this.typeOfToolStripMenuItem.Text = "Type of";
			// 
			// rangeToolStripMenuItem
			// 
			this.rangeToolStripMenuItem.Name = "rangeToolStripMenuItem";
			this.rangeToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
			this.rangeToolStripMenuItem.Text = "Range";
			// 
			// cRIDToolStripMenuItem
			// 
			this.cRIDToolStripMenuItem.Name = "cRIDToolStripMenuItem";
			this.cRIDToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
			this.cRIDToolStripMenuItem.Text = "CRID";
			// 
			// stringToolStripMenuItem
			// 
			this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
			this.stringToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
			this.stringToolStripMenuItem.Text = "String";
			// 
			// ScanButton
			// 
			this.ScanButton.BackgroundImage = global::GridMapper.Properties.Resources.Sans_titre;
			this.ScanButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ScanButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ScanButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ScanButton.ForeColor = System.Drawing.SystemColors.Window;
			this.ScanButton.Location = new System.Drawing.Point( 368, 15 );
			this.ScanButton.Margin = new System.Windows.Forms.Padding( 0 );
			this.ScanButton.Name = "ScanButton";
			this.ScanButton.Size = new System.Drawing.Size( 44, 28 );
			this.ScanButton.TabIndex = 9;
			this.ScanButton.UseVisualStyleBackColor = true;
			this.ScanButton.Click += new System.EventHandler( this.ScanButton_Click );
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
			// ipRangeUserControl1
			// 
			this.ipRangeUserControl1.BackColor = System.Drawing.Color.Transparent;
			this.ipRangeUserControl1.Location = new System.Drawing.Point( 76, 22 );
			this.ipRangeUserControl1.Name = "ipRangeUserControl1";
			this.ipRangeUserControl1.Size = new System.Drawing.Size( 271, 27 );
			this.ipRangeUserControl1.TabIndex = 10;
			// 
			// GridWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size( 414, 405 );
			this.Controls.Add( this.ipRangeUserControl1 );
			this.Controls.Add( this.ScanButton );
			this.Controls.Add( this.ProgressScan );
			this.Controls.Add( this.dataGridView1 );
			this.Controls.Add( this.toolStrip1 );
			this.Controls.Add( this.menuStrip1 );
			this.DoubleBuffered = true;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "GridWindow";
			this.Text = "Grid Mapper";
			this.Load += new System.EventHandler( this.Form1_Load );
			this.toolStrip1.ResumeLayout( false );
			this.toolStrip1.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit();
			this.menuStrip1.ResumeLayout( false );
			this.menuStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton LoadScan;
		private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar ProgressScan;
        private System.Windows.Forms.ToolStripButton SaveScan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem advancedOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fastScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPScanToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem typeOfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cRIDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItem;
		private IPRangeUserControl ipRangeUserControl1;
	}
}

