using System;
using System.Drawing;

namespace Transitions
{
    internal class ManagedType_Color : IManagedType
    {
        public Type getManagedType() => typeof(Color);

        public object copy(object o) => (object)Color.FromArgb(((Color)o).ToArgb());

        public object getIntermediateValue(object start, object end, double dPercentage)
        {
            Color color1 = (Color)start;
            Color color2 = (Color)end;
            int r1 = (int)color1.R;
            int g1 = (int)color1.G;
            int b1 = (int)color1.B;
            int a1 = (int)color1.A;
            int r2 = (int)color2.R;
            int g2 = (int)color2.G;
            int b2 = (int)color2.B;
            int a2 = (int)color2.A;
            int i2 = r2;
            double dPercentage1 = dPercentage;
            int red = Utility.interpolate(r1, i2, dPercentage1);
            int green = Utility.interpolate(g1, g2, dPercentage);
            int blue = Utility.interpolate(b1, b2, dPercentage);
            return (object)Color.FromArgb(Utility.interpolate(a1, a2, dPercentage), red, green, blue);
        }
    }
}
