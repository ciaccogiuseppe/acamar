using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class FadeAction : EventAction
    {
        public enum TYPE
        {
            FADEIN,
            FADEOUT
        }

        //Entity to fadein/fadeout
        private Entity target;

        //Type of action (fadein/fadeout)
        private TYPE type;

        //Flag for action ended running
        private bool ended = false;

        public FadeAction(Entity target, TYPE type)
        {
            this.target = target;
            this.type = type;
        }
        
        //Activate action
        public override void Trigger()
        {
            switch (type)
            {
                case TYPE.FADEIN:
                    target.FadeIn();
                    break;
                case TYPE.FADEOUT:
                    target.FadeOut();
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
