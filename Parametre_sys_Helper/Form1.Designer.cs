namespace Parametre_sys_Helper
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
            this.label1 = new System.Windows.Forms.Label();
            this.bGenererArbre = new System.Windows.Forms.Button();
            this.tbDefinition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entrez la définition du parametre :";
            // 
            // bGenererArbre
            // 
            this.bGenererArbre.Location = new System.Drawing.Point(8, 186);
            this.bGenererArbre.Name = "bGenererArbre";
            this.bGenererArbre.Size = new System.Drawing.Size(101, 23);
            this.bGenererArbre.TabIndex = 20;
            this.bGenererArbre.Text = "Générer l\'arbre";
            this.bGenererArbre.UseVisualStyleBackColor = true;
            // 
            // tbDefinition
            // 
            this.tbDefinition.AcceptsTab = true;
            this.tbDefinition.Location = new System.Drawing.Point(8, 25);
            this.tbDefinition.Multiline = true;
            this.tbDefinition.Name = "tbDefinition";
            this.tbDefinition.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDefinition.Size = new System.Drawing.Size(236, 155);
            this.tbDefinition.TabIndex = 10;
            this.tbDefinition.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 216);
            this.Controls.Add(this.tbDefinition);
            this.Controls.Add(this.bGenererArbre);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(300, 255);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parametre.sys Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bGenererArbre;
        private System.Windows.Forms.TextBox tbDefinition;
    }
}

