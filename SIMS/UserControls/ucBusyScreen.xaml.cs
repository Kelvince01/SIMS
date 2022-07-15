using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using CustomControls.RibbonStyle;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for ucBusyScreen.xaml
    /// </summary>
    public partial class ucBusyScreen : UserControl
    {
        public bool drag = false;
        public bool enab = false;
        private int m_opacity = 100;
        private int alpha;

        public ucBusyScreen()
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
            InitializeComponent();
        }

        public int Opacity
        {
            get
            {
                if (this.m_opacity > 100)
                    this.m_opacity = 100;
                else if (this.m_opacity < 1)
                    this.m_opacity = 1;
                return this.m_opacity;
            }
            set
            {
                this.m_opacity = value;
                if (this.Parent == null)
                    return;
                //this.Parent.Invalidate(this.Bounds, true);
            }
        }

        protected override void OnRender(DrawingContext e)
        {
            /*Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Color backColor = this.Parent.BackColor;
            this.alpha = this.m_opacity * (int)byte.MaxValue / 100;
            Brush brush;
            if (this.drag)
            {
                Color color = new Color();
                Color baseColor = !(this.BackColor != Color.Transparent) ? backColor : Color.FromArgb((int)this.BackColor.R * this.alpha / (int)byte.MaxValue + (int)backColor.R * ((int)byte.MaxValue - this.alpha) / (int)byte.MaxValue, (int)this.BackColor.G * this.alpha / (int)byte.MaxValue + (int)backColor.G * ((int)byte.MaxValue - this.alpha) / (int)byte.MaxValue, (int)this.BackColor.B * this.alpha / (int)byte.MaxValue + (int)backColor.B * ((int)byte.MaxValue - this.alpha) / (int)byte.MaxValue);
                this.alpha = (int)byte.MaxValue;
                brush = (Brush)new SolidBrush(Color.FromArgb(this.alpha, baseColor));
            }
            else
                brush = (Brush)new SolidBrush(Color.FromArgb(this.alpha, this.BackColor));
            if (this.BackColor != Color.Transparent | this.drag)
                graphics.FillRectangle(brush, rect);
            brush.Dispose();
            graphics.Dispose();
            base.OnPaint(e);*/
            base.OnRender(e);
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
    }
}
