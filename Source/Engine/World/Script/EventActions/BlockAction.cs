using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    //Block action: block movement
    class BlockAction : EventAction
    {
        public enum TYPE
        {
            LOCK,
            UNLOCK
        }

        //Entity to lock/unlock
        private Entity target;

        //Type of action to operate (lock/unlock)
        private TYPE type;

        //Flag for action ended running
        private bool ended = false;

        public BlockAction(Entity target, TYPE type)
        {
            this.target = target;
            this.type = type;
        }

        //Activate action
        public override void Trigger()
        {
            if (!ended)
            {
                switch (type)
                {
                    case TYPE.LOCK:
                        target.Lock();
                        break;
                    case TYPE.UNLOCK:
                        target.Unlock();
                        break;
                }
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
            ended = false;
        }
    }
}
