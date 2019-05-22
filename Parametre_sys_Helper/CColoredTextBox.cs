using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Parametre_sys_Helper
{
    public partial class CColoredTextBox : FastColoredTextBox
    {
        Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

        private Style sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));
        Style KeywordsStyle = new TextStyle(Brushes.Green, null, FontStyle.Bold);
        Style OperatorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        Style SeparatorStyle = new TextStyle(Brushes.Blue, null, FontStyle.Italic);
        Style StringStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

        public CColoredTextBox() : base()
        {
            this.Font = new Font("Consolas", 9.75f);
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.LeftPadding = 17;
            this.Language = Language.Custom;
            this.AddStyle(KeywordsStyle);//same words style
            this.Tag = new TbInfo();
            this.DelayedTextChangedInterval = 1000;
            this.DelayedEventsInterval = 500;
            this.ChangedLineColor = changedLineColor;
            this.CurrentLineColor = currentLineColor;
            this.ShowFoldingLines = true;
            this.HighlightingRangeType = HighlightingRangeType.VisibleRange;
            this.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctb_TextChangedDelayed);
        }

        private void fctb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            this.Range.SetStyle(KeywordsStyle, @"\b(L'enregistrement|mnemonique|type de fichiers parametres|L'interface|La balise|famille|longueur|description|pointeur|taille|cle|constante|defini par la constante|la longueur de l enregistrement|offset|type|ne fournira pas de fonction|format|utilise la structure existante|fieldssize)\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            this.Range.SetStyle(OperatorStyle, "[{}()]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            this.Range.SetStyle(OperatorStyle, @"\b(Numeric|Text|Date|Hour|Buffer|Subrecord)\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            this.Range.SetStyle(SeparatorStyle, "( et |,)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            this.Range.SetStyle(StringStyle, "\"([^\"]*)\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }

    public class InvisibleCharsRenderer : Style
    {
        Pen pen;

        public InvisibleCharsRenderer(Pen pen)
        {
            this.pen = pen;
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            var tb = range.tb;
            using (Brush brush = new SolidBrush(pen.Color))
                foreach (var place in range)
                {
                    switch (tb[place].c)
                    {
                        case ' ':
                            var point = tb.PlaceToPoint(place);
                            point.Offset(tb.CharWidth / 2, tb.CharHeight / 2);
                            gr.DrawLine(pen, point.X, point.Y, point.X + 1, point.Y);
                            break;
                    }

                    if (tb[place.iLine].Count - 1 == place.iChar)
                    {
                        var point = tb.PlaceToPoint(place);
                        point.Offset(tb.CharWidth, 0);
                        gr.DrawString("¶", tb.Font, brush, point);
                    }
                }
        }
    }

    public class TbInfo
    {
        public AutocompleteMenu popupMenu;
    }
}
