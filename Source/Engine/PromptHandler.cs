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
        public static Globals.STATE PREVSTATE;
        public static Globals.STATE PROMPTSTATE;
        private static bool active = false;

        public static void Activate()
        {
            active = true;
            currentPrompt.Activate();
            Globals.CURRENTSTATE = PROMPTSTATE;
            
        }

        public static bool IsActive()
        {
            return active;
        }

        public static void Update()
        {
            if (currentPrompt.IsEnded()) active = false;
            if (active)
            {
                currentPrompt.Update();
            }

            if(!active)
            {
                Globals.CURRENTSTATE = PREVSTATE;
            }
        }

        public static void Draw(SpriteBatch batch)
        {
            currentPrompt.Draw(batch);
        }
    }
}
