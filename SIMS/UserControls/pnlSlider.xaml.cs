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
        private MetroContentControl _owner = (MetroContentControl)null;

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

        /*public pnlSlider(Window owner)
            : this()
        {
            this.Visible = false;
            this._owner = owner;
            owner.Controls.Add((Control)this);
            this.BringToFront();
            owner.Resize += new EventHandler(this.owner_Resize);
            this.Click += new EventHandler(this.pnlSlider_Click);
            this.ResizeForm();
        }

        private void pnlSlider_Click(object sender, EventArgs e)
        {
        }

        private void ResizeForm()
        {
            this.Width = this._owner.Width;
            this.Height = this._owner.Height - 40;
            this.Location = new Point(this._loaded ? 0 : this._owner.Width, 0);
        }

        public void swipe(bool show = true)
        {
            this.Visible = true;
            Transition transition = new Transition((ITransitionType)new TransitionType_EaseInEaseOut(500));
            transition.add((object)this, "Left", (object)(show ? 0 : this.Width));
            transition.run();
            while (this.Left != (show ? 0 : this.Width))
                Application.DoEvents();
            if (!show)
            {
                this.closed(new EventArgs());
                this._owner.Resize -= new EventHandler(this.owner_Resize);
                this._owner.Controls.Remove((Control)this);
                this.Dispose();
            }
            else
            {
                this._loaded = true;
                this.ResizeForm();
                this.shown(new EventArgs());
            }
        }*/
    }
}
