using System;

namespace Transitions
{
    public class TransitionType_Deceleration : ITransitionType
    {
        private double m_dTransitionTime;

        public TransitionType_Deceleration(int iTransitionTime) => this.m_dTransitionTime = iTransitionTime > 0 ? (double)iTransitionTime : throw new Exception("Transition time must be greater than zero.");

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            double num = (double)iTime / this.m_dTransitionTime;
            dPercentage = num * (2.0 - num);
            if (num >= 1.0)
            {
                dPercentage = 1.0;
                bCompleted = true;
            }
            else
                bCompleted = false;
        }
    }
}
