using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    //Enable/disable action: collision enable/disable
    class DisableAction : EventAction
    {
        public enum TYPE
        {
            ENABLE,
            DISABLE
        }

        //Entity to enable/disable
        Entity target;

        //Type of action to operate (enable/disable)
        private TYPE type;

        //Flag for action ended running
        private bool ended = false;


        public DisableAction(Entity target, TYPE type)
        {
            this.target = target;
            this.type = type;
        }

        //Activate action
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
