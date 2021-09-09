using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SleepAction : EventAction
    {
        private bool ended = false;
        private int duration;
        private int tic;

        public SleepAction(int duration)
        {
            this.duration = duration;
            tic = 0;
        }

        public override void Trigger()
        {
            
        }

        public override bool IsEnded()
        {
            if(tic < duration && Globals.CURRENTSTATE == Globals.STATE.RUNNING)
                tic++;
            if (tic == duration)
                ended = true;
            return ended;
        }

        public override void Reset()
        {
            ended = false;
            tic = 0;
        }
    }
}
