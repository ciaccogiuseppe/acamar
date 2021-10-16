using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SubEventAction : EventAction
    {
        private Event subEvent;
        private bool ended = false;
        private bool started = false;

        public SubEventAction(Event evn)
        {
            subEvent = evn;
        }

        public override void Trigger()
        {

            subEvent.Trigger();
            started = true;
            if(!subEvent.IsActive())
            {
                ended = true;
            }
        }

        public override bool IsEnded()
        {
            if(subEvent.IsActive() && started)
            {
                subEvent.Continue();
            }
            else if (started && !subEvent.IsActive())
            {
                ended = true;
            }
            return ended;
        }

        public override void Reset()
        {
            subEvent.Reset();
            started = false;
            ended = false;
        }

        public override bool IsStarted()
        {
            return started;
        }

        public override bool GetEnded()
        {
            return ended;
        }
    }
}
