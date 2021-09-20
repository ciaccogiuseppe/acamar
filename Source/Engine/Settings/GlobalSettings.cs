using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Settings
{
    public class GlobalSettings
    {
        public enum RESOLUTION
        {
            RES400x400,
            RES600x600,
            RES800x600,
            RES600x800,
            RES800x800,
            COUNT
        }

        public static Dictionary<RESOLUTION, string> resolutionDict = new Dictionary<RESOLUTION, string>()
            {
                {RESOLUTION.RES400x400, "400 x 400"},
                {RESOLUTION.RES600x600, "600 x 600"},
                {RESOLUTION.RES800x600, "800 x 600"},
                {RESOLUTION.RES600x800, "600 x 800"},
                {RESOLUTION.RES800x800, "800 x 800"}
            };

        public static RESOLUTION CURRENTRESOLUTION = RESOLUTION.RES400x400;
        public static bool CHANGEDRES = false;

        public static void SetResolution(RESOLUTION res)
        {
            CHANGEDRES = true;
            CURRENTRESOLUTION = res;
            switch (CURRENTRESOLUTION)
            {
                case (RESOLUTION.RES400x400):
                    Globals.SIZEX = 400;
                    Globals.SIZEY = 400;
                    Globals.OFFX = 0;
                    Globals.OFFY = 0;
                    Globals.SCALE = 1;
                    break;
                case (RESOLUTION.RES600x600):
                    Globals.SIZEX = 600;
                    Globals.SIZEY = 600;
                    Globals.OFFX = 100;
                    Globals.OFFY = 100;
                    Globals.SCALE = 1;
                    break;
                case (RESOLUTION.RES800x600):
                    Globals.SIZEX = 800;
                    Globals.SIZEY = 600;
                    Globals.OFFX = 200;
                    Globals.OFFY = 100;
                    Globals.SCALE = 1;
                    break;
                case (RESOLUTION.RES600x800):
                    Globals.SIZEX = 600;
                    Globals.SIZEY = 800;
                    Globals.OFFX = 100;
                    Globals.OFFY = 200;
                    Globals.SCALE = 1;
                    break;
                case (RESOLUTION.RES800x800):
                    Globals.SIZEX = 800;
                    Globals.SIZEY = 800;
                    Globals.OFFX = 0;
                    Globals.OFFY = 0;
                    Globals.SCALE = 2;
                    break;
            }
        }


    }
}
