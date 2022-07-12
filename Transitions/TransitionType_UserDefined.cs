using System;
using System.Collections.Generic;

namespace Transitions
{
    public class TransitionType_UserDefined : ITransitionType
    {
        private IList<TransitionElement> m_Elements;
        private double m_dTransitionTime;
        private int m_iCurrentElement;

        public TransitionType_UserDefined()
        {
        }

        public TransitionType_UserDefined(IList<TransitionElement> elements, int iTransitionTime) => this.setup(elements, iTransitionTime);

        public void setup(IList<TransitionElement> elements, int iTransitionTime)
        {
            this.m_Elements = elements;
            this.m_dTransitionTime = (double)iTransitionTime;
            if (elements.Count == 0)
                throw new Exception("The list of elements passed to the constructor of TransitionType_UserDefined had zero elements. It must have at least one element.");
        }

        public void onTimer(int iTime, out double dPercentage, out bool bCompleted)
        {
            double dTimeFraction = (double)iTime / this.m_dTransitionTime;
            double dStartTime;
            double dEndTime;
            double dStartValue;
            double dEndValue;
            InterpolationMethod eInterpolationMethod;
            this.getElementInfo(dTimeFraction, out dStartTime, out dEndTime, out dStartValue, out dEndValue, out eInterpolationMethod);
            double num = dEndTime - dStartTime;
            double dElapsed = (dTimeFraction - dStartTime) / num;
            double dPercentage1;
            switch (eInterpolationMethod)
            {
                case InterpolationMethod.Linear:
                    dPercentage1 = dElapsed;
                    break;

                case InterpolationMethod.Accleration:
                    dPercentage1 = Utility.convertLinearToAcceleration(dElapsed);
                    break;

                case InterpolationMethod.Deceleration:
                    dPercentage1 = Utility.convertLinearToDeceleration(dElapsed);
                    break;

                case InterpolationMethod.EaseInEaseOut:
                    dPercentage1 = Utility.convertLinearToEaseInEaseOut(dElapsed);
                    break;

                default:
                    throw new Exception("Interpolation method not handled: " + eInterpolationMethod.ToString());
            }
            dPercentage = Utility.interpolate(dStartValue, dEndValue, dPercentage1);
            if ((double)iTime >= this.m_dTransitionTime)
            {
                bCompleted = true;
                dPercentage = dEndValue;
            }
            else
                bCompleted = false;
        }

        private void getElementInfo(
          double dTimeFraction,
          out double dStartTime,
          out double dEndTime,
          out double dStartValue,
          out double dEndValue,
          out InterpolationMethod eInterpolationMethod)
        {
            int count;
            for (count = this.m_Elements.Count; this.m_iCurrentElement < count; ++this.m_iCurrentElement)
            {
                double num = this.m_Elements[this.m_iCurrentElement].EndTime / 100.0;
                if (dTimeFraction < num)
                    break;
            }
            if (this.m_iCurrentElement == count)
                this.m_iCurrentElement = count - 1;
            dStartTime = 0.0;
            dStartValue = 0.0;
            if (this.m_iCurrentElement > 0)
            {
                TransitionElement element = this.m_Elements[this.m_iCurrentElement - 1];
                dStartTime = element.EndTime / 100.0;
                dStartValue = element.EndValue / 100.0;
            }
            TransitionElement element1 = this.m_Elements[this.m_iCurrentElement];
            dEndTime = element1.EndTime / 100.0;
            dEndValue = element1.EndValue / 100.0;
            eInterpolationMethod = element1.InterpolationMethod;
        }
    }
}
