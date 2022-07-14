using System;
using System.Text;

namespace CustomControls.RibbonStyle
{
    /// <summary>Encapsulates the information needed when creating a control.</summary>
    public class CreateParams
    {
        private string className;
        private string caption;
        private int style;
        private int exStyle;
        private int classStyle;
        private int x;
        private int y;
        private int width;
        private int height;
        private IntPtr parent;
        private object param;

        /// <summary>Gets or sets the name of the Windows class to derive the control from.</summary>
        /// <returns>The name of the Windows class to derive the control from.</returns>
        public string ClassName
        {
            get => this.className;
            set => this.className = value;
        }

        /// <summary>Gets or sets the control's initial text.</summary>
        /// <returns>The control's initial text.</returns>
        public string Caption
        {
            get => this.caption;
            set => this.caption = value;
        }

        /// <summary>Gets or sets a bitwise combination of window style values.</summary>
        /// <returns>A bitwise combination of the window style values.</returns>
        public int Style
        {
            get => this.style;
            set => this.style = value;
        }

        /// <summary>Gets or sets a bitwise combination of extended window style values.</summary>
        /// <returns>A bitwise combination of the extended window style values.</returns>
        public int ExStyle
        {
            get => this.exStyle;
            set => this.exStyle = value;
        }

        /// <summary>Gets or sets a bitwise combination of class style values.</summary>
        /// <returns>A bitwise combination of the class style values.</returns>
        public int ClassStyle
        {
            get => this.classStyle;
            set => this.classStyle = value;
        }

        /// <summary>Gets or sets the initial left position of the control.</summary>
        /// <returns>The numeric value that represents the initial left position of the control.</returns>
        public int X
        {
            get => this.x;
            set => this.x = value;
        }

        /// <summary>Gets or sets the top position of the initial location of the control.</summary>
        /// <returns>The numeric value that represents the top position of the initial location of the control.</returns>
        public int Y
        {
            get => this.y;
            set => this.y = value;
        }

        /// <summary>Gets or sets the initial width of the control.</summary>
        /// <returns>The numeric value that represents the initial width of the control.</returns>
        public int Width
        {
            get => this.width;
            set => this.width = value;
        }

        /// <summary>Gets or sets the initial height of the control.</summary>
        /// <returns>The numeric value that represents the initial height of the control.</returns>
        public int Height
        {
            get => this.height;
            set => this.height = value;
        }

        /// <summary>Gets or sets the control's parent.</summary>
        /// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle of the control's parent.</returns>
        public IntPtr Parent
        {
            get => this.parent;
            set => this.parent = value;
        }

        /// <summary>Gets or sets additional parameter information needed to create the control.</summary>
        /// <returns>The <see cref="T:System.Object" /> that holds additional parameter information needed to create the control.</returns>
        public object Param
        {
            get => this.param;
            set => this.param = value;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(64);
            stringBuilder.Append("CreateParams {'");
            stringBuilder.Append(this.className);
            stringBuilder.Append("', '");
            stringBuilder.Append(this.caption);
            stringBuilder.Append("', 0x");
            stringBuilder.Append(Convert.ToString(this.style, 16));
            stringBuilder.Append(", 0x");
            stringBuilder.Append(Convert.ToString(this.exStyle, 16));
            stringBuilder.Append(", {");
            stringBuilder.Append(this.x);
            stringBuilder.Append(", ");
            stringBuilder.Append(this.y);
            stringBuilder.Append(", ");
            stringBuilder.Append(this.width);
            stringBuilder.Append(", ");
            stringBuilder.Append(this.height);
            stringBuilder.Append("}");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
