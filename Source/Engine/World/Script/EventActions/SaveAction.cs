using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SaveAction : EventAction
    {
        //Flag for action ended running
        private bool ended = false;

        public SaveAction()
        {

        }

        //Activate action
        public override void Trigger()
        {
            Globals.SAVESLOTS[Globals.CURRENTSAVESLOT].Save();
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
