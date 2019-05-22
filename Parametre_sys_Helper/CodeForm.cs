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
    public partial class CodeForm : Form
    {
        private CColoredTextBox cctCode = null;

        public CodeForm(string code)
        {
            InitializeComponent();

            cctCode = new CColoredTextBox();
            cctCode.Dock = DockStyle.Fill;
            cctCode.Text = code;
            this.Controls.Add(cctCode);
        }
    }
}
