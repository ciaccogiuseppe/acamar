using acamar.Source.Engine.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.Prompts
{
    class RunningPrompt : Prompt
    {
        public RunningPrompt(string promptMessage, List<string> promptOptions)
            : base(promptMessage, promptOptions)
        {
            this.promptMessage.SetFont(FontConstants.FONT0);
            promptPage = new PromptPage(promptOptions, FontConstants.FONT0, FontConstants.FONT0);
        }

        public override void Activate()
        {
            PromptHandler.PROMPTSTATE = Globals.STATE.RUNNINGPROMPT;
            PromptHandler.PREVSTATE = Globals.STATE.RUNNING;
            promptMessage.Reset();
            //MessageHandler.currentMessage = promptMessage;
            //MessageHandler.PromptActivate();
            active = true;
        }

        public override void Update()
        {
            if (promptPage.IsEnded()) active = false;
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
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
            }
        }

    }
}
