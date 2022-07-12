using System;

namespace Transitions
{
    public class TransitionType_Linear : ITransitionType
    {
        private double m_dTransitionTime;

        public TransitionType_Linear(int iTransitionTime) => this.m_dTransitionTime = iTransitionTime > 0 ? (double)iTransitionTime : throw new Exception("Transition time must be greater than zero.");

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            dPercentage = (double)iTime / this.m_dTransitionTime;
            if (dPercentage >= 1.0)
            {
                dPercentage = 1.0;
                bCompleted = true;
            }
            else
                bCompleted = false;
        }
    }
}
