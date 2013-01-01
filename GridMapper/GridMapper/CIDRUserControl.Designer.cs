namespace GridMapper
{
    partial class CIDRUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
			this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.slash = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ipAddressControl1
			// 
			this.ipAddressControl1.AllowInternalTab = false;
			this.ipAddressControl1.AutoHeight = true;
			this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
			this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ipAddressControl1.Location = new System.Drawing.Point( 35, 3 );
			this.ipAddressControl1.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ipAddressControl1.Name = "ipAddressControl1";
			this.ipAddressControl1.ReadOnly = false;
			this.ipAddressControl1.Size = new System.Drawing.Size( 87, 20 );
			this.ipAddressControl1.TabIndex = 0;
			this.ipAddressControl1.Text = "...";
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point( 142, 3 );
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size( 26, 20 );
			this.maskedTextBox1.TabIndex = 1;
			// 
			// slash
			// 
			this.slash.AutoSize = true;
			this.slash.Location = new System.Drawing.Point( 124, 6 );
			this.slash.Name = "slash";
			this.slash.Size = new System.Drawing.Size( 12, 13 );
			this.slash.TabIndex = 2;
			this.slash.Text = "/";
			// 
			// CIDRUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add( this.slash );
			this.Controls.Add( this.maskedTextBox1 );
			this.Controls.Add( this.ipAddressControl1 );
			this.Margin = new System.Windows.Forms.Padding( 2 );
			this.Name = "CIDRUserControl";
			this.Size = new System.Drawing.Size( 270, 25 );
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

		private IPAddressControlLib.IPAddressControl ipAddressControl1;
		private System.Windows.Forms.MaskedTextBox maskedTextBox1;
		private System.Windows.Forms.Label slash;

    }
}
