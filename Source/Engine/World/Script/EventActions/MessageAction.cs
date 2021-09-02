using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class MessageAction : EventAction
    {
        private Message message;
        private Entity talker;
        public MessageAction(string message, Entity talker) //character needed to take portrait
        {
            this.message = new Message(message);
            this.talker = talker;
        }

        public override void Trigger()
        {
            MessageHandler.currentMessage = message;
            MessageHandler.Activate();
        }
    }
}
