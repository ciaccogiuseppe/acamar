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
        public static Font FONT1;
        public static Font FONT2;
        public static Font FONT3;
        public static Font FONT4;
        public static Font FONT5;
        public static void Initialize()
        {
            //Font initialization: new Font(fontID, fontWidth, fontHeight)
            FONT0 = new Font(0, 12, 16);
            FONT1 = new Font(1, 12, 16);
            FONT2 = new Font(2, 12, 16);
            FONT3 = new Font(3, 11, 18);
            FONT4 = new Font(4, 11, 18);
            FONT5 = new Font(5, 11, 18);

            //FontDictionary: to get font from scripts
            FontDictionary.Add("FONT0", FONT0);
            FontDictionary.Add("FONT1", FONT1);
            FontDictionary.Add("FONT2", FONT2);
            FontDictionary.Add("FONT3", FONT3);
            FontDictionary.Add("FONT4", FONT4);
            FontDictionary.Add("FONT5", FONT5);
        }
    }
}
