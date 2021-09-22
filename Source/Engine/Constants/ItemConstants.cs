using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Constants
{
    public class ItemConstants
    {
        public enum ITEMS
        {
            KEY
        }
        public static Dictionary<string, ITEMS> itemDict = new Dictionary<string, ITEMS>()
            {
                {"KEY", ITEMS.KEY}
            };

        public static Dictionary<ITEMS, string> names = new Dictionary<ITEMS, string>()
            {
                {ITEMS.KEY, "KEY"}
            };

        //TODO: load names from file
        public static Dictionary<string, string> itemNames = new Dictionary<string, string>()
        {
            {"KEY", "Small Key" }
        };
    }
}
