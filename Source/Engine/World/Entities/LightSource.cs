using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    public class LightSource
    {
        protected int posx;
        protected int posy;
        protected int intensity;

        protected int count = 10;
        protected bool decrease = true;
        protected bool angled = false;
        protected int step = 0;

        protected int angle = 1;
        protected DIRECTION dir;

        public enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public LightSource(int posx, int posy, int intensity)
        {
            this.posx = posx;
            this.posy = posy;
            this.intensity = intensity;
        }

        public void SetDirection(DIRECTION dir)
        {
            this.dir = dir;
        }

        public virtual int GetLightLevel(int x, int y)
        {
            if(angled)
            {
                if(dir == DIRECTION.RIGHT)
                {
                    if ((x - posx) > 0 && angled && Math.Abs((y - posy) * 3) / Math.Abs(x - posx) > angle) return 0;
                    if ((x - posx) <= 0) return 0;
                }
                else if(dir == DIRECTION.LEFT)
                {
                    if ((x - posx) < 0 && angled && Math.Abs((y - posy) * 3) / Math.Abs(x - posx) > angle) return 0;
                    if ((x - posx) >= 0) return 0;
                }
                else if(dir == DIRECTION.DOWN)
                {
                    if ((y - posy) > 0 && angled && Math.Abs((x - posx) * 3) / Math.Abs(y - posy) > angle) return 0;
                    if ((y - posy) <= 0) return 0;
                }
                else if (dir == DIRECTION.UP)
                {
                    if ((y - posy) < 0 && angled && Math.Abs((x - posx) * 3) / Math.Abs(y - posy) > angle) return 0;
                    if ((y - posy) >= 0) return 0;
                }

                if (((x - posx) * (x - posx) + (y - posy) * (y - posy)) == 0) return intensity;
                return intensity / ((x - posx) * (x - posx) + (y - posy) * (y - posy));
            }

            
            if (((x - posx) * (x - posx) + (y - posy) * (y - posy)) == 0) return intensity;
            return intensity / ((x - posx) * (x - posx) + (y - posy) * (y - posy));
        }


        ///https://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
        ///https://stackoverflow.com/questions/2284746/xna-line-segment-intersection
        ///https://gist.github.com/ChickenProp/3194723
        ///
        // p3 ------ p4
        // |          |
        // p5 ------ p6
        public virtual bool IsCovered(int x, int y, Rectangle rec, int[,] lightMap)
        {
            bool intersects = false;
            
            int vx = posx - x;
            int vy = posy - y;
            int[] p = new int[4] {-vx, vx, -vy, vy};
            int[] q = new int[4] { x - rec.Left, rec.Right - x, y - rec.Top, rec.Bottom - y };
            float u1 = float.NegativeInfinity;
            float u2 = float.PositiveInfinity;



            if (posx > x && x > rec.Right) return false;
            if (posx < x && x < rec.Left) return false;
            if (posy > y && y > rec.Bottom) return false;
            if (posy < y && y < rec.Top) return false;


            int dist = (posx - x) * (posx - x) + (posy - y) * (posy - y);


            if (y > rec.Top && y < rec.Bottom)
            {
                if (Math.Min(Math.Abs(x - rec.Left), Math.Abs(x - rec.Right)) > Math.Sqrt(Math.Sqrt(dist))-2) return false;
            }
            else if (x > rec.Left && x < rec.Right)
            {
                if (Math.Min(Math.Abs(y - rec.Top), Math.Abs(y - rec.Bottom)) > Math.Sqrt(Math.Sqrt(dist))-2) return false;
            }
            else
            {
                //if (Math.Max(
                //    Math.Min(Math.Abs(y - rec.Top), Math.Abs(y - rec.Bottom)),
                //    Math.Min(Math.Abs(x - rec.Left), Math.Abs(x - rec.Right))
                //    ) > 3) return false;
                if ((Math.Min(Math.Abs(y - rec.Top), Math.Abs(y - rec.Bottom)) * Math.Min(Math.Abs(y - rec.Top), Math.Abs(y - rec.Bottom)) +
                    Math.Min(Math.Abs(x - rec.Left), Math.Abs(x - rec.Right)) * Math.Min(Math.Abs(x - rec.Left), Math.Abs(x - rec.Right))) > Math.Sqrt(dist) -  20)
                    return false;
            }

            //int minDist = (posx - rec.Center.X) * (posx - rec.Center.X) +
            //    (posy - rec.Center.Y) * (posy - rec.Center.Y);

            //int ptDist = (x - rec.Center.X) * (x - rec.Center.X) +
            //    (y - rec.Center.Y) * (y- rec.Center.Y);

            //int dist = (posx - x) * (posx - x) +
            //    (posy - y) * (posy - y);

            //if (ptDist > (dist - minDist)) return false;

            //if (ptDist > minDist/2) return false;

            for (int i = 0; i < 4; i++)
            {
                if(p[i] == 0)
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


            int collx = x + (int)u1 * vx;
            int colly = y + (int)u1 * vy;

            int distLR = (collx - posx) * (collx - posx) + (colly - posy) * (colly - posy);
            int distPR = (collx - x) * (collx - x) + (colly - y) * (colly - y);
            if (2 * distPR > distLR) return false;

            return true;
            //return intersects;
        }

        public virtual void SetPosition(int x, int y)
        {
            this.posx = x;
            this.posy = y;
        }

        public virtual void Update()
        {
            Random  rnd = new Random();
            if(decrease && count == 10)
            {
                step = rnd.Next(1, 10);
                intensity -= step * 100;
                decrease = false;
                count--;
            }
            else if (!decrease && count == 10)
            {
                intensity += step * 100;
                decrease = true;
                count--;
            }

            if (count != 10) count--;
            if (count == 0) count = 10;
        }
    }
}
