using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    //TODO: Remove List of flags if not used
    class FlagCondition : EventCondition
    {
        //Flags to check
        private List<int> flagIDs;

        //Type of condition (flag set/unset)
        private FLAGTYPE type;

        private enum FLAGTYPE
        {
            FLAGISSET,
            FLAGISUNSET
        }

        //TODO:REMOVE IF NOT USED
        public FlagCondition(List<int> flagIDs, bool type)
        {
            this.flagIDs = flagIDs;
            if (type == true) this.type = FLAGTYPE.FLAGISSET;
            else this.type = FLAGTYPE.FLAGISUNSET;
        }

        public FlagCondition(int flagID, bool type)
        {
            flagIDs = new List<int>();
            this.flagIDs.Add(flagID);
            if (type == true) this.type = FLAGTYPE.FLAGISSET;
            else this.type = FLAGTYPE.FLAGISUNSET;
        }

        //Check if condition is verified
        public override bool IsVerified()
        {
            bool verified = true;
            switch(type)
            {
                case FLAGTYPE.FLAGISUNSET:
                    foreach (int flag in flagIDs)
                    {
                        if (Flag.CheckFlag(flag))
                        {
                            verified = false;
                            break;
                        }
                    }
                    break;
                case FLAGTYPE.FLAGISSET:
                    foreach (int flag in flagIDs)
                    {
                        if (!Flag.CheckFlag(flag))
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
