using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities.LightSources
{
    class Sonar : LightSource
    {

        private int radius;
        private int curRadius;
        public Sonar(int posx, int posy, int radius):
            base(posx, posy, 10000)
        {
            this.posx = posx;
            this.posy = posy;
            this.radius = radius;

            curRadius = 0;
        }

        public override void Update()
        {
            curRadius++;
            if (curRadius == radius) curRadius = 0;
        }

        public override int GetLightLevel(int x, int y)
        {
            if (
                (posx - x) * (posx - x) + (posy - y) * (posy - y) < ((curRadius + 1) * (curRadius + 1))
                &&
                (posx - x) * (posx - x) + (posy - y) * (posy - y) > ((curRadius - 1) * (curRadius - 1))
                ) /*return 5;*/
            {
                if (((x - posx) * (x - posx) + (y - posy) * (y - posy)) == 0) return intensity >= 6? 6:intensity;
                return Math.Min(intensity / ((x - posx) * (x - posx) + (y - posy) * (y - posy)), 6);
            }
            else return 0;
        }

        public override void SetPosition(int x, int y)
        {
            if(curRadius == 0)
            {
                this.posx = x;
                this.posy = y;
            }
                
        }

        public override bool IsCovered(int x, int y, Rectangle rec, int[,] lightMap)
        {
            if((posx - x) * (posx - x) + (posy - y) * (posy - y) > ((curRadius + 1) * (curRadius + 1))
                &&
                (posx - x) * (posx - x) + (posy - y) * (posy - y) < ((curRadius - 1) * (curRadius - 1))
                ) return false;

            if (rec.Contains(new Point(x, y))) return true;


            int vx = posx - x;
            int vy = posy - y;
            int[] p = new int[4] { -vx, vx, -vy, vy };
            int[] q = new int[4] { x - rec.Left, rec.Right - x, y - rec.Top, rec.Bottom - y };
            float u1 = float.NegativeInfinity;
            float u2 = float.PositiveInfinity;



            if (posx > x && x > rec.Right) return false;
            if (posx < x && x < rec.Left) return false;
            if (posy > y && y > rec.Bottom) return false;
            if (posy < y && y < rec.Top) return false;

            for (int i = 0; i < 4; i++)
            {
                if (p[i] == 0)
                {
                    if (q[i] < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    float t = (float)q[i] / (float)p[i];
                    if (p[i] < 0 && u1 < t)
                    {
                        u1 = t;
                    }
                    else if (p[i] > 0 && u2 > t)
                    {
                        u2 = t;
                    }
                }
            }
            if (u1 > u2 || u1 > 1 || u1 < 0)
            {
                return false;
            }

            for(int i = rec.Top; i <= rec.Bottom; i++)
            {
                for(int j = rec.Left; j <= rec.Right; j++)
                {
                    if (i == rec.Top || i == rec.Bottom || j == rec.Left || j == rec.Right)
                        try
                        {
                            //lightMap[i, j] = 6;
                            if (((j - posx) * (j - posx) + (i - posy) * (i - posy)) == 0) lightMap[i, j] = intensity >= 6 ? 6 : intensity;
                            else lightMap[i,j] = Math.Min(intensity / ((j - posx) * (j - posx) + (i - posy) * (i - posy)), 6);
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }

                }
            }
            return true;
        }

    }
}
