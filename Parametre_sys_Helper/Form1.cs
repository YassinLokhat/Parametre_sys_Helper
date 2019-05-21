using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parametre_sys_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bGenererArbre.Click += BGenererArbre_Click;
            this.SizeChanged += Form1_SizeChanged;
            this.Size = new Size(500, 400);
        }

        private void BGenererArbre_Click(object sender, EventArgs e)
        {
            if(tbDefinition.Text == "")
                return;

            new ConfigForm(tbDefinition.Text.Replace("\r", "").Split('\n').ToList()).Show();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int padding = 5;
            int costante = 20;

            bGenererArbre.Location = new Point(bGenererArbre.Location.X, this.Height - padding - bGenererArbre.Height - costante - costante);
            tbDefinition.Size = new Size(this.Width - padding - padding - costante, this.Height - tbDefinition.Location.Y - padding - bGenererArbre.Height - padding - costante - costante);
        }
    }
}
