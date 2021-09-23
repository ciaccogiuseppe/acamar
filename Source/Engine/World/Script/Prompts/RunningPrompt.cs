using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Script.EventConditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.Prompts
{
    class RunningPrompt : Prompt
    {
        private Event[] promptEvents;
        private Event curEvent;
        private bool triggered = false;
        public RunningPrompt(string promptMessage, List<string> promptOptions)
            : base(promptMessage, promptOptions)
        {
            this.promptMessage.SetFont(FontConstants.FONT0);
            promptPage = new PromptPage(promptOptions, FontConstants.FONT0, FontConstants.FONT0);
            promptEvents = new Event[promptOptions.Count];
            for(int i = 0; i < promptOptions.Count; i++)
            {
                promptEvents[i] = new Event();
                promptEvents[i].AddCondition(new NoCondition());
            }
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
            if (promptPage.IsEnded() && !triggered)
            {
                curEvent = promptEvents[Globals.PROMPTRESULT];
                curEvent.Trigger();
                ended = true;
                triggered = true;
            }
            if(triggered && curEvent.IsActive())
            {
                curEvent.Continue();
            }
            if(triggered && !curEvent.IsActive())
            {
                triggered = false;
                active = false;
                //ended = true;
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
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
            }
        }

        public void AddAction(EventAction action, int option)
        {
            promptEvents[option].AddAction(action);
        }

        public override void Reset()
        {
            base.Reset();
            triggered = false;
            ended = false;
            active = false;
            foreach (Event evn in promptEvents)
            {
                evn.Reset();
            }
        }
    }
}
