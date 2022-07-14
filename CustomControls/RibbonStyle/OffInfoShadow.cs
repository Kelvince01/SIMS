using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;

namespace CustomControls.RibbonStyle
{
    public class OffInfoShadow : Window
    {
        private int X0;
        private int XF;
        private int Y0;
        private int YF;
        private int T = 2;
        private int i_Zero = 180;
        private int i_Sweep = 90;
        private int X;
        private int Y;
        private GraphicsPath path;
        private int D = -1;

        public OffInfoShadow()
        {
            this.ShowInTaskbar = false;
            //this.Background = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            //this.TransparencyKey = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            this.Opacity = 0.5;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        protected CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = this.CreateParams;
                createParams.ExStyle |= 32;
                return createParams;
            }
        }

        public void DrawArc()
        {
            this.X = this.X0;
            this.Y = this.Y0;
            this.i_Zero = 180;
            ++this.D;
            this.path = new GraphicsPath();
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X += this.XF;
            this.path.AddArc(this.X - this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y += this.YF;
            this.path.AddArc(this.X - this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X -= this.XF;
            this.path.AddArc(this.X + this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y -= this.YF;
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
        }
    }
}
