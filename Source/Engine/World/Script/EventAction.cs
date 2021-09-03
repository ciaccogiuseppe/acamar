using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script
{
    public abstract class EventAction
    {
        public abstract void Trigger();

        public abstract bool IsEnded();

        public abstract void Reset();

        //internal abstract void Continue();
    }
}
