using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parametre_sys_Helper
{
    public class TypePanel : GroupBox
    {
        public Dictionary<string, Control> Fields;
        int margin = 5;

        public TypePanel(string name, string[] fields)
        {
            Fields = new Dictionary<string, Control>();
            this.Text = name;

            foreach (string field in fields)
            {
                switch (field)
                {
                    case "":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
