using System;
using System.Collections.Generic;
using System.Timers;

namespace Transitions
{
    internal class TransitionManager
    {
        private static TransitionManager m_Instance;
        private IDictionary<Transition, bool> m_Transitions = (IDictionary<Transition, bool>)new Dictionary<Transition, bool>();
        private Timer m_Timer;
        private object m_Lock = new object();

        public static TransitionManager getInstance()
        {
            if (TransitionManager.m_Instance == null)
                TransitionManager.m_Instance = new TransitionManager();
            return TransitionManager.m_Instance;
        }

        public void register(Transition transition)
        {
            lock (this.m_Lock)
            {
                this.removeDuplicates(transition);
                this.m_Transitions[transition] = true;
                transition.TransitionCompletedEvent += new EventHandler<Transition.Args>(this.onTransitionCompleted);
            }
        }

        private void removeDuplicates(Transition transition)
        {
            foreach (KeyValuePair<Transition, bool> transition1 in (IEnumerable<KeyValuePair<Transition, bool>>)this.m_Transitions)
                this.removeDuplicates(transition, transition1.Key);
        }

        private void removeDuplicates(Transition newTransition, Transition oldTransition)
        {
            IList<Transition.TransitionedPropertyInfo> transitionedProperties1 = newTransition.TransitionedProperties;
            IList<Transition.TransitionedPropertyInfo> transitionedProperties2 = oldTransition.TransitionedProperties;
            for (int index = transitionedProperties2.Count - 1; index >= 0; --index)
            {
                Transition.TransitionedPropertyInfo info = transitionedProperties2[index];
                foreach (Transition.TransitionedPropertyInfo transitionedPropertyInfo in (IEnumerable<Transition.TransitionedPropertyInfo>)transitionedProperties1)
                {
                    if (info.target == transitionedPropertyInfo.target && info.propertyInfo == transitionedPropertyInfo.propertyInfo)
                        oldTransition.removeProperty(info);
                }
            }
        }

        private TransitionManager()
        {
            this.m_Timer = new Timer(15.0);
            this.m_Timer.Elapsed += new ElapsedEventHandler(this.onTimerElapsed);
            this.m_Timer.Enabled = true;
        }

        private void onTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.m_Timer == null)
                return;
            this.m_Timer.Enabled = false;
            IList<Transition> transitionList;
            lock (this.m_Lock)
            {
                transitionList = (IList<Transition>)new List<Transition>();
                foreach (KeyValuePair<Transition, bool> transition in (IEnumerable<KeyValuePair<Transition, bool>>)this.m_Transitions)
                    transitionList.Add(transition.Key);
            }
            foreach (Transition transition in (IEnumerable<Transition>)transitionList)
                transition.onTimer();
            this.m_Timer.Enabled = true;
        }

        private void onTransitionCompleted(object sender, Transition.Args e)
        {
            Transition key = (Transition)sender;
            key.TransitionCompletedEvent -= new EventHandler<Transition.Args>(this.onTransitionCompleted);
            lock (this.m_Lock)
                this.m_Transitions.Remove(key);
        }
    }
}
