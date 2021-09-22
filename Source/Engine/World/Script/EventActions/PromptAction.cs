using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class PromptAction : EventAction
    {
        private Prompt prompt;
        private bool ended = false;
        private bool started = false;
        public PromptAction(Prompt prompt)
        {
            this.prompt = prompt;
        }

        public override void Trigger()
        {
            PromptHandler.currentPrompt = prompt;
            PromptHandler.Activate();
            started = true;
            ended = false;
        }

        public override bool IsEnded()
        {
            if (started && !PromptHandler.IsActive()) ended = true;
            return ended;
        }

        public override void Reset()
        {
            prompt.Reset();
            ended = false;
            started = false;
        }
    }
}
