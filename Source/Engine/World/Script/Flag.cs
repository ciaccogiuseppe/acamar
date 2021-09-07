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

        public static string FlagsToString()
        {
            string outputString = "";
            foreach (int i in flags)
            {
                outputString += i + " ";
            }
            return outputString;
        }

        public static void SetFlags(string flagsString)
        {
            string[] f = flagsString.Split();
            for(int i = 0; i < FLAGNO; i++)
            {
                flags[i] = int.Parse(f[i]);
            }
        }
    }
}
