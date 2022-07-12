using System;
using System.Collections.Generic;

namespace Transitions
{
    internal class TransitionChain
    {
        private LinkedList<Transition> m_listTransitions = new LinkedList<Transition>();

        public TransitionChain(params Transition[] transitions)
        {
            foreach (Transition transition in transitions)
                this.m_listTransitions.AddLast(transition);
            this.runNextTransition();
        }

        private void runNextTransition()
        {
            if (this.m_listTransitions.Count == 0)
                return;
            Transition transition = this.m_listTransitions.First.Value;
            transition.TransitionCompletedEvent += new EventHandler<Transition.Args>(this.onTransitionCompleted);
            transition.run();
        }

        private void onTransitionCompleted(object sender, Transition.Args e)
        {
            ((Transition)sender).TransitionCompletedEvent -= new EventHandler<Transition.Args>(this.onTransitionCompleted);
            this.m_listTransitions.RemoveFirst();
            this.runNextTransition();
        }
    }
}
