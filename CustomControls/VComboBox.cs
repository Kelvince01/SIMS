using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CustomControls
{
    public class VComboBox : ComboBox
    {
        private bool _limitToList = true;
        private bool _inEditMode = false;

        public event CancelEventHandler NotInList;

        static VComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VComboBox), new FrameworkPropertyMetadata(typeof(VComboBox)));
        }

        public VComboBox()
        {
            this.IsEditable = true;
            //TextBoxBase.TextChanged += new TextChangedEventHandler(VComboBox_TextChanged);
            this.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                      new System.Windows.Controls.TextChangedEventHandler(VComboBox_TextChanged));
        }

        private void VComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._inEditMode)
            {
                //string text = this.Text;
                //int num = this.FindString(text);
                //if (num >= 0)
                //{
                //    this._inEditMode = false;
                //    this.SelectedIndex = num;
                //    this._inEditMode = true;
                //    this.Select(text.Length, this.Text.Length);
                //}
            }
        }

        [Category("Behavior")]
        public bool LimitToList
        {
            get => this._limitToList;
            set => this._limitToList = value;
        }

        protected virtual void OnNotInList(CancelEventArgs e)
        {
            if (this.NotInList == null)
                return;
            this.NotInList((object)this, e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this._inEditMode = e.Key != Key.Back && e.Key != Key.Delete;
            base.OnKeyDown(e);
        }
    }
}
