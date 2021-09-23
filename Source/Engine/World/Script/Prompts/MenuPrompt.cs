using acamar.Source.Engine.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.Prompts
{
    class MenuPrompt : Prompt
    {
        public MenuPrompt(string promptMessage, List<string> promptOptions)
            : base(promptMessage, promptOptions)
        {
            this.promptMessage.SetFont(FontConstants.FONT3);
        }

        public override void Activate()
        {
            PromptHandler.PROMPTSTATE = Globals.STATE.MAINMENUPROMPT;
            PromptHandler.PREVSTATE = Globals.STATE.MAINMENU;
            promptMessage.Reset();
            //MessageHandler.currentMessage = promptMessage;
            //MessageHandler.PromptActivate();
            active = true;
        }

        public override void Update()
        {
            if (promptPage.IsEnded())
            {
                active = false;
                ended = true;
            }
            if (active && !promptMessage.IsEnded())
            {
                promptMessage.Update();
            }
            else if (active && promptMessage.IsEnded())
            {
                promptPage.Update();
            }
            else
            {
                Globals.CURRENTSTATE = Globals.STATE.MAINMENU;
            }
        }


    }
}
