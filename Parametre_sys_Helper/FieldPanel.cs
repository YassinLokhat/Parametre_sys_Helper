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
        public new static int Margin = 5;

        public List<Field> Fields;
        private string _Output = "";
        public string Output
        {
            get
            {
                string output = _Output;

                foreach (Field field in Fields)
                    output = output.Replace(field.Key, field.GetOutput());

                return output;
            }
        }

        public TypePanel(string xmlCode) : base()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlType = xmlDocument.FirstChild;

            this.Size = new Size(Margin, 20);

            Fields = new List<Field>();
            this.Text = xmlType.Attributes["name"].Value;
            _Output = xmlType.SelectSingleNode("/type[@name='" + this.Text + "']/text").InnerText;

            foreach (XmlNode fieldNode in xmlType.SelectNodes("/type[@name='" + this.Text + "']/field"))
            {
                Field field = Field.FromXml(fieldNode.OuterXml);
                field.Location = new Point(Margin, this.Size.Height);
                this.Size = new Size(Math.Max(this.Width, Margin + field.Width + Margin), this.Height + field.Height + Margin);
                Fields.Add(field);
                this.Controls.Add(field);
            }

            this.SizeChanged += Field_SizeChanged;
        }

        private void Field_SizeChanged(object sender, EventArgs e)
        {
            foreach (Field field in Fields)
                field.Size = new Size(- Margin + this.Width - Margin, field.Height);
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

        public Field(string xmlCode) : base()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            IsMendatory = xmlField.Attributes["ismendatory"].Value == "true";
            Input = xmlField.Attributes["input"].Value;
            Key = xmlField.Attributes["key"].Value;
            _Output = xmlField.Attributes["output"].Value;
            if (_Output == "")
                _Output = Key;
            Label = xmlField.Attributes["label"].Value;
            label = new Label();
            if (IsMendatory)
                label.Text = Label != "" ? "*" + Label + " :" : "*";
            else
                label.Text = Label != "" ? Label + " :" : "";
            label.Location = new Point(0, 0);
            label.Size = new Size(150, label.Height);
            this.Controls.Add(label);
            this.Size = new Size(label.Width, label.Height);

            this.SizeChanged += Field_SizeChanged;
        }

        protected virtual void Field_SizeChanged(object sender, EventArgs e)
        {
            
        }

        public virtual string GetOutput()
        {
            return _Output;
        }

        public static Field FromXml(string xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            switch (xmlField.Attributes["input"].Value)
            {
                case "textbox":
                    return new TextBoxField(xmlCode);
                case "combobox":
                    return new ComboBoxField(xmlCode);
                case "checkbox":
                    return new CheckBoxField(xmlCode);
                case "radiobox":
                    return new RadioBoxField(xmlCode);
                case "groupbox":
                    return new GroupBoxField(xmlCode);

                default:
                    return new Field(xmlCode);
            }
        }
    }

    public class TextBoxField : Field
    {
        private TextBox textBox = null;

        public TextBoxField(string xmlCode) : base(xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            textBox = new TextBox();
            textBox.Location = new Point(label.Location.X + label.Width + TypePanel.Margin, label.Location.Y);

            try { textBox.Text = xmlField.Attributes["default"].Value; } catch { textBox.Text = ""; }

            this.Controls.Add(textBox);
        }

        public override string GetOutput()
        {
            if (IsMendatory && textBox.Text == "")
                throw new Exception("Le champ \"" + Label + "\" ne peut pas être vide.");
            return textBox.Text != "" ? _Output.Replace(Key, textBox.Text) : "";
        }

        protected override void Field_SizeChanged(object sender, EventArgs e)
        {
            textBox.Size = new Size(this.Width - TypePanel.Margin - label.Width - TypePanel.Margin, textBox.Height);
        }
   }

    public class ComboBoxField : Field
    {
        private ComboBox comboBox = null;

        public ComboBoxField(string xmlCode) : base(xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            comboBox = new ComboBox();

            foreach (XmlNode option in xmlField.ChildNodes)
                comboBox.Items.Add(option.Attributes["text"].Value);

            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox.Location = new Point(label.Location.X + label.Width + TypePanel.Margin, label.Location.Y);

            this.Controls.Add(comboBox);
        }

        public override string GetOutput()
        {
            if (IsMendatory && !comboBox.Items.Contains(comboBox.Text))
                throw new Exception("Le champ \"" + Label + "\" doit être renseigné.");
            return comboBox.Text != "" ? _Output.Replace(Key, comboBox.Text) : "";
        }

        protected override void Field_SizeChanged(object sender, EventArgs e)
        {
            comboBox.Size = new Size(this.Width - TypePanel.Margin - label.Width - TypePanel.Margin, comboBox.Height);
        }
    }

    public class CheckBoxField : Field
    {
        private GroupBox groupBox = null;
        private List<CheckBox> checkBoxes = null;

        public CheckBoxField(string xmlCode) : base(xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            label.Hide();
            groupBox = new GroupBox();
            groupBox.Text = Label;
            groupBox.Location = new Point(label.Location.X, label.Location.Y);
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, 20);

            checkBoxes = new List<CheckBox>();

            foreach (XmlNode option in xmlField.ChildNodes)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = option.Attributes["text"].Value;
                checkBox.Location = new Point(TypePanel.Margin, groupBox.Height);
                checkBox.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, checkBox.Height);
                groupBox.Size = new Size(groupBox.Width, checkBox.Location.Y + checkBox.Height + TypePanel.Margin);
                this.Size = new Size(groupBox.Width, checkBox.Location.Y + checkBox.Height + TypePanel.Margin);

                checkBoxes.Add(checkBox);
                groupBox.Controls.Add(checkBox);
            }

            this.Controls.Add(groupBox);
        }

        public override string GetOutput()
        {
            if (IsMendatory)
                if ((from x in checkBoxes where x.Checked select x).Count() == 0)
                    throw new Exception("Au moins une des options du champ \"" + Label + "\" doit-être selectionnée.");

            string output = "";
            foreach (string c in (from x in checkBoxes where x.Checked select x.Text))
                output += c;

            return _Output.Replace(Key, output);
        }

        protected override void Field_SizeChanged(object sender, EventArgs e)
        {
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, this.Height);
            foreach (CheckBox checkBox in checkBoxes)
                checkBox.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, checkBox.Height);
        }
    }

    public class RadioBoxField : Field
    {
        private GroupBox groupBox = null;
        private List<RadioButton> radioButtons = null;

        public RadioBoxField(string xmlCode) : base(xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            label.Hide();
            groupBox = new GroupBox();
            groupBox.Text = Label;
            groupBox.Location = new Point(label.Location.X, label.Location.Y);
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, 20);

            radioButtons = new List<RadioButton>();

            foreach (XmlNode option in xmlField.ChildNodes)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = option.Attributes["text"].Value;
                radioButton.Location = new Point(TypePanel.Margin, groupBox.Height);
                radioButton.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, radioButton.Height);
                groupBox.Size = new Size(groupBox.Width, radioButton.Location.Y + radioButton.Height + TypePanel.Margin);
                this.Size = new Size(groupBox.Width, radioButton.Location.Y + radioButton.Height + TypePanel.Margin);

                radioButtons.Add(radioButton);
                groupBox.Controls.Add(radioButton);
            }

            this.Controls.Add(groupBox);
        }

        public override string GetOutput()
        {
            if (IsMendatory)
                if ((from x in radioButtons where x.Checked select x).Count() == 0)
                    throw new Exception("Une des options du champ \"" + Label + "\" doit-être selectionnée.");

            string output = (from x in radioButtons where x.Checked select x.Text).FirstOrDefault();

            return _Output.Replace(Key, output);
        }

        protected override void Field_SizeChanged(object sender, EventArgs e)
        {
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, groupBox.Height);
            foreach (RadioButton radioButton in radioButtons)
                radioButton.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, radioButton.Height);
        }
    }

    public class GroupBoxField : Field
    {
        private GroupBox groupBox = null;
        private List<Field> fields = null;

        public GroupBoxField(string xmlCode) : base(xmlCode)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlCode);
            XmlNode xmlField = xmlDocument.FirstChild;

            label.Hide();
            groupBox = new GroupBox();
            groupBox.Text = Label;
            groupBox.Location = new Point(label.Location.X, label.Location.Y);
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, 20);

            fields = new List<Field>();

            foreach (XmlNode f in xmlField.ChildNodes)
            {
                Field field = Field.FromXml(f.OuterXml);
                field.Location = new Point(TypePanel.Margin, groupBox.Height);
                field.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, field.Height);
                groupBox.Size = new Size(groupBox.Width, field.Location.Y + field.Height + TypePanel.Margin);
                this.Size = new Size(groupBox.Width, field.Location.Y + field.Height + TypePanel.Margin);

                fields.Add(field);
                groupBox.Controls.Add(field);
            }

            this.Controls.Add(groupBox);
        }

        public override string GetOutput()
        {
            string output = _Output;
            string tmp = "";
            foreach (Field field in fields)
            {
                tmp += field.GetOutput() == "" ? "0" : "1";
                output = output.Replace(field.Key, field.GetOutput());
            }

            if (tmp.Contains("0") && tmp.Contains("1"))
                throw new Exception("Une ou plusieurs champs du groupe \"" + Label + "\" sont vide.");

            return output;
        }

        protected override void Field_SizeChanged(object sender, EventArgs e)
        {
            groupBox.Size = new Size(-TypePanel.Margin + this.Width - TypePanel.Margin, groupBox.Height);
            foreach (Field field in fields)
                field.Size = new Size(-TypePanel.Margin + groupBox.Width - TypePanel.Margin, field.Height);
        }
    }
}
