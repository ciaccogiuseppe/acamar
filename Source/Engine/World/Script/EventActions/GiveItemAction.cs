using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class GiveItemAction : EventAction
    {
        private bool ended = false;
        public override void Trigger()
        {

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
