using System;

namespace Transitions
{
    internal class ManagedType_String : IManagedType
    {
        public Type getManagedType() => typeof(string);

        public object copy(object o) => (object)new string(((string)o).ToCharArray());

        public object getIntermediateValue(object start, object end, double dPercentage)
        {
            string str1 = (string)start;
            string str2 = (string)end;
            int length1 = str1.Length;
            int length2 = str2.Length;
            int length3 = Utility.interpolate(length1, length2, dPercentage);
            char[] chArray = new char[length3];
            for (int index = 0; index < length3; ++index)
            {
                char ch1 = 'a';
                if (index < length1)
                    ch1 = str1[index];
                char ch2 = 'a';
                if (index < length2)
                    ch2 = str2[index];
                char ch3 = ch2 != ' ' ? Convert.ToChar(Utility.interpolate(Convert.ToInt32(ch1), Convert.ToInt32(ch2), dPercentage)) : ' ';
                chArray[index] = ch3;
            }
            return (object)new string(chArray);
        }
    }
}
