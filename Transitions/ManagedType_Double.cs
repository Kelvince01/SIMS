using System;

namespace Transitions
{
    internal class ManagedType_Double : IManagedType
    {
        public Type getManagedType() => typeof(double);

        public object copy(object o) => (object)(double)o;

        public object getIntermediateValue(object start, object end, double dPercentage) => (object)Utility.interpolate((double)start, (double)end, dPercentage);
    }
}
