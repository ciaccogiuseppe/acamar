using System;
using System.Collections.Generic;
using System.IO;
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

        public static void LoadStringBank(int bankID)
        {
            string filename = "Content\\TEXTBANK." + bankID;
            string[] lines = File.ReadAllLines(filename);
            int id;
            for(int i = 0; i < lines.Length; i++)
            {
                id = int.Parse(lines[i].Split("::")[0]);
                lines[i] = lines[i].Split("::")[1];
                SetStringInBank(lines[i], id);
            }
        }
    }
}
