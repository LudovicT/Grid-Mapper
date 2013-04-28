namespace GridMapper
{
    partial class gridmapperv2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Options = new System.Windows.Forms.CheckedListBox();
            this.packets_gridView = new System.Windows.Forms.DataGridView();
            this.IP_SRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP_DST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Packet_Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Secured = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.packets_gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.FormattingEnabled = true;
            this.Options.Items.AddRange(new object[] {
            "Mail (POP, IMAP)",
            "Web (HTTP)"});
            this.Options.Location = new System.Drawing.Point(0, 44);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(112, 34);
            this.Options.TabIndex = 1;
            // 
            // packets_gridView
            // 
            this.packets_gridView.AllowUserToDeleteRows = false;
            this.packets_gridView.AllowUserToOrderColumns = true;
            this.packets_gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.packets_gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packets_gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP_SRC,
            this.IP_DST,
            this.Packet_Data,
            this.Protocol,
            this.Secured});
            this.packets_gridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.packets_gridView.Location = new System.Drawing.Point(0, 177);
            this.packets_gridView.Name = "packets_gridView";
            this.packets_gridView.ReadOnly = true;
            this.packets_gridView.RowTemplate.ReadOnly = true;
            this.packets_gridView.Size = new System.Drawing.Size(603, 356);
            this.packets_gridView.TabIndex = 2;
            this.packets_gridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packets_gridView_CellContentClick);
            // 
            // IP_SRC
            // 
            this.IP_SRC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IP_SRC.HeaderText = "IP_SRC";
            this.IP_SRC.Name = "IP_SRC";
            this.IP_SRC.ReadOnly = true;
            // 
            // IP_DST
            // 
            this.IP_DST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IP_DST.HeaderText = "IP_DST";
            this.IP_DST.Name = "IP_DST";
            this.IP_DST.ReadOnly = true;
            // 
            // Packet_Data
            // 
            this.Packet_Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Packet_Data.HeaderText = "Packet_Data";
            this.Packet_Data.Name = "Packet_Data";
            this.Packet_Data.ReadOnly = true;
            // 
            // Protocol
            // 
            this.Protocol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Protocol.HeaderText = "Protocol";
            this.Protocol.Name = "Protocol";
            this.Protocol.ReadOnly = true;
            // 
            // Secured
            // 
            this.Secured.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Secured.HeaderText = "Secured";
            this.Secured.Name = "Secured";
            this.Secured.ReadOnly = true;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(253, 44);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 34);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(362, 44);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(75, 34);
            this.browser.TabIndex = 5;
            this.browser.Text = "Browser view";
            this.browser.UseVisualStyleBackColor = true;
            // 
            // gridmapperv2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 533);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.packets_gridView);
            this.Controls.Add(this.Options);
            this.Name = "gridmapperv2";
            this.Text = "gridmapperv2";
            ((System.ComponentModel.ISupportInitialize)(this.packets_gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox Options;
        private System.Windows.Forms.DataGridView packets_gridView;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP_SRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP_DST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Packet_Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Secured;
        private System.Windows.Forms.Button browser;
    }
}