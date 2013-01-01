namespace GridMapper
{
    partial class StringUserControl
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
			this.StringInput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// StringInput
			// 
			this.StringInput.Location = new System.Drawing.Point( 2, 2 );
			this.StringInput.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.StringInput.Name = "StringInput";
			this.StringInput.Size = new System.Drawing.Size( 265, 20 );
			this.StringInput.TabIndex = 0;
			// 
			// StringUserControl1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add( this.StringInput );
			this.Margin = new System.Windows.Forms.Padding( 2, 2, 2, 2 );
			this.Name = "StringUserControl1";
			this.Size = new System.Drawing.Size( 270, 25 );
			this.ResumeLayout( false );
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StringInput;
    }
}
