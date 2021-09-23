using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    //TODO: REMOVE IF NOT USED
    class MenuSaveSlot : MenuButton
    {
        public enum ACTION
        {
            NEWGAME,
            LOADGAME
        }

        public enum SLOT
        {
            FIRST,
            SECOND,
            THIRD
        }

        ACTION action;
        SLOT slot;

        public MenuSaveSlot(ACTION action, SLOT slot)
        {
            this.action = action;
            this.slot = slot;
            switch(slot)
            {
                case SLOT.FIRST:
                    //check if save file exists, get time, make box with time and place, if newgame ask if want to replace, if continue can choose to remove or continue
                    break;
                case SLOT.SECOND:
                    break;
                case SLOT.THIRD:
                    break;
            }
        }
    }
}
