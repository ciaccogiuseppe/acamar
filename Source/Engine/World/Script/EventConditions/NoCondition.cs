using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class NoCondition : EventCondition
    {
        public NoCondition()
        {
        }

        //Check if condition is verified
        public override bool IsVerified()
        {
            return true;
        }
    }
}
