using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControls
{
    public class VTextBox : TextBox
    {
        public VTextBox()
        {
            this.FontFamily = new FontFamily("Microsoft Sans Serif");
            this.FontSize = 11.25f;
            this.FontStyle = FontStyles.Normal;
            this.Width = 100;
            this.Height = 24;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            if (!this.IsReadOnly)
                this.Background = new SolidColorBrush(Colors.LightCyan);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (!this.IsReadOnly)
                this.Background = new SolidColorBrush(Colors.White);
            base.OnLostFocus(e);
        }
    }
}
