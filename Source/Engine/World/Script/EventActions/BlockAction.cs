using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class BlockAction : EventAction
    {
        public enum BLOCKTYPE
        {
            LOCK,
            UNLOCK
        }

        private Entity target;
        private BLOCKTYPE type;
        private bool ended = false;

        public BlockAction(Entity target, BLOCKTYPE type)
        {
            this.target = target;
            this.type = type;
        }

        public override void Trigger()
        {
            switch(type)
            {
                case BLOCKTYPE.LOCK:
                    target.Lock();
                    break;
                case BLOCKTYPE.UNLOCK:
                    target.Unlock();
                    break;
            }
            ended = true;
        }


        public override bool IsEnded()
        {
            return ended;
        }

        public override void Reset()
        {
            ended = false;
        }
    }
}
