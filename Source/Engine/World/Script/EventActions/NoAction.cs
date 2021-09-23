using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class NoAction : EventAction
    {
        private bool ended = false;
        public NoAction()
        {

        }

        public override void Trigger()
        {
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
