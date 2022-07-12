using System;

namespace Transitions
{
    internal class ManagedType_Int : IManagedType
    {
        public Type getManagedType() => typeof(int);

        public object copy(object o) => (object)(int)o;

        public object getIntermediateValue(object start, object end, double dPercentage) => (object)Utility.interpolate((int)start, (int)end, dPercentage);
    }
}
