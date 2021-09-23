using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class MessageAction : EventAction
    {
        //Message to write
        private Message message;

        //Talking entity
        private Entity talker;

        //Flag for action started running
        private bool started = false;

        //Flag for action ended running
        private bool ended = false;

        public MessageAction(string message, Entity talker) //character needed to take portrait
        {
            this.message = new Message(message);
            this.talker = talker;
        }

        //Activate action
        public override void Trigger()
        {
            if (!ended)
            {
                started = true;
                MessageHandler.currentMessage = message;
                MessageHandler.Activate();
            }
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            if (started && !MessageHandler.IsActive())
            {
                ended = true;
            }
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            started = false;
            ended = false;
        }
    }
}
