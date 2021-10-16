using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class NoAction : EventAction
    {
        //Flag for action ended running
        private bool ended = false;
        private bool started = false;
        public NoAction()
        {

        }

        //Activate action
        public override void Trigger()
        {
            started = true;
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
