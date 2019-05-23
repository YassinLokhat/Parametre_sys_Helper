namespace Parametre_sys_Helper
{
    partial class ConfigForm
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
            this.scPanels = new System.Windows.Forms.SplitContainer();
            this.tvParametre = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.générerLeCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developpéParYassinLOKHATToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scPanels)).BeginInit();
            this.scPanels.Panel1.SuspendLayout();
            this.scPanels.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scPanels
            // 
            this.scPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scPanels.Location = new System.Drawing.Point(0, 24);
            this.scPanels.Name = "scPanels";
            // 
            // scPanels.Panel1
            // 
            this.scPanels.Panel1.Controls.Add(this.tvParametre);
            this.scPanels.Size = new System.Drawing.Size(800, 487);
            this.scPanels.SplitterDistance = 224;
            this.scPanels.SplitterWidth = 5;
            this.scPanels.TabIndex = 0;
            // 
            // tvParametre
            // 
            this.tvParametre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvParametre.FullRowSelect = true;
            this.tvParametre.HideSelection = false;
            this.tvParametre.Location = new System.Drawing.Point(0, 0);
            this.tvParametre.Name = "tvParametre";
            this.tvParametre.PathSeparator = " \\ ";
            this.tvParametre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tvParametre.Size = new System.Drawing.Size(224, 487);
            this.tvParametre.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.générerLeCodeToolStripMenuItem,
            this.developpéParYassinLOKHATToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // générerLeCodeToolStripMenuItem
            // 
            this.générerLeCodeToolStripMenuItem.Name = "générerLeCodeToolStripMenuItem";
            this.générerLeCodeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.générerLeCodeToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.générerLeCodeToolStripMenuItem.Text = "Générer le code";
            this.générerLeCodeToolStripMenuItem.Click += new System.EventHandler(this.générerLeCodeToolStripMenuItem_Click);
            // 
            // developpéParYassinLOKHATToolStripMenuItem
            // 
            this.developpéParYassinLOKHATToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.developpéParYassinLOKHATToolStripMenuItem.Name = "developpéParYassinLOKHATToolStripMenuItem";
            this.developpéParYassinLOKHATToolStripMenuItem.Size = new System.Drawing.Size(178, 20);
            this.developpéParYassinLOKHATToolStripMenuItem.Text = "Developpé par Yassin LOKHAT";
            this.developpéParYassinLOKHATToolStripMenuItem.Click += new System.EventHandler(this.developpéParYassinLOKHATToolStripMenuItem_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 511);
            this.Controls.Add(this.scPanels);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.scPanels.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scPanels)).EndInit();
            this.scPanels.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scPanels;
        private System.Windows.Forms.TreeView tvParametre;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem générerLeCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developpéParYassinLOKHATToolStripMenuItem;
    }
}