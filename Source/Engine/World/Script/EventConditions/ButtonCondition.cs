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

        private Keys key;
        private KEYSTATE state;

        public ButtonCondition(Keys key, KEYSTATE state)
        {
            this.key = key;
            this.state = state;
        }

        public override bool IsVerified()
        {
            bool verified = false;
            switch(state)
            {
                case KEYSTATE.ISPRESSED:
                    //if (Keyboard.GetState().IsKeyDown(key))
                    if (MyKeyboard.IsPressedNotCont(key))
                        verified = true;
                    break;
                case KEYSTATE.ISRELEASED:
                    //if (!Keyboard.GetState().IsKeyDown(key))
                    if (!MyKeyboard.IsPressedNotCont(key))
                        verified = true;
                    break;
            }
            return verified;
        }
    }
}
