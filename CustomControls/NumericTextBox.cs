using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace CustomControls
{
    [ToolboxBitmap(typeof(TextBox))]
    [Serializable]
    public class NumericTextBox : TextBox
    {
        private const int m_MaxDecimalLength = 10;
        private const int m_MaxValueLength = 27;
        private const int WM_CHAR = 258;
        private const int WM_CUT = 768;
        private const int WM_COPY = 769;
        private const int WM_PASTE = 770;
        private const int WM_CLEAR = 771;
        private int m_decimalLength = 0;
        private bool m_allowNegative = true;
        private string m_valueFormatStr = string.Empty;
        private char m_decimalSeparator = '.';
        private char m_negativeSign = '-';

        public NumericTextBox()
        {
            this.TextWrapping = TextWrapping.NoWrap;
            this.TextAlignment = TextAlignment.Right;
            base.Text = "0";
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            this.m_decimalSeparator = currentCulture.NumberFormat.CurrencyDecimalSeparator[0];
            this.m_negativeSign = currentCulture.NumberFormat.NegativeSign[0];
            //this.SetValueFormatStr();
            // this.BorderStyle = BorderStyle.FixedSingle;
            //this.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            //this.Location = new Point(0, 0);
            //this.Size = new Size(100, 24);
        }

        [DefaultValue("0")]
        public string Text
        {
            get => base.Text;
            set
            {
                Decimal result;
                if (Decimal.TryParse(value, out result))
                    base.Text = result.ToString(this.m_valueFormatStr);
                else
                    base.Text = 0.ToString(this.m_valueFormatStr);
            }
        }
    }
}
