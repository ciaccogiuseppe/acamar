using acamar.Source.Engine.World.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace acamar.Source.Engine.World
{
    public class Entity
    {
        //public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        //public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        protected int entid;
        protected int posx;
        protected int posy;
        protected int dir;
        protected int sprid;
        protected int width = 400;
        protected int height = 10;
        protected int currentAnimation = 0;
        protected int animationLength = 2;
        protected bool animationLoop = false;
        protected bool moving = false;
        protected bool locked = false;
        protected int layer = 0;
        protected Texture2D texture;
        protected Rectangle destRec;
        protected Rectangle sourceRec;
        protected Rectangle collRec;

        protected float opacity = 1.0f;
        protected float fadeStep = 0.1f;
        protected bool fading;


        protected List<Event> events = new List<Event>();
        protected List<Event> activeEvents = new List<Event>();
        protected bool active = true; //to activate/deactivate entity
        protected bool animActive = false; //activate/deactive animation
        protected bool loopAnim = false; //loop/nonloop animation

        protected bool collidable = true;
        protected int cPosx;
        protected int cPosy;

        public Entity()
        {
            texture = Globals.Content.Load<Texture2D>("2D\\0.spr");
        }

        public Entity(int entid, int sprid, int posx, int posy, int dir)
        {
            this.entid = entid;
            this.sprid = sprid;
            this.posx = posx;
            this.posy = posy;
            this.dir = dir;

            //width = WIDTHS[sprid]
            //height = HEIGHTS[sprid]
            destRec = new Rectangle(posx, posy, width, height);
            sourceRec = new Rectangle(0, 0, width, height);

            string SpritePATH = "2D\\" +  sprid + ".spr";

            texture = Globals.Content.Load<Texture2D>(SpritePATH);
        }


        public Entity(int entid, Texture2D texture, int posx, int posy, int dir)
        {
            this.entid = entid;
            this.posx = posx;
            this.posy = posy;
            this.dir = dir;

            //width = WIDTHS[sprid]
            //height = HEIGHTS[sprid]
            destRec = new Rectangle(posx, posy, width, height);
            sourceRec = new Rectangle(0, 0, width, height);


            this.texture = texture;
        }



        public virtual void Update()
        {
            
            Animate();


            foreach (Event evn in activeEvents)
            {
                evn.Continue();
                if(!evn.IsActive())
                {
                    activeEvents.Remove(evn);
                    evn.Reset();
                    break;
                }
            }
            foreach (Event evn in events)
            {
                evn.Trigger();
                if(evn.IsActive() && !activeEvents.Contains(evn))
                {
                    activeEvents.Add(evn);
                }
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if(active)
                batch.Draw(texture, destRec, sourceRec, Color.White*opacity, 0.0f, new Vector2(0,0), SpriteEffects.None,  layer);
        }

        protected virtual void Animate()
        {
            //= ANIMLEN[sprid][currentAnimation]
            if(fading)
                opacity += fadeStep;
            if(fading && (opacity < 0 || opacity > 1))
            {
                fading = false;
                opacity = (opacity < 0) ? 0 : 1;
            }

            if(animActive)
                sourceRec.X = (sourceRec.X + sourceRec.Width) % (animationLength * sourceRec.Width);
        }

        public virtual void Animation()
        {
            sourceRec.X = (sourceRec.X + sourceRec.Width) % (animationLength * sourceRec.Width);
        }

        protected void SetAnimation(int id)
        {
            sourceRec.Y = id * sourceRec.Height;
        }

        public void SetSourceRectangle(Rectangle rect)
        {
            sourceRec = rect;
            destRec.Width = rect.Width;
            destRec.Height = rect.Height;
        }

        public virtual void SetPosition(int posx, int posy)
        {
            this.posx = posx;
            this.posy = posy;
            destRec.X = posx;
            destRec.Y = posy;

            collRec.X = posx + cPosx;
            collRec.Y = posy + cPosy;
        }

        public int GetPosX()
        {
            return this.posx;
        }

        public int GetPosY()
        {
            return this.posy;
        }

        public bool Collide(Entity target)
        {
            if (target.collRec.Intersects(this.collRec))
            {

                return true;
            }

            return false;
        }

        protected virtual bool IsMoving()
        {
            return moving;
        }

        public virtual void Stop()
        {

        }
        
        public virtual void ReverseMove()
        {

        }

        public virtual void HandleCollision(Entity target)
        {
            if (!target.collidable || !collidable) return;
            if (IsMoving() /*&& IsStoppable*/)
            {
                ReverseMove();
                //Stop();
                if(Collide(target))
                {
                    target.ReverseMove();
                    //target.Stop();
                }
            }
            else
            {
                target.ReverseMove();
                //target.Stop();
            }
        }

        

        public virtual bool IsNear(Entity target)
        {
            return false;
        }

        public void ResetAnimation()
        {
            sourceRec.X = 0;
            sourceRec.Y = 0;
        }

        public void SetAnimationLength(int len)
        {
            animationLength = len;
        }

        public void AddEvent(Event evn)
        {
            events.Add(evn);
        }
        
        public int GetCenterX()
        {
            return destRec.Center.X;
        }

        public int GetCenterY()
        {
            return destRec.Center.Y;
        }


        public bool IsActive()
        {
            return active;
        }

        public void Activate()
        {
            active = true;
        }

        public void Deactivate()
        {
            active = false;
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }

        public Rectangle GetCollisionBox()
        {
            return collRec;
        }

        public void SetDir(int dir)
        {
            this.dir = dir;
        }

        public void FadeIn()
        {
            fading = true;
            opacity = 0;
            fadeStep = 0.01f;
        }

        public void FadeOut()
        {
            fading = true;
            opacity = 1;
            fadeStep = -0.01f;
        }

        public void ActivateAnimation()
        {
            animActive = true;
        }

        public bool IsCollidable()
        {
            return collidable;
        }

        public void SetCollisionRectangle(int posx, int posy, int width, int height)
        {
            cPosx = posx;
            cPosy = posy;
            //this.width = width;
            //this.height = height;
            collRec = new Rectangle(this.posx + posx, this.posy + posy, width, height);
        }
    }

}
