using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;

namespace CustomControls.RibbonStyle
{
    public class RibbonButton : Button
    {
        private DispatcherTimer timer1 = new DispatcherTimer();
        private DispatcherTimer timer2 = new DispatcherTimer();
        private Image _img_on;
        private Image _img_click;
        private Image _img_back;
        private Image _img;
        private Image _img_fad;
        private string s_folder;
        private string s_filename;
        private string _infotitle = "";
        private string _infocomment = "";
        private string _infoimage = "";
        private Color _infocolor = Color.FromArgb(201, 217, 239);
        private Color _TextColor = Color.Black;
        private Image _toshow;
        private bool b_fad = false;
        private int i_fad = 0;
        private int i_value = (int)byte.MaxValue;
        private InfoWindow info;
        private int t = 0;
        private int t_end = 100;

        public RibbonButton()
        {
            this._toshow = this._img_back;
            this.timer1.Interval = new TimeSpan(0, 0, 10);
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.timer2.Interval = new TimeSpan(0, 0, 10);
            this.timer2.Tick += new EventHandler(this.timer2_Tick);
        }

        public Image img_on
        {
            get => this._img_on;
            set => this._img_on = value;
        }

        public Image img_click
        {
            get => this._img_click;
            set => this._img_click = value;
        }

        public Image img_back
        {
            get => this._img_back;
            set => this._img_back = value;
        }

        public Image img
        {
            get => this._img;
            set
            {
                this._img = value;
                //this.Image = this._img;
            }
        }

        public string folder
        {
            get => this.s_folder;
            set
            {
                if (value == null)
                    return;
                this.s_folder = value[value.Length - 1] == '\\' ? value : value + "\\";
            }
        }

        public string filename
        {
            get => this.s_filename;
            set
            {
                this.s_filename = value;
                if (!(this.s_folder != null & this.s_filename != null))
                    return;
                this._img = System.Drawing.Image.FromFile(this.s_folder + this.s_filename);
                //this.Image = this._img;
            }
        }

        public string InfoTitle
        {
            get => this._infotitle;
            set => this._infotitle = value;
        }

        public string InfoComment
        {
            get => this._infocomment;
            set => this._infocomment = value;
        }

        public string InfoImage
        {
            get => this._infoimage;
            set => this._infoimage = value;
        }

        public Color InfoColor
        {
            get => this._infocolor;
            set => this._infocolor = value;
        }

        public void PaintBackground()
        {
            if (!this.b_fad)
                return;
            object obj = new object();
            if (this.i_fad == 1)
                obj = this._img_on.Clone();
            else if (this.i_fad == 2)
                obj = this._img_back.Clone();
            this._img_fad = (Image)obj;
            Graphics.FromImage(this._img_fad).FillRectangle((Brush)new SolidBrush(Color.FromArgb(this.i_value, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue)), 0, 0, this._img_fad.Width, this._img_fad.Height);
            //this.Background = this._img_fad;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.t < this.t_end)
            {
                ++this.t;
            }
            else
            {
                this.timer2.Stop();
                this.t = 0;
                this.ShowInfo();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.i_fad)
            {
                case 1:
                    if (this.i_value == 0)
                        this.i_value = (int)byte.MaxValue;
                    if (this.i_value > -1)
                    {
                        this.PaintBackground();
                        this.i_value -= 10;
                        break;
                    }
                    this.i_value = 0;
                    this.PaintBackground();
                    this.timer1.Stop();
                    break;

                case 2:
                    if (this.i_value == 0)
                        this.i_value = (int)byte.MaxValue;
                    if (this.i_value > -1)
                    {
                        this.PaintBackground();
                        this.i_value -= 10;
                        break;
                    }
                    this.i_value = 0;
                    this.PaintBackground();
                    this.timer1.Stop();
                    break;
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (this.b_fad)
            {
                this.i_fad = 1;
                this.timer1.Start();
            }
            else
            {
                //this.BackgroundImage = this._img_on;
                this._toshow = this._img_on;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.b_fad)
            {
                this.i_fad = 2;
                this.timer1.Start();
            }
            else
            {
                //this.BackgroundImage = this._img_back;
                this._toshow = this._img_back;
            }
            if (this.info != null)
                this.info.Close();
            this.timer2.Stop();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs mevent)
        {
            //this.BackgroundImage = this._img_click;
            this._toshow = this._img_click;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseButtonEventArgs mevent)
        {
            //this.BackgroundImage = this._img_on;
            this._toshow = this._img_on;
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.timer2.Start();
            base.OnMouseMove(e);
        }

        public void ShowInfo()
        {
            this.info = new InfoWindow();
            this.info.Title = this._infotitle;
            this.info.Comment = this._infocomment;
            this.info.Picture = this._infoimage;
            this.info.FillColor = this._infocolor;
            /*if (this.GetInfoLocation() == RibbonButton.Side.UpLeft)
                this.info.Location = new Point(Cursor.Position.X, Application.OpenForms[0].Location.Y + this.Bottom + 80);
            else
                this.info.Location = new Point(Cursor.Position.X - this.info.Width, Application.OpenForms[0].Location.Y + this.Bottom + 80);
            */
            this.info.Show();
        }

        public RibbonButton.Side GetInfoLocation()
        {
            /*Point point = Cursor.Position;
            int x1 = point.X;
            point = Application.OpenForms[0].Location;
            int x2 = point.X;
            return x1 - x2 < Application.OpenForms[0].Width / 2 ? RibbonButton.Side.UpLeft : RibbonButton.Side.UpRight;
        */
            return Side.DownLeft;
        }

        public enum Side
        {
            UpLeft,
            UpRight,
            DownLeft,
            DownRight,
        }
    }
}
