using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script
{
    public class Event
    {
        private List<EventCondition> triggerCondition = new List<EventCondition>();
        private List<EventAction> triggerAction = new List<EventAction>();
        private bool active = false;

        public void AddCondition(EventCondition cond)
        {
            triggerCondition.Add(cond);
        }

        public void AddAction(EventAction act)
        {
            triggerAction.Add(act);
        }

        public void Trigger()
        {
            bool verified = true;
            foreach (EventCondition cond in triggerCondition)
            {
                if (!cond.IsVerified())
                {
                    verified = false;
                    break;
                }
            }
            if (verified)
            {
                active = true;
                //foreach (EventAction act in triggerAction)
                if(!triggerAction[0].IsEnded())
                    triggerAction[0].Trigger();
                    //act.Trigger();
            }
        }

        internal bool IsActive()
        {
            return active;
        }

        public void Continue()
        {
            active = false;
            foreach (EventAction act in triggerAction)
            {
                if(!act.IsEnded())
                {
                    active = true;
                    act.Trigger();
                    break;
                }
            }
        }

        public void Reset()
        {
            foreach (EventAction act in triggerAction)
            {
                act.Reset();
            }
        }
    }
}
