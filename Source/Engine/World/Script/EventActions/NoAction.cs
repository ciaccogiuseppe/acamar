using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class NoAction : EventAction
    {
        //Flag for action ended running
        private bool ended = false;
        public NoAction()
        {

        }

        //Activate action
        public override void Trigger()
        {
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
