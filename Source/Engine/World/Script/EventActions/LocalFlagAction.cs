using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class LocalFlagAction : EventAction
    {
        //TODO: use Type instead of int in initialization
        private enum TYPE
        {
            SETFLAG,
            UNSETFLAG
        }

        //List of flags to operate on
        private List<int> flags;

        //Type of action (set/unset)
        private TYPE type;

        //Flag for action ended running
        private bool ended = false;

        private bool started = false;

        //Map to operate on
        private Map map;

        //Flag action on List of flags
        //TODO: Remove if not used
        public LocalFlagAction(List<int> flags, int type, Map map)
        {
            this.flags = flags;
            if (type == 1) this.type = TYPE.SETFLAG;
            else this.type = TYPE.UNSETFLAG;
            this.map = map;
        }

        //Flag action on single flag
        public LocalFlagAction(int flag, int type, Map map)
        {
            flags = new List<int>();
            this.flags.Add(flag);
            if (type == 1) this.type = TYPE.SETFLAG;
            else this.type = TYPE.UNSETFLAG;
            this.map = map;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            switch (type)
            {
                case TYPE.SETFLAG:
                    foreach (int f in flags)
                        map.SetFlag(f);
                    break;
                case TYPE.UNSETFLAG:
                    foreach (int f in flags)
                        map.UnsetFlag(f);
                    break;
            }
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
