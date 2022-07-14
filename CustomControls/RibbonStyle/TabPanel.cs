using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Brush = System.Windows.Media.Brush;
using Color = System.Drawing.Color;
using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;
using Pen = System.Windows.Media.Pen;
using Point = System.Windows.Point;

namespace CustomControls.RibbonStyle
{
    public class TabPanel : Panel
    {
        private int X0;
        private int XF;
        private int Y0;
        private int YF;
        private int T = 8;
        private int i_Zero = 180;
        private int i_Sweep = 90;
        private int X;
        private int Y;
        private GraphicsPath path;
        private int D = -1;
        private int R0 = 215;
        private int G0 = 227;
        private int B0 = 242;
        private Color _BaseColor = Color.FromArgb(215, 227, 242);
        private Color _BaseColorOn = Color.FromArgb(215, 227, 242);
        private int i_mode = 0;
        private int i_factor = 1;
        private int i_fR = 1;
        private int i_fG = 1;
        private int i_fB = 1;
        private int i_Op = (int)byte.MaxValue;
        private string S_TXT = "";
        private DispatcherTimer timer1 = new DispatcherTimer();

        public TabPanel()
        {
            this.timer1.Interval = new TimeSpan(0, 0, 1);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
        }

        public Color BaseColor
        {
            get => this._BaseColor;
            set
            {
                this._BaseColor = value;
                this.R0 = (int)this._BaseColor.R;
                this.B0 = (int)this._BaseColor.B;
                this.G0 = (int)this._BaseColor.G;
            }
        }

        public Color BaseColorOn
        {
            get => this._BaseColorOn;
            set
            {
                this._BaseColorOn = value;
                this.R0 = (int)this._BaseColor.R;
                this.B0 = (int)this._BaseColor.B;
                this.G0 = (int)this._BaseColor.G;
            }
        }

        public string Caption
        {
            get => this.S_TXT;
            set
            {
                this.S_TXT = value;
                //this.Refresh();
            }
        }

        public int Speed
        {
            get => this.i_factor;
            set => this.i_factor = value;
        }

