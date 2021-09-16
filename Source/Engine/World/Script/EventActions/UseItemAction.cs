using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class UseItemAction : EventAction
    {
        private bool ended = false;
        private Item item;

        public override void Trigger()
        {
            item.Use();
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
