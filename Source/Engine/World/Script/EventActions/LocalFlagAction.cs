using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class LocalFlagAction : EventAction
    {
        private enum FLAGTYPE
        {
            SETFLAG,
            UNSETFLAG
        }

        private List<int> flags;
        private FLAGTYPE type;
        private bool ended = false;
        private Map map;
        public LocalFlagAction(List<int> flags, int type, Map map)
        {
            this.flags = flags;
            if (type == 1) this.type = FLAGTYPE.SETFLAG;
            else this.type = FLAGTYPE.UNSETFLAG;
            this.map = map;
        }

        public LocalFlagAction(int flag, int type, Map map)
        {
            flags = new List<int>();
            this.flags.Add(flag);
            if (type == 1) this.type = FLAGTYPE.SETFLAG;
            else this.type = FLAGTYPE.UNSETFLAG;
            this.map = map;
        }

        public static LocalFlagAction Parse(string s, Map map)
        {
            string[] values = s.Split(' ');
            int flag;
            int type = -1;
            switch (values[0])
            {
                case "LOCFLGSET":
                    type = 1;
                    break;

                case "LOCFLGUNSET":
                    type = 0;
                    break;
            }
            flag = int.Parse(values[1]);
            LocalFlagAction action = new LocalFlagAction(flag, type, map);
            return action;
        }

        public override void Trigger()
        {
            switch (type)
            {
                case FLAGTYPE.SETFLAG:
                    foreach (int f in flags)
                        map.SetFlag(f);
                    break;
                case FLAGTYPE.UNSETFLAG:
                    foreach (int f in flags)
                        map.UnsetFlag(f);
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
