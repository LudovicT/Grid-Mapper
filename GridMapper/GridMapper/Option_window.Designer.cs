namespace GridMapper
{
    partial class Option_window
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Option_window ) );
			this.option_cancel = new System.Windows.Forms.Button();
			this.option_accept = new System.Windows.Forms.Button();
			this.option_ping = new System.Windows.Forms.CheckBox();
			this.option_ARPing = new System.Windows.Forms.CheckBox();
			this.option_DNS = new System.Windows.Forms.CheckBox();
			this.option_port = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.option_ARP = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// option_cancel
			// 
			this.option_cancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_cancel.Location = new System.Drawing.Point( 128, 203 );
			this.option_cancel.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_cancel.Name = "option_cancel";
			this.option_cancel.Size = new System.Drawing.Size( 56, 19 );
			this.option_cancel.TabIndex = 0;
			this.option_cancel.Text = "Cancel";
			this.option_cancel.UseVisualStyleBackColor = true;
			this.option_cancel.Click += new System.EventHandler( this.option_cancel_Click );
			// 
			// option_accept
			// 
			this.option_accept.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_accept.Location = new System.Drawing.Point( 56, 203 );
			this.option_accept.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_accept.Name = "option_accept";
			this.option_accept.Size = new System.Drawing.Size( 56, 19 );
			this.option_accept.TabIndex = 1;
			this.option_accept.Text = "OK";
			this.option_accept.UseVisualStyleBackColor = true;
			this.option_accept.Click += new System.EventHandler( this.option_accept_Click );
			// 
			// option_ping
			// 
			this.option_ping.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_ping.AutoSize = true;
			this.option_ping.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.option_ping.Checked = true;
			this.option_ping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.option_ping.Enabled = false;
			this.option_ping.ForeColor = System.Drawing.SystemColors.GrayText;
			this.option_ping.Location = new System.Drawing.Point( 9, 28 );
			this.option_ping.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_ping.Name = "option_ping";
			this.option_ping.Size = new System.Drawing.Size( 149, 17 );
			this.option_ping.TabIndex = 2;
			this.option_ping.Text = "Ping (cannot be changed)";
			this.option_ping.UseVisualStyleBackColor = true;
			// 
			// option_ARPing
			// 
			this.option_ARPing.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_ARPing.AutoSize = true;
			this.option_ARPing.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.option_ARPing.Location = new System.Drawing.Point( 9, 50 );
			this.option_ARPing.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_ARPing.Name = "option_ARPing";
			this.option_ARPing.Size = new System.Drawing.Size( 71, 17 );
			this.option_ARPing.TabIndex = 3;
			this.option_ARPing.Text = "ARP ping";
			this.option_ARPing.UseVisualStyleBackColor = true;
			// 
			// option_DNS
			// 
			this.option_DNS.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_DNS.AutoSize = true;
			this.option_DNS.Checked = true;
			this.option_DNS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.option_DNS.Location = new System.Drawing.Point( 9, 92 );
			this.option_DNS.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_DNS.Name = "option_DNS";
			this.option_DNS.Size = new System.Drawing.Size( 49, 17 );
			this.option_DNS.TabIndex = 4;
			this.option_DNS.Text = "DNS";
			this.option_DNS.UseVisualStyleBackColor = true;
			// 
			// option_port
			// 
			this.option_port.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_port.AutoSize = true;
			this.option_port.Location = new System.Drawing.Point( 9, 113 );
			this.option_port.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.option_port.Name = "option_port";
			this.option_port.Size = new System.Drawing.Size( 50, 17 );
			this.option_port.TabIndex = 5;
			this.option_port.Text = "Ports";
			this.option_port.UseVisualStyleBackColor = true;
			this.option_port.CheckedChanged += new System.EventHandler( this.option_port_CheckedChanged );
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.textBox1.Location = new System.Drawing.Point( 103, 139 );
			this.textBox1.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size( 80, 20 );
			this.textBox1.TabIndex = 6;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.textBox2.Location = new System.Drawing.Point( 103, 162 );
			this.textBox2.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size( 80, 20 );
			this.textBox2.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 6, 140 );
			this.label1.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 69, 13 );
			this.label1.TabIndex = 8;
			this.label1.Text = "Ping Timeout";
			// 
			// label2
			// 
			this.label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 6, 163 );
			this.label2.Margin = new System.Windows.Forms.Padding( 2, 0, 2, 0 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 78, 13 );
			this.label2.TabIndex = 9;
			this.label2.Text = "Maximum Task";
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.textBox3.Location = new System.Drawing.Point( 103, 113 );
			this.textBox3.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size( 80, 20 );
			this.textBox3.TabIndex = 10;
			this.textBox3.Visible = false;
			// 
			// option_ARP
			// 
			this.option_ARP.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.option_ARP.AutoSize = true;
			this.option_ARP.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.option_ARP.Checked = true;
			this.option_ARP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.option_ARP.Location = new System.Drawing.Point( 9, 71 );
			this.option_ARP.Margin = new System.Windows.Forms.Padding( 2 );
			this.option_ARP.Name = "option_ARP";
			this.option_ARP.Size = new System.Drawing.Size( 48, 17 );
			this.option_ARP.TabIndex = 11;
			this.option_ARP.Text = "ARP";
			this.option_ARP.UseVisualStyleBackColor = true;
			// 
			// Option_window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size( 194, 232 );
			this.ControlBox = false;
			this.Controls.Add( this.option_ARP );
			this.Controls.Add( this.textBox3 );
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.textBox2 );
			this.Controls.Add( this.textBox1 );
			this.Controls.Add( this.option_port );
			this.Controls.Add( this.option_DNS );
			this.Controls.Add( this.option_ARPing );
			this.Controls.Add( this.option_ping );
			this.Controls.Add( this.option_accept );
			this.Controls.Add( this.option_cancel );
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.DoubleBuffered = true;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
			this.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.MinimumSize = new System.Drawing.Size( 210, 270 );
			this.Name = "Option_window";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.TopMost = true;
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button option_cancel;
        private System.Windows.Forms.Button option_accept;
        private System.Windows.Forms.CheckBox option_ping;
        private System.Windows.Forms.CheckBox option_ARPing;
        private System.Windows.Forms.CheckBox option_DNS;
        private System.Windows.Forms.CheckBox option_port;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.CheckBox option_ARP;
    }
}