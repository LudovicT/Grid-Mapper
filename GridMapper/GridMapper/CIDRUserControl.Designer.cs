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
			this.ipAddress = new IPAddressControlLib.IPAddressControl();
			this.slash = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			( (System.ComponentModel.ISupportInitialize)( this.numericUpDown1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// ipAddress
			// 
			this.ipAddress.AllowInternalTab = false;
			this.ipAddress.AutoHeight = true;
			this.ipAddress.BackColor = System.Drawing.SystemColors.Window;
			this.ipAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ipAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ipAddress.Location = new System.Drawing.Point( 35, 3 );
			this.ipAddress.MinimumSize = new System.Drawing.Size( 87, 20 );
			this.ipAddress.Name = "ipAddress";
			this.ipAddress.ReadOnly = false;
			this.ipAddress.Size = new System.Drawing.Size( 87, 20 );
			this.ipAddress.TabIndex = 0;
			this.ipAddress.Text = "...";
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
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point( 142, 3 );
			this.numericUpDown1.Maximum = new decimal( new int[] {
            32,
            0,
            0,
            0} );
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size( 36, 20 );
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.Value = new decimal( new int[] {
            24,
            0,
            0,
            0} );
			this.numericUpDown1.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.numericUpDown1_KeyPress );
			// 
			// CIDRUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add( this.numericUpDown1 );
			this.Controls.Add( this.slash );
			this.Controls.Add( this.ipAddress );
			this.Margin = new System.Windows.Forms.Padding( 2 );
			this.Name = "CIDRUserControl";
			this.Size = new System.Drawing.Size( 270, 25 );
			( (System.ComponentModel.ISupportInitialize)( this.numericUpDown1 ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

		private IPAddressControlLib.IPAddressControl ipAddress;
		private System.Windows.Forms.Label slash;
		private System.Windows.Forms.NumericUpDown numericUpDown1;

    }
}
