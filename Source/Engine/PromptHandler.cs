using acamar.Source.Engine.World.Script;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine
{
    class PromptHandler
    {
        public static Prompt currentPrompt;
        public static List<Prompt> currentPrompts = new List<Prompt>();
        public static List<Globals.STATE> PREVSTATES = new List<Globals.STATE>();
        public static Globals.STATE PREVSTATE;
        public static Globals.STATE PROMPTSTATE;
        private static bool active = false;

        public static void Activate()
        {
            active = true;
            currentPrompt.Activate();
            PREVSTATES.Add(Globals.CURRENTSTATE);
            Globals.CURRENTSTATE = PROMPTSTATE;
            
        }

        public static void AddPrompt(Prompt p)
        {
            currentPrompts.Add(p);
            currentPrompt = currentPrompts[currentPrompts.Count - 1];
        }

        public static bool IsActive()
        {
            return active;
        }

        public static void Update()
        {
            //if (currentPrompt.IsEnded()) active = false;
            //if (active)
            //{
            //    currentPrompt.Update();
            //}

            //if(!active)
            //{
            //    Globals.CURRENTSTATE = PREVSTATE;
            //}

            while (currentPrompts.Count > 0 && currentPrompt.IsEnded())
            {
                currentPrompts.RemoveAt(currentPrompts.Count - 1);
                if(currentPrompts.Count == 0)
                {
                    currentPrompt = null;
                    active = false;
                }
                else
                {
                    currentPrompt = currentPrompts[currentPrompts.Count - 1];
                }
            }

            if (active)
            {
                currentPrompt.Update();
            }

            if (!active)
            {
                //Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                Globals.CURRENTSTATE = PREVSTATES[currentPrompts.Count];
                PREVSTATES.RemoveAt(currentPrompts.Count);
            }
        }

        public static void Draw(SpriteBatch batch)
        {
            if(active)
                currentPrompt.Draw(batch);
        }
    }
}
