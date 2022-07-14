using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Transitions;

namespace CustomControls
{
    public class MetroLogOffPanel : UserControl
    {
        private Tile tileUser;
        private Tile btnExit;
        private Panel panel1;
        private Panel panel2;
        private Tile btnLogOff;
        private Tile btnSettingsAccount;
        private Tile btnThemeSettings;

        public MetroLogOffPanel()
        {
        }

        /// <summary>Gets or sets the text associated with this control.</summary>
        /// <returns>The text associated with this control.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Text
        {
            get => (string)base.Content;
            set => base.Content = value;
        }

        public void SetToInitStage() => this.Height = 42;

        public event MetroLogOffPanel.afterTileButtonClick onTileButtonClick;

        private void tileUser_Click(object sender, EventArgs e)
        {
            this.ToggleSlide();
            if (this.onTileButtonClick == null)
                return;
            this.onTileButtonClick(sender, e);
        }

        private void ToggleSlide()
        {
            if (this.Height == 42)
                Transition.run((object)this, "Height", (object)166, (ITransitionType)new TransitionType_Linear(200));
            else
                Transition.run((object)this, "Height", (object)42, (ITransitionType)new TransitionType_Linear(200));
        }

        public void SetUser(string userName) => (this.tileUser).Content = "Welcome " + userName;

        private void MetroLogOffPanel_Leave(object sender, EventArgs e) => this.SetToInitStage();

        public event MetroLogOffPanel.afterExitButtonClick onExitButtonClick;

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (this.onExitButtonClick == null)
                return;
            this.onExitButtonClick(sender, e);
        }

        public event MetroLogOffPanel.afterThemeButtonClick onThemeButtonClick;

        private void btnThemeSettings_Click(object sender, EventArgs e)
        {
            this.SetToInitStage();
            if (this.onThemeButtonClick == null)
                return;
            this.onThemeButtonClick(sender, e);
        }

        public event MetroLogOffPanel.afterAccountSettingsButtonClick onAccountSettingsButtonClick;

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.SetToInitStage();
            if (this.onAccountSettingsButtonClick == null)
                return;
            this.onAccountSettingsButtonClick(sender, e);
        }

        public event MetroLogOffPanel.afterLogOffButtonClick onLoggOffButtonClick;

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            this.SetToInitStage();
            if (this.onLoggOffButtonClick == null)
                return;
            this.onLoggOffButtonClick(sender, e);
        }

        public delegate void afterTileButtonClick(object sender, EventArgs e);

        public delegate void afterExitButtonClick(object sender, EventArgs e);

        public delegate void afterThemeButtonClick(object sender, EventArgs e);

        public delegate void afterAccountSettingsButtonClick(object sender, EventArgs e);

        public delegate void afterLogOffButtonClick(object sender, EventArgs e);
    }
}
