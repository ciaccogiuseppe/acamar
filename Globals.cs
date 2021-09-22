using acamar.Source.Engine;
using acamar.Source.Engine.World;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar
{
    public class Globals
    {
        public enum STATE
        {
            MAINMENU,
            MAINMENUPROMPT,
            RUNNING,
            RUNNINGPROMPT,
            PAUSE,
            TRANSITION,
            INGAMEMENU,
            EXIT
        }

        public static SpriteBatch _spriteBatch;

        public static SpriteBatch _overBatch;

        public static ContentManager Content;
        public static STATE CURRENTSTATE = STATE.MAINMENU;

        public static int clockCount = 30;

        public static int GSIZEX = 400;
        public static int GSIZEY = 400;

        public static int SIZEX = 400;
        public static int SIZEY = 400;

        //View offset in window, used to place the sprites correctly
        public static int OFFX = 0;
        public static int OFFY = 0;

        public static float SCALE = (float)SIZEX / 400.0f;

        public static int CAMX = 0;
        public static int CAMY = 0;

        public static Player player;
        public static MainMenu mainMenu;
        public static World world;

        public static TimeSpan runningTime;
        public static DateTime lastTime;

        public static Keys MENUKEY = Keys.Z;

        public static Keys MOVEUP = Keys.Up;
        public static Keys MOVELEFT = Keys.Left;
        public static Keys MOVERIGHT = Keys.Right;
        public static Keys MOVEDOWN = Keys.Down;

        public static List<Keys> ASSIGNEDKEYS = new List<Keys> {MENUKEY, MOVEUP, MOVELEFT, MOVERIGHT, MOVEDOWN};


        public static int CURRENTSAVESLOT = 1;

        public static SaveSlot EMPTYSLOT = new SaveSlot(0);
        public static SaveSlot SLOT1 = new SaveSlot(1);
        public static SaveSlot SLOT2 = new SaveSlot(2);
        public static SaveSlot SLOT3 = new SaveSlot(3);

        public static SaveSlot[] SAVESLOTS = new SaveSlot[4] { EMPTYSLOT, SLOT1, SLOT2, SLOT3 };

        public static int PROMPTRESULT = 0;
    }
}
