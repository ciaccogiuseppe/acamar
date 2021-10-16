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
        public static List<Message> currentMessages = new List<Message>();
        public static List<Globals.STATE> PREVSTATES = new List<Globals.STATE>();
        private static bool active = false;

        public static Globals.STATE PREVSTATE;

        public static void Activate()
        {
            active = true;
            //if (Globals.CURRENTSTATE != Globals.STATE.PAUSE) PREVSTATE = Globals.CURRENTSTATE;
            //Globals.CURRENTSTATE = Globals.STATE.PAUSE;
            PREVSTATES.Add(Globals.CURRENTSTATE);
            Globals.CURRENTSTATE = Globals.STATE.PAUSE;

            currentMessage.Reset();
        }

        public static void AddMessage(Message message)
        {
            currentMessages.Add(message);
            currentMessage = currentMessages[currentMessages.Count - 1];
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
            //if(active && !currentMessage.IsEnded())
            //{
            //    currentMessage.Update();
            //}
            //else if (active && currentMessage.IsEnded())
            //{
            //    active = false;
            //    currentMessage = null;
            //    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
            //}

            if(active && !currentMessage.IsEnded())
            {
                currentMessage.Update();
            }
            else if (active && currentMessage.IsEnded())
            {
                currentMessages.RemoveAt(currentMessages.Count - 1);
                if (currentMessages.Count == 0)
                {
                    active = false;
                    currentMessage = null;
                    //Globals.CURRENTSTATE = PREVSTATE;
                }
                else
                {
                    currentMessage = currentMessages[currentMessages.Count - 1];
                }

                Globals.CURRENTSTATE = PREVSTATES[currentMessages.Count];
                PREVSTATES.RemoveAt(currentMessages.Count);
            }
            else if(!active)
            {
                Globals.CURRENTSTATE = PREVSTATES[currentMessages.Count];
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
