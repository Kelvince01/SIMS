using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Transitions;

namespace SIMS.UserControls
{
    /// <summary>
    /// Interaction logic for pnlSlider.xaml
    /// </summary>
    public partial class pnlSlider : MetroContentControl
    {
        private Window _owner = (Window)null;
        private bool _loaded = false;

        public event EventHandler Closed;

        public event EventHandler Shown;

        protected virtual void closed(EventArgs e)
        {
            EventHandler closed = this.Closed;
            if (closed == null)
                return;
            closed((object)this, e);
        }

        protected virtual void shown(EventArgs e)
        {
            EventHandler shown = this.Shown;
            if (shown == null)
                return;
            shown((object)this, e);
        }

        public pnlSlider()
        {
            InitializeComponent();
        }

        public pnlSlider(Window owner) : this()
        {
            this.Visibility = Visibility.Hidden;
            this._owner = owner;
            var grid = owner.Content as Grid;
            grid.Children.Add((Control)this);
            this.BringIntoView();
            owner.SizeChanged += new SizeChangedEventHandler(this.owner_Resize);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.pnlSlider_Click);
            this.ResizeForm();
            this.UpdateLayout();
        }

        private void owner_Resize(object sender, SizeChangedEventArgs e)
        {
        }

        private void pnlSlider_Click(object sender, MouseButtonEventArgs e)
        {
        }

        private void ResizeForm()
        {
            this.Width = this._owner.Width;
            this.Height = this._owner.Height - 40;
            this.PointToScreen(new Point(this._loaded ? 0 : this._owner.Width, 0));
        }

        public void swipe(bool show = true)
        {
            this.Visibility = Visibility.Visible;
            Transition transition = new Transition((ITransitionType)new TransitionType_EaseInEaseOut(500));
            transition.add((object)this, "Left", (object)(show ? 0 : this.Width));
            transition.run();
            /*while (this.Left != (show ? 0 : this.Width))
                App.DoEvents();*/
            if (!show)
            {
                this.closed(new EventArgs());
                this._owner.SizeChanged -= new SizeChangedEventHandler(this.owner_Resize);
                var grid = _owner.Content as Grid;
                grid.Children.Remove((Control)this);
            }
            else
            {
                this._loaded = true;
                this.ResizeForm();
                this.shown(new EventArgs());
            }
        }
    }
}
