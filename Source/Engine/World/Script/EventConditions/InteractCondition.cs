using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class InteractCondition : EventCondition
    {
        PositionCondition position;
        ButtonCondition button;

        public InteractCondition(Character target, Entity ent)
        {
            position = new PositionCondition(target, ent, PositionCondition.POSTYPE.POSTOUCHFACING);
            button = new ButtonCondition(Globals.INTERACTKEY, ButtonCondition.KEYSTATE.ISPRESSED);
        }

        public override bool IsVerified()
        {
            return position.IsVerified() && button.IsVerified();
        }
    }
}
