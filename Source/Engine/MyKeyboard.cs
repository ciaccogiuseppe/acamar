using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine
{
    public static class MyKeyboard
    {
        public static List<Keys> pressedKeys = new List<Keys> ();

        public static bool IsPressedNotCont(Keys key)
        {
            if(Keyboard.GetState().IsKeyDown(key) && !pressedKeys.Contains(key))
            {
                pressedKeys.Add(key);
                return true;
            }
            //else if (Keyboard.GetState().IsKeyDown(key) && pressedKeys.Contains(key))
            //{
            //    return false;
            //}
            return false;
        }

        public static void Reset()
        {
            List<Keys> toRemove = new List<Keys>();
            foreach (Keys k in pressedKeys)
            {
                if (!Keyboard.GetState().IsKeyDown(k))
                {
                    //pressedKeys.Remove(k);
                    toRemove.Add(k);
                }
            }

            foreach (Keys k in toRemove)
            {
                pressedKeys.Remove(k);
            }
        }
    }
}
