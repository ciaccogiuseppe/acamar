using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class ActivateAction : EventAction
    {
        public enum ACTIVTYPE
        {
            ACTIVATE,
            DEACTIVATE  
        }

        private Entity target;
        private ACTIVTYPE type;
        private bool ended = false;
        public ActivateAction(Entity target, ACTIVTYPE type)
        {
            this.target = target;
            this.type = type;
        }

        public override void Trigger()
        {
            switch(type)
            {
                case ACTIVTYPE.ACTIVATE:
                    target.Activate();
                    break;
                case ACTIVTYPE.DEACTIVATE:
                    target.Deactivate();
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
