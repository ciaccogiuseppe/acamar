using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Constants
{
    class ItemConstants
    {
        public enum ITEMS
        {
            KEY
        }
        public static Dictionary<string, ITEMS> itemDict = new Dictionary<string, ITEMS>()
            {
                {"KEY", ITEMS.KEY}
            };
    }
}
