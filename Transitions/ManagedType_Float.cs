using System;

namespace Transitions
{
    internal class ManagedType_Float : IManagedType
    {
        public Type getManagedType() => typeof(float);

        public object copy(object o) => (object)(float)o;

        public object getIntermediateValue(object start, object end, double dPercentage) => (object)Utility.interpolate((float)start, (float)end, dPercentage);
    }
}
