using System.ComponentModel;
using System.Windows.Controls;

namespace CustomControls
{
    public class VComboBox : ComboBox
    {
        private bool _limitToList = true;
        private bool _inEditMode = false;

        public event CancelEventHandler NotInList;

        public VComboBox()
        {
        }
    }
}
