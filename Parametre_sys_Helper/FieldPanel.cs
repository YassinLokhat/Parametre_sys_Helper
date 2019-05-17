using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Parametre_sys_Helper
{
    public class TypePanel : GroupBox
    {
        public static int Margin = 5;

        public Dictionary<string, Field> Fields;
        private string _Code = "";
        public string Code
        {
            get
            {
                return _Code;
            }
        }

        public TypePanel(XmlNode xmlType) : base()
        {
            this.Size = new Size(5, 5);

            Fields = new Dictionary<string, Field>();
            this.Text = xmlType.Attributes["name"].Value;
            _Code = xmlType.SelectSingleNode("//type[@mane='" + this.Text + "']/text").InnerText;

            foreach (XmlNode fieldNode in xmlType.SelectNodes("//type[@mane='" + this.Text + "']/field"))
            {
                Field field = Field.GetField(fieldNode);
                field.Location = new Point(Margin, this.Size.Height);
                this.Size = new Size(Math.Max(this.Width, Margin + field.Width + Margin), this.Height + field.Height + Margin);
                Fields[field.Key] = field;
                this.Controls.Add(field);
            }

            this.SizeChanged += Field_SizeChanged;
        }

        private void Field_SizeChanged(object sender, EventArgs e)
        {
            foreach (Field field in Fields.Values)
            {
                field.Size = new Size(- Margin + this.Width - Margin, field.Height);
            }
        }
    }

    public class Field : Panel
    {
        public bool IsMendatory { get; set; }
        public string Input { get; set; }
        public string Label { get; set; }
        protected Label label = null;
        public string Key { get; set; }
        protected string _Output = "";

        public Field(XmlNode xmlField)
        {
            IsMendatory = xmlField.Attributes["ismendatory"].Value == "true";
            Input = xmlField.Attributes["input"].Value;
            Key = xmlField.Attributes["key"].Value;
            _Output = xmlField.Attributes["output"].Value;
            Label = xmlField.Attributes["label"].Value;
            label = new Label();
            label.Text = Label != "" ? Label + " :" : "";
            label.Location = new Point(0, 0);
            this.Controls.Add(label);
            this.Size = new Size(label.Width, label.Height);
        }

        public static Field GetField(XmlNode xmlField)
        {
            switch (xmlField.Attributes["input"].Value)
            {
                case "textbox":
                    return new TextBoxField(xmlField);
                case "combobox":
                    return new ComboBoxField(xmlField);
                case "checkbox":
                    return new CheckBoxField(xmlField);
                case "radiobox":
                    return new RadioBoxField(xmlField);
                case "groupbox":
                    return new GroupBoxField(xmlField);

                default:
                    return new Field(xmlField);
            }
        }
    }

    public class TextBoxField : Field
    {
        private TextBox textBox = null;
        public string Output
        {
            get
            {
                return _Output;
            }
        }

        public TextBoxField(XmlNode xmlField) : base(xmlField)
        {
            textBox = new TextBox();
            textBox.Location = new Point(label.Location.X + label.Width + TypePanel.Margin, label.Location.Y);
        }
    }

    public class ComboBoxField : Field
    {

        public ComboBoxField(XmlNode xmlField) : base(xmlField)
        {

        }
    }

    public class CheckBoxField : Field
    {

        public CheckBoxField(XmlNode xmlField) : base(xmlField)
        {

        }
    }

    public class RadioBoxField : Field
    {

        public RadioBoxField(XmlNode xmlField) : base(xmlField)
        {

        }
    }

    public class GroupBoxField : Field
    {

        public GroupBoxField(XmlNode xmlField) : base(xmlField)
        {

        }
    }
}
