using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class FaceAction : EventAction
    {
        private bool ended = false;
        private Character target;
        private Character targeted;

        public FaceAction(Character target, Character targeted)
        {
            this.target = target;
            this.targeted = targeted;
        }

        public override void Trigger()
        {
            target.FaceCharacter(targeted);
            ended = true;
        }

        public override bool IsEnded()
        {
            return ended;
        }

        public override void Reset()
        {
            ended = false;
        }
    }
}
