using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine
{
    class MessageHandler
    {
        public static Message currentMessage;
        private static bool active = false;

        public static void Activate()
        {
            active = true;
            Globals.CURRENTSTATE = Globals.STATE.PAUSE;
            currentMessage.Reset();
        }

        //public static void PromptActivate()
        //{
        //    active = true;
        //    currentMessage.Reset();
        //}

        public static bool IsActive()
        {
            return active;
        }

        public static void Update()
        {
            if(active && !currentMessage.IsEnded())
            {
                currentMessage.Update();
            }
            else if (active && currentMessage.IsEnded())
            {
                active = false;
                currentMessage = null;
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
            }
        }

        public static void Draw(SpriteBatch batch)
        {
            if(active)
            {
                currentMessage.Draw(batch);
            }
        }
    }
}
