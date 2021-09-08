using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class LoadAction : EventAction
    {
        private int saveSlot;
        private bool ended = false;

        public LoadAction(int saveSlot)
        {
            this.saveSlot = saveSlot;
        }

        public override void Trigger()
        {
            Globals.world.Load(saveSlot);
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
