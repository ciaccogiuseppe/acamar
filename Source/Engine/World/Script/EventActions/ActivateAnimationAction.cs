using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class ActivateAnimationAction : EventAction
    {
        public enum TYPE
        {
            ACTIVATEANIM,
            DEACTIVATEANIM
        }

        //Entity to activate/deactivate
        private Entity target;

        //Type of action to operate (activate/deactivate)
        private TYPE type;

        //Flag for action ended running
        private bool ended = false;
        private bool started = false;

        public ActivateAnimationAction(Entity target, TYPE type)
        {
            this.target = target;
            this.type = type;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            switch (type)
            {
                case TYPE.ACTIVATEANIM:
                    target.ActivateAnimation();
                    break;
                case TYPE.DEACTIVATEANIM:
                    target.DectivateAnimation();
                    break;
            }
            ended = true;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            return ended;
        }

        //Reset action
        public override void Reset()
        {
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
