using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class FlagAction : EventAction
    {
        private enum FLAGTYPE
        {
            SETFLAG,
            UNSETFLAG
        }

        private List<int> flags;
        private FLAGTYPE type;
        private bool ended = false;
        public FlagAction(List<int> flags, int type)
        {
            this.flags = flags;
            if (type == 1) this.type = FLAGTYPE.SETFLAG;
            else this.type = FLAGTYPE.UNSETFLAG;
        }

        public override void Trigger()
        {
            switch(type)
            {
                case FLAGTYPE.SETFLAG:
                    foreach (int f in flags)
                        Flag.SetFlag(f);
                    break;
                case FLAGTYPE.UNSETFLAG:
                    foreach (int f in flags)
                        Flag.UnsetFlag(f);
                    break;
            }
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
