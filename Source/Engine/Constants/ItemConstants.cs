using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Constants
{
    public class ItemConstants
    {

        //Item Type Enum
        public enum ITEMS
        {
            KEY
        }

        //Dictionary: String Type to Enum Type
        public static Dictionary<string, ITEMS> itemDict = new Dictionary<string, ITEMS>()
            {
                {"KEY", ITEMS.KEY}
            };

        //Dictionary: Enum Type to String Type
        public static Dictionary<ITEMS, string> names = new Dictionary<ITEMS, string>()
            {
                {ITEMS.KEY, "KEY"}
            };

        //TODO: load names from file
        //Dictionary: String Type to String Name
        public static Dictionary<string, string> itemNames = new Dictionary<string, string>()
            {
                {"KEY", "Small Key" }
            };
    }
}
