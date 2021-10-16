using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    //Face action: Target char faces Targeted char
    class FaceAction : EventAction
    {
        
        //Facing character; changes dir towards targeted; action subject
        private Character target;

        //Faced character; character followed by target one; action object
        private Character targeted;

        //Flag for action ended running
        private bool ended = false;
        private bool started = false;


        public FaceAction(Character target, Character targeted)
        {
            this.target = target;
            this.targeted = targeted;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            target.FaceCharacter(targeted);
            ended = true;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            started = false;
            ended = false;
        }
        public override bool IsStarted()
        {
            return started;
        }

        public override bool GetEnded()
        {
            return ended;
        }
    }
}
