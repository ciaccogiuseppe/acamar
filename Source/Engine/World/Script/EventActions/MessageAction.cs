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
        private bool started = false;
        private bool ended = false;
        public MessageAction(string message, Entity talker) //character needed to take portrait
        {
            this.message = new Message(message);
            this.talker = talker;
        }

        public override void Trigger()
        {
            if (!ended)
            {
                //Globals.world.Save(1); //DEBUG

                started = true;
                //ended = false;
                MessageHandler.currentMessage = message;
                MessageHandler.Activate();
            }
        }

        public override bool IsEnded()
        {
            if (started && !MessageHandler.IsActive())
            {
                ended = true;
            }
            return ended;
        }

        public override void Reset()
        {
            started = false;
            ended = false;
        }
    }
}
