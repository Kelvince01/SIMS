using System;
using System.Windows;

namespace CustomControls
{
    /// <summary>
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        private int options;
        private int[] customColors;
        private System.Drawing.Color color;

        public ColorDialog()
        {
            InitializeComponent();
            this.customColors = new int[16];
            this.Reset();
        }

        /// <summary>Gets or sets the color selected by the user.</summary>
        /// <returns>The color selected by the user. If a color is not selected, the default value is black.</returns>
        public System.Drawing.Color Color
        {
            get => this.color;
            set
            {
                if (!value.IsEmpty)
                    this.color = value;
                else
                    this.color = System.Drawing.Color.Black;
            }
        }

        /// <summary>Gets or sets the set of custom colors shown in the dialog box.</summary>
        /// <returns>A set of custom colors shown by the dialog box. The default value is <see langword="null" />.</returns>
        public int[] CustomColors
        {
            get => (int[])this.customColors.Clone();
            set
            {
                int length = value == null ? 0 : Math.Min(value.Length, 16);
                if (length > 0)
                    Array.Copy((Array)value, 0, (Array)this.customColors, 0, length);
                for (int index = length; index < 16; ++index)
                    this.customColors[index] = 16777215;
            }
        }

        private void ResetColor() => this.Color = System.Drawing.Color.Black;

        /// <summary>Resets all options to their default values, the last selected color to black, and the custom colors to their default values.</summary>
        public void Reset()
        {
            this.options = 0;
            this.color = System.Drawing.Color.Black;
            this.CustomColors = (int[])null;
        }

        private void SetOption(int option, bool value)
        {
            if (value)
                this.options |= option;
            else
                this.options &= ~option;
        }

        private bool ShouldSerializeColor() => !this.Color.Equals((object)System.Drawing.Color.Black);

        /// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ColorDialog" />.</summary>
        /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ColorDialog" />.</returns>
        public override string ToString() => base.ToString() + ",  Color: " + this.Color.ToString();
    }
}
