using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Office.Interop.Excel;
using Window = System.Windows.Window;

namespace CustomControls.RibbonStyle
{
    public class InfoWindow : Window
    {
        private string _title = "";
        private string _comment = "";
        private string _picture = "";
        private Color _fillcolor;
        private Image img;
        private int XC = 8;
        private int YC = 20;
        private int WC = 220;
        private int HC = 90;
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

        //private RibbonPanel ribbonPanel1;
        private TabPanel tabPanel1;

        //private TabStrip tabStrip1;
        private Tab tab1;

        //private TabStripPage tabStripPage1;
        private Tab tab2;

        //private TabStripPage tabStripPage2;
        //private TabPageSwitcher tabPageSwitcher1;
        private OffInfoShadow shadow;

        private DispatcherTimer timer = new DispatcherTimer();
        private int ms = 300;
        private int j = 10;
        private bool appearing = true;
        private Pen p = new Pen(Color.Black, 8f);
        private Brush b = (Brush)new SolidBrush(Color.FromArgb(160, 0, (int)byte.MaxValue, 0));

        public InfoWindow()
        {
            //this.Background = Color.Fuchsia;
            //this.TransparencyKey = Color.Fuchsia;
            this.ShowInTaskbar = false;
            this.ShowInTaskbar = false;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 10;
            this.Height = 10;
            this.Opacity = 0.8;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.timer.Interval = new TimeSpan(0, 0, 5);
            this.timer.Tick += new EventHandler(this.timer_Tick);
        }

        public string Title
        {
            get => this._title;
            set
            {
                //this.Size = new Size(150, 30);
                this._title = value;
            }
        }

        public string Comment
        {
            get => this._comment;
            set
            {
                if (!(value != ""))
                    return;
                //this.Size = new Size(240, 100);
                this._comment = value;
            }
        }

        public string Picture
        {
            get => this._picture;
            set
            {
                if (!(value != ""))
                    return;
                //this.Size = new Size(380, 180);
                this.XC = 122;
                this.YC = 30;
                this.WC = 240;
                this.HC = 120;
                this._picture = value;
                try
                {
                    this.img = Image.FromFile(this._picture);
                }
                catch
                {
                }
            }
        }

        public Color FillColor
        {
            get => this._fillcolor;
            set => this._fillcolor = value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.appearing)
            {
                if (this.Opacity == 1.0)
                {
                    if (this.j < this.ms)
                        ++this.j;
                    else
                        this.appearing = !this.appearing;
                }
                else
                    this.Opacity += 0.1;
            }
            if (this.appearing)
                return;
            if (this.Opacity == 0.0)
            {
                this.Close();
            }
            else
            {
                this.Opacity -= 0.2;
                this.shadow.Close();
            }
        }

        public new void Close()
        {
            this.appearing = false;
            this.timer.Start();
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
