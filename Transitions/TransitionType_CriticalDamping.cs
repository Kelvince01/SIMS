using System;

namespace Transitions
{
    public class TransitionType_CriticalDamping : ITransitionType
    {
        private double m_dTransitionTime;

        public TransitionType_CriticalDamping(int iTransitionTime) => this.m_dTransitionTime = iTransitionTime > 0 ? (double)iTransitionTime : throw new Exception("Transition time must be greater than zero.");

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            double num = (double)iTime / this.m_dTransitionTime;
            dPercentage = (1.0 - Math.Exp(-1.0 * num * 5.0)) / 0.993262053;
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
