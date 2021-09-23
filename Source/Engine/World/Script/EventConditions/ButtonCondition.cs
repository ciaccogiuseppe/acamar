using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class ButtonCondition : EventCondition
    {
        public enum KEYSTATE
        {
            ISPRESSED,
            ISRELEASED
        }

        //Key to operate on
        private Keys key;

        //State of key (pressed/released)
        private KEYSTATE state;

        public ButtonCondition(Keys key, KEYSTATE state)
        {
            this.key = key;
            this.state = state;
        }

        //Check if condition is verified
        public override bool IsVerified()
        {
            bool verified = false;
            switch(state)
            {
                case KEYSTATE.ISPRESSED:
                    if (MyKeyboard.IsPressedNotCont(key))
                        verified = true;
                    break;
                case KEYSTATE.ISRELEASED:
                    if (!MyKeyboard.IsPressedNotCont(key))
                        verified = true;
                    break;
            }
            return verified;
        }
    }
}
