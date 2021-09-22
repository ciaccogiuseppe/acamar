﻿using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SaveAction : EventAction
    {
        private int saveSlot;
        private bool ended = false;

        public SaveAction(int saveSlot)
        {
            this.saveSlot = saveSlot;
        }

        public override void Trigger()
        {
            //Globals.world.Save(saveSlot);
            Globals.SAVESLOTS[Globals.CURRENTSAVESLOT].Save();
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
