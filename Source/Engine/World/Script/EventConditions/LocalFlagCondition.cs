using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    //TODO: Remove List of flags if not used
    class LocalFlagCondition : EventCondition
    {
        //Flags to check
        private List<int> flagIDs;

        //Type of condition (flag set/unset)
        private FLAGTYPE type;

        //Map to operate on
        private Map map;

        private enum FLAGTYPE
        {
            FLAGISSET,
            FLAGISUNSET
        }

        //TODO: REMOVE IF NOT USED
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


        //Check if condition is verified
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
