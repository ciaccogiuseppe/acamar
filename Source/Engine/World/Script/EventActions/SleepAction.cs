using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class SleepAction : EventAction
    {
        //Sleep duration in tics
        private int duration;

        //Current tic
        private int tic;

        //Flag for action ended running
        private bool started = false;
        private bool ended = false;

        

        public SleepAction(int duration)
        {
            this.duration = duration;
            tic = 0;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            if(tic < duration && Globals.CURRENTSTATE == Globals.STATE.RUNNING)
                tic++;
            if (tic == duration)
                ended = true;
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            started = false;
            ended = false;
            tic = 0;
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
