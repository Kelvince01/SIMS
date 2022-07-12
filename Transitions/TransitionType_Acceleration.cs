using System;

namespace Transitions
{
    public class TransitionType_Acceleration : ITransitionType
    {
        private double m_dTransitionTime;

        public TransitionType_Acceleration(int iTransitionTime) => this.m_dTransitionTime = iTransitionTime > 0 ? (double)iTransitionTime : throw new Exception("Transition time must be greater than zero.");

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            double num1 = (double)iTime / this.m_dTransitionTime;
            dPercentage = 0;
            ref double local = ref dPercentage;
            double num2 = num1;
            double num3 = num2 * num2;
            local = num3;
            if (num1 >= 1.0)
            {
                dPercentage = 1.0;
                bCompleted = true;
            }
            else
                bCompleted = false;
        }
    }
}
