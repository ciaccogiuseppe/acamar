using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script
{
    class Flag
    {
        public const int FLAGNO = 256;
        public static int[] flags = new int[FLAGNO];

        public static bool CheckFlag(int flagId)
        {
            return flags[flagId] == 0 ? false : true;
        }

        public static void SetFlag(int flagId)
        {
            flags[flagId] = 1;
        }

        public static void UnsetFlag(int flagId)
        {
            flags[flagId] = 0;
        }

        public static void ResetFlags()
        {
            for (int i = 0; i < FLAGNO; i++)
            {
                UnsetFlag(i);
            }
        }
    }
}
