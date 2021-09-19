using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class DisableAction : EventAction
    {
        private bool ended = false;
        private TYPE type;
        Entity target;

        public enum TYPE
        {
            ENABLE,
            DISABLE
        }

        public DisableAction(Entity target, TYPE type)
        {
            this.target = target;
            this.type = type;
        }

        public override void Trigger()
        {
            switch(type)
            {
                case TYPE.ENABLE:
                    target.Enable();
                    break;

                case TYPE.DISABLE:
                    target.Disable();
                    break;
            }
            ended = true;
        }

        public override void Reset()
        {
            ended = false;
        }

        public override bool IsEnded()
        {
            return ended;
        }
    }
}
