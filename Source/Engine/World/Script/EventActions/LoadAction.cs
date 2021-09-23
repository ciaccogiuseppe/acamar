using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class LoadAction : EventAction
    {
        //Save slot to operate on
        private int saveSlot;

        //Flag for action ended running
        //TODO: make flag member of EventAction and not repeated
        private bool ended = false;

        public LoadAction(int saveSlot)
        {
            this.saveSlot = saveSlot;
        }

        //Activate action
        public override void Trigger()
        {
            Globals.world.Load(saveSlot);
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
