using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class LocalFlagCondition : EventCondition
    {
        private List<int> flagIDs;
        private FLAGTYPE type;
        private Map map;
        private enum FLAGTYPE
        {
            FLAGISSET,
            FLAGISUNSET
        }

        public LocalFlagCondition(List<int> flagIDs, bool type, Map map)
        {
            this.flagIDs = flagIDs;
            if (type == true) this.type = FLAGTYPE.FLAGISSET;
            else this.type = FLAGTYPE.FLAGISUNSET;
            this.map = map;
        }

        public LocalFlagCondition(int flagID, bool type, Map map)
        {
            flagIDs = new List<int>();
            this.flagIDs.Add(flagID);
            if (type == true) this.type = FLAGTYPE.FLAGISSET;
            else this.type = FLAGTYPE.FLAGISUNSET;
            this.map = map;
        }

        public override bool IsVerified()
        {
            bool verified = true;
            switch (type)
            {
                case FLAGTYPE.FLAGISUNSET:
                    foreach (int flag in flagIDs)
                    {
                        if (map.CheckFlag(flag))
                        {
                            verified = false;
                            break;
                        }
                    }
                    break;
                case FLAGTYPE.FLAGISSET:
                    foreach (int flag in flagIDs)
                    {
                        if (!map.CheckFlag(flag))
                        {
                            verified = false;
                            break;
                        }
                    }
                    break;
            }
            return verified;
        }
    }
}
