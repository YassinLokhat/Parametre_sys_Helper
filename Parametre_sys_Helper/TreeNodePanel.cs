using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Parametre_sys_Helper
{
    public class TreeNodePanel : TreeNode
    {
        public Panel Panel = null;
        private Label label = null;
        private ComboBox comboBox = null;
        public TypePanel TypePanel = null;

        public TreeNodePanel(string text, bool isRoot = false) : base(text)
        {
            this.Text = this.Name = text;

            label = new Label();
            label.Text = "Type :";
            label.Location = new System.Drawing.Point(0, 0);
            label.Size = new System.Drawing.Size(37, 13);

            comboBox = new ComboBox();
            foreach (XmlNode type in ConfigForm.Grammar.SelectNodes("//type"))
                comboBox.Items.Add(type.Attributes["name"].Value);
            comboBox.Items.Remove("Parametre");
            comboBox.Location = new System.Drawing.Point(label.Width + TypePanel.Margin, 0);
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            Panel = new Panel();
            Panel.Controls.Add(label);
            Panel.Controls.Add(comboBox);

            if (isRoot)
                SetType("Parametre");
            else
                comboBox.SelectedIndex = 0;

            Panel.SizeChanged += Panel_SizeChanged;
            Panel.Dock = DockStyle.Fill;
        }

        public void SetType(string type)
        {
            label.Hide();
            label.Size = new System.Drawing.Size(0, 0);
            comboBox.Hide();
            comboBox.Size = new System.Drawing.Size(0, 0);
            comboBox.Text = type;
            ComboBox_SelectedIndexChanged(null, null);
        }

        public void RemoveType(string type)
        {
            if (comboBox.Items.Contains(type))
                comboBox.Items.Remove(type);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypePanel != null)
            {
                Panel.Controls.Remove(TypePanel);
                TypePanel.Dispose();
                TypePanel = null;
            }

            TypePanel = new TypePanel(ConfigForm.Grammar.SelectSingleNode("//type[@name='" + comboBox.Text + "']").OuterXml.Replace("default=\"\"", "default='" + Text.Replace("'", "&apos;") + "'"));
            TypePanel.Location = new System.Drawing.Point(0, Math.Max(label.Height, comboBox.Height) + TypePanel.Margin);
            Panel.Controls.Add(TypePanel);
            Panel_SizeChanged(null, null);
        }

        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            comboBox.Size = new System.Drawing.Size(Panel.Width - TypePanel.Margin - label.Location.X - label.Width - TypePanel.Margin, comboBox.Height);
            TypePanel.Size = new System.Drawing.Size(Panel.Width, Panel.Height - TypePanel.Margin - Math.Max(label.Height, comboBox.Height) - TypePanel.Margin);
        }

        public string GetOutput()
        {
            string ident = "";
            string output = "";

            for (int i = 0; i < this.Level; i++)
                ident += "\t";
            if (Level != 0)
                ident += "\t";

            try
            {
                output = ident + TypePanel.Output.Replace("\\n", "\n").Replace("\\t", "\t") + "\n";
            }
            catch (Exception ex)
            {
                this.TreeView.SelectedNode = this;
                throw new Exception(this.FullPath + " :\n" + ex.Message);
            }

            string content = "\n";
            if (Nodes.Count > 0 && Level != 0)
                content += ident + "{\n";

            foreach (TreeNodePanel node in Nodes)
                content += node.GetOutput();

            if (Nodes.Count > 0 && Level != 0)
                content += ident + "}";

            return output.Replace("{{_DEFINITION_CONTENU_}}", content);
        }
    }
}
