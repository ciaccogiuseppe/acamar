using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class PromptAction : EventAction
    {
        //Prompt to be asked
        private Prompt prompt;

        //Flag for action ended running
        private bool ended = false;

        //Flag for action started running
        private bool started = false;

        public PromptAction(Prompt prompt)
        {
            this.prompt = prompt;
        }

        //Activate action
        public override void Trigger()
        {
            PromptHandler.currentPrompt = prompt;
            PromptHandler.Activate();
            started = true;
            ended = false;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            if (started && !PromptHandler.IsActive()) ended = true;
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            prompt.Reset();
            ended = false;
            started = false;
        }
    }
}
