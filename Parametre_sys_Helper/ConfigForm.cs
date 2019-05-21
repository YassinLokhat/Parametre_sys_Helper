using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Parametre_sys_Helper
{
    public partial class ConfigForm : Form
    {
        private static XmlDocument _Grammar = null;
        public static XmlDocument Grammar
        {
            get
            {
                if (_Grammar == null)
                {
                    _Grammar = new XmlDocument();
                    _Grammar.LoadXml(File.ReadAllText("grammaire.xml"));
                }

                return _Grammar;
            }
        }

        public ConfigForm(List<string> definition)
        {
            InitializeComponent();

            TreeNodePanel root = new TreeNodePanel(definition[0], true);
            tvParametre.Nodes.Add(root);
            definition.RemoveAt(0);

            TreeNodePanel last = null;

            for (int i = 0; i < definition.Count; i++)
            {
                string def = definition[i];
                int level = getLevel(ref def) + 1;
                TreeNodePanel node = new TreeNodePanel(def);

                if (level == 1)
                {
                    root.Nodes.Add(node);
                }
                else
                {
                    if (last.Level == level)
                        last.Parent.Nodes.Add(node);
                    else
                        last.Nodes.Add(node);
                }

                last = node;
            }

            tvParametre.ExpandAll();

            scPanels.Panel2.Controls.Add(root.Panel);
            tvParametre.AfterSelect += TvParametre_AfterSelect;
        }

        private void TvParametre_AfterSelect(object sender, TreeViewEventArgs e)
        {
            scPanels.Panel2.Controls.Clear();
            scPanels.Panel2.Controls.Add(((TreeNodePanel)e.Node).Panel);
            if (e.Node.Parent != null && e.Node.Nodes.Count > 0)
                ((TreeNodePanel)e.Node).SetType("Champ Tableau");
            else
                ((TreeNodePanel)e.Node).RemoveType("Champ Tableau");
        }

        private int getLevel(ref string node)
        {
            int output = 0;

            while (node.StartsWith("\t"))
            {
                node = node.Substring(1);
                output++;
            }

            return output;
        }

        private void générerLeCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
