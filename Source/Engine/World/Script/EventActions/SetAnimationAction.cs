using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SetAnimationAction : EventAction
    {
        //Entity to activate/deactivate
        private Entity target;
        private int animID;
        private int animLength;
        private bool loop;


        //Flag for action ended running
        private bool ended = false;
        private bool started = false;

        public SetAnimationAction(Entity target, int animID, int animLength, bool loop)
        {
            this.target = target;
            this.animID = animID;
            this.animLength = animLength;
            this.loop = loop;
        }

        public SetAnimationAction(Entity target, int animID)
        {
            this.target = target;
            this.animID = animID;
            this.animLength = -1;
            this.loop = false;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            target.SetAnimation(animID);
            if(animLength != -1)
            {
                target.SetAnimationLength(animLength);
                target.SetLoop(loop);
            }
            target.ResetAnimation();
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
