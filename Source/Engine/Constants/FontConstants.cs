using acamar.Source.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Constants
{
    public static class FontConstants
    {
        public static Font FONT0;
        public static void Initialize()
        {
            FONT0 = new Font(0, 12, 16);
        }
    }
}
