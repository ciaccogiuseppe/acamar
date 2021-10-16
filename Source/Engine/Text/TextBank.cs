using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Text
{
    public class TextBank
    {
        public const int BANKSIZE = 8192;
        public static string[] TEXTBANK = new string[BANKSIZE];

        public static string GetStringFromBank(int stringID)
        {
            return TEXTBANK[stringID];
        }

        public static void SetStringInBank(string str, int stringID)
        {
            TEXTBANK[stringID] = str;
        }
    }
}
