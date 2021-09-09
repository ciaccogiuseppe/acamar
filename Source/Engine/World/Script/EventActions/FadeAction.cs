using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class FadeAction : EventAction
    {
        public enum FADETYPE
        {
            FADEIN,
            FADEOUT
        }
        private bool ended = false;
        private Entity target;
        private FADETYPE type;

        public FadeAction(Entity target, FADETYPE type)
        {
            this.target = target;
            this.type = type;
        }
        
        public override void Trigger()
        {
            switch (type)
            {
                case FADETYPE.FADEIN:
                    target.FadeIn();
                    break;
                case FADETYPE.FADEOUT:
                    target.FadeOut();
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
