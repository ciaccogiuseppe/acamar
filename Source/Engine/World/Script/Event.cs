using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script
{
    class Event
    {
        private List<EventCondition> triggerCondition = new List<EventCondition>();
        private List<EventAction> triggerAction = new List<EventAction>();

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
                foreach (EventAction act in triggerAction)
                    act.Trigger();
            }
        }
    }
}
