using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitions
{
    public class TransitionType_EaseInEaseOut : ITransitionType
    {
        private double m_dTransitionTime;

        public TransitionType_EaseInEaseOut(int iTransitionTime) => this.m_dTransitionTime = iTransitionTime > 0 ? (double)iTransitionTime : throw new Exception("Transition time must be greater than zero.");

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            double dElapsed = (double)iTime / this.m_dTransitionTime;
            dPercentage = Utility.convertLinearToEaseInEaseOut(dElapsed);
            if (dElapsed >= 1.0)
            {
                dPercentage = 1.0;
                bCompleted = true;
            }
            else
                bCompleted = false;
        }
    }
}
