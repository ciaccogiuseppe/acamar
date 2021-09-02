using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class NoCondition : EventCondition
    {
        public override bool IsVerified()
        {
            return true;
        }
    }
}