        public int Opacity
        {
            get => this.i_Op;
            set
            {
                if (!(value < 256 | value > -1))
                    return;
                this.i_Op = value;
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
            this.X += this.XF - 6;
            this.path.AddArc(this.X - this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y += this.YF - 6;
            this.path.AddArc(this.X - this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X -= this.XF - 6;
            this.path.AddArc(this.X + this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y -= this.YF - 6;
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
        }

        public void DrawArc2(int OF_Y, int SW_Y)
        {
            this.X = this.X0 + 4;
            this.Y = this.Y0 + OF_Y;
            this.i_Zero = 180;
            this.path = new GraphicsPath();
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X += this.XF - 8;
            this.path.AddArc(this.X - this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y += SW_Y;
            this.path.AddArc(this.X - this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X -= this.XF - 8;
            this.path.AddArc(this.X + this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y -= SW_Y;
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
        }

        public void DrawArc3(int OF_Y, int SW_Y)
        {
            this.X = this.X0;
            this.Y = this.Y0 + OF_Y;
            this.i_Zero = 180;
            this.path = new GraphicsPath();
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X += this.XF;
            this.path.AddArc(this.X - this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y += SW_Y;
            this.path.AddArc(this.X - this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.X -= this.XF;
            this.path.AddArc(this.X + this.D, this.Y - this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
            this.i_Zero += 90;
            this.Y -= SW_Y;
            this.path.AddArc(this.X + this.D, this.Y + this.D, this.T, this.T, (float)this.i_Zero, (float)this.i_Sweep);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            Point client = this.PointToScreen(Mouse.GetPosition(this));
            if (client.X > 0 | client.X < this.Width | client.Y > 0 | client.Y < this.Height)
            {
                this.i_mode = 0;
                this.timer1.Start();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            Point client = this.PointToScreen(Mouse.GetPosition(this));
            if (client.X < 0 | client.X >= this.Width | client.Y < 0 | client.Y >= this.Height)
            {
                this.i_mode = 1;
                this.timer1.Start();
            }

            base.OnMouseLeave(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.i_mode == 0)
            {
                this.i_fR = Math.Abs((int)this._BaseColorOn.R - this.R0) <= this.i_factor ? 1 : this.i_factor;
                this.i_fG = Math.Abs((int)this._BaseColorOn.G - this.G0) <= this.i_factor ? 1 : this.i_factor;
                this.i_fB = Math.Abs((int)this._BaseColorOn.B - this.B0) <= this.i_factor ? 1 : this.i_factor;
                if ((int)this._BaseColorOn.R < this.R0)
                    this.R0 -= this.i_fR;
                else if ((int)this._BaseColorOn.R > this.R0)
                    this.R0 += this.i_fR;
                if ((int)this._BaseColorOn.G < this.G0)
                    this.G0 -= this.i_fG;
                else if ((int)this._BaseColorOn.G > this.G0)
                    this.G0 += this.i_fG;
                if ((int)this._BaseColorOn.B < this.B0)
                    this.B0 -= this.i_fB;
                else if ((int)this._BaseColorOn.B > this.B0)
                    this.B0 += this.i_fB;
                if (this._BaseColorOn == Color.FromArgb(this.R0, this.G0, this.B0))
                    this.timer1.Stop();
                else
                    this.UpdateLayout();
                //this.Refresh();
            }
            if (this.i_mode != 1)
                return;
            this.i_fR = Math.Abs((int)this._BaseColor.R - this.R0) >= this.i_factor ? this.i_factor : 1;
            this.i_fG = Math.Abs((int)this._BaseColor.G - this.G0) >= this.i_factor ? this.i_factor : 1;
            this.i_fB = Math.Abs((int)this._BaseColor.B - this.B0) >= this.i_factor ? this.i_factor : 1;
            if ((int)this._BaseColor.R < this.R0)
                this.R0 -= this.i_fR;
            else if ((int)this._BaseColor.R > this.R0)
                this.R0 += this.i_fR;
            if ((int)this._BaseColor.G < this.G0)
                this.G0 -= this.i_fG;
            else if ((int)this._BaseColor.G > this.G0)
                this.G0 += this.i_fG;
            if ((int)this._BaseColor.B < this.B0)
                this.B0 -= this.i_fB;
            else if ((int)this._BaseColor.B > this.B0)
                this.B0 += this.i_fB;
            if (this._BaseColor == Color.FromArgb(this.R0, this.G0, this.B0))
                this.timer1.Stop();
            else
                this.UpdateLayout();
            //this.Refresh();
        }

        protected override void OnRender(DrawingContext e)
        {
            /*this.X0 = 0;
            this.XF = this.Width + this.X0 - 3;
            this.Y0 = 0;
            this.YF = this.Height + this.Y0 - 3;
            Point point1 = new Point(this.X0, this.Y0);
            Point point2 = new Point(this.X0, this.Y0 + this.YF);
            Pen pen1 = new Pen(System.Windows.Media.Color.FromArgb(this.i_Op, this.R0 - 18, this.G0 - 17, this.B0 - 19), 1);
            Pen pen2 = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromArgb(this.i_Op, this.R0 - 39, this.G0 - 24, this.B0 - 3)), 1);
            Pen pen3 = new Pen(Color.FromArgb(this.i_Op, this.R0 + 11, this.G0 + 9, this.B0 + 3));
            Pen pen4 = new Pen(Color.FromArgb(this.i_Op, this.R0 - 8, this.G0 - 4, this.B0 - 2));
            Pen pen5 = new Pen(Color.FromArgb(this.i_Op, this.R0, this.G0, this.B0));
            Pen pen6 = new Pen(Color.FromArgb(this.i_Op, this.R0 - 16, this.G0 - 11, this.B0 - 5));
            Pen pen7 = new Pen(Color.FromArgb(this.i_Op, this.R0 + 1, this.G0 + 5, this.B0 + 3));
            Pen pen8 = new Pen(Color.FromArgb(this.i_Op, this.R0 - 22, this.G0 - 10, this.B0));
            this.T = 1;
            this.DrawArc3(0, 20);
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            Brush brush = pen4.Brush;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            this.X = this.X0;
            this.Y = this.Y0;
            this.i_Zero = 180;
            this.D = 0;
            e.Graphics.FillPath(pen5.Brush, this.path);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(point1, point2, pen6.Color, pen7.Color);
            this.DrawArc2(15, this.YF - 20);
            e.Graphics.FillPath(linearGradientBrush, this.path);
            this.DrawArc2(this.YF - 16, 12);
            Pen pen9 = new Pen(Color.FromArgb(this.i_Op, this.R0 - 22, this.G0 - 11, this.B0));
            e.Graphics.FillPath(pen9.Brush, this.path);
            this.T = 6;
            this.DrawArc();
            this.DrawArc();
            e.Graphics.DrawPath(pen2, this.path);
            this.DrawArc();
            e.Graphics.DrawPath(pen3, this.path);
            this.PointToScreen(Mouse.GetPosition(this));
            PointF point = new PointF((float)(10 + this.Width / 2 - this.S_TXT.Length * (int)this.Font.Size / 2), (float)(this.Height - 20));
            Pen pen10 = new Pen();
            e.Graphics.DrawString(this.S_TXT, this.Font, pen10.Brush, point);*/
            base.OnRender(e);
        }
    }
}
