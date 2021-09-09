using acamar.Source.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Constants
{
    public static class FontConstants
    {
        public static Dictionary<string, Font> FontDictionary = new Dictionary<string, Font>();
        public static Font FONT0;
        public static void Initialize()
        {
            FONT0 = new Font(0, 12, 16);
            FontDictionary.Add("FONT0", FONT0);
        }
    }
}
