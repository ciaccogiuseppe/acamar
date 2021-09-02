using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Transition
    {
        private bool ended;
        //private int tranPosX = 0;
        //private int tranPosY = 0;
        private int step = 20;
        private TRANDIR dir;
        private TRANTYPE type;
        private int[,] tranArray;
        private Entity[,] entArray;
        private int animationLength = 5;
        private int globalCount;
        private int h;
        private int w;

        private Texture2D tranSprite;

        public enum TRANDIR
        {
            LTOR, //→
            RTOL, //←
            UTOD, //↓
            DTOU  //↑
        }

        public enum TRANTYPE
        {
            PRE,
            POST
        }

        public Transition(TRANDIR dir, TRANTYPE type)
        {
            
            

            switch (dir)
            {
                case TRANDIR.LTOR:
                    if (type == TRANTYPE.PRE)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\LTORPRE.spr");
                    else if (type == TRANTYPE.POST)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\LTORPOST.spr");
                    break;
                case TRANDIR.RTOL:
                    //tranPosX = Globals.SIZEX;
                    if (type == TRANTYPE.PRE)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\RTOLPRE.spr");
                    else if (type == TRANTYPE.POST)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\RTOLPOST.spr");
                    break;
                case TRANDIR.UTOD:
                    if (type == TRANTYPE.PRE)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\UTODPRE.spr");
                    else if (type == TRANTYPE.POST)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\UTODPOST.spr");
                    break;
                case TRANDIR.DTOU:
                    //tranPosY = Globals.SIZEY;
                    if (type == TRANTYPE.PRE)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\DTOUPRE.spr");
                    else if (type == TRANTYPE.POST)
                        tranSprite = Globals.Content.Load<Texture2D>("2D\\DTOUPOST.spr");
                    break;
            }

            w = Globals.SIZEX / tranSprite.Width * animationLength;
            h = Globals.SIZEY / tranSprite.Height;
            

            GenerateArray();
            ResetArray();
            ResetCount();
            

            this.dir = dir;
            this.type = type;
        }

        public void GenerateArray()
        {
            tranArray = new int[h, w];
            entArray = new Entity[h, w];

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    entArray[i, j] = new Entity(0, tranSprite, j * tranSprite.Width / animationLength, i * tranSprite.Height, 0);
                    entArray[i, j].SetSourceRectangle(new Rectangle(0, 0, tranSprite.Width / animationLength, tranSprite.Height));
                    entArray[i, j].SetAnimationLength(animationLength);
                }
            }
        }

        public void ResetArray()
        {
            for(int i = 0; i < h; i++)
            {
                for(int j = 0; j < w; j++)
                {
                    if (dir == TRANDIR.LTOR) tranArray[i, j] = -j * animationLength;
                    else if (dir == TRANDIR.RTOL) tranArray[i, j] = -(w - 1 - j) * animationLength;
                    else if (dir == TRANDIR.UTOD) tranArray[i, j] = -i * animationLength;
                    else if (dir == TRANDIR.DTOU) tranArray[i, j] = -(h - 1 - i) * animationLength;
                    entArray[i, j].ResetAnimation();
                }
            }
        }

        public void ResetCount()
        {
            ended = false;
            if (dir == TRANDIR.LTOR || dir == TRANDIR.RTOL) globalCount = w * animationLength;
            else globalCount = h * animationLength;
        }

        public void Update()
        {
            globalCount-=2;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (tranArray[i, j] >= 0 && tranArray[i, j] < animationLength - 1)
                    {
                        entArray[i, j].Update();
                        entArray[i, j].Update();
                    }
                    tranArray[i, j]+=2;
                }
            }

            if (globalCount < 0)
            {
                ended = true;
            }
        }

        public void Draw()
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    entArray[i, j].Draw();
                }
            }
        }

        public bool IsEnded()
        {
            return ended;
        }


    }
}
