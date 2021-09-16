using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    public class Player : Character
    {
        private int nextPosx;
        private int nextPosy;

        public Player(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {
            animActive = true;
        }

        public override void StateMachine()
        {

        }

        public override void ReverseMove()
        {
            switch (CURRENTSTATE)
            {
                case STATE.WALKDOWN:
                    destRec.Y--;
                    Globals.CAMY += (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKLEFT:
                    destRec.X++;
                    Globals.CAMX -= (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKRIGHT:
                    destRec.X--;
                    Globals.CAMX += (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKUP:
                    destRec.Y++;
                    Globals.CAMY -= (int)Globals.SCALE;
                    Stop();
                    break;
            }
        }

        public override void HandleCollision(Entity target)
        {
            if (!target.IsCollidable() || !collidable) return;
            if (IsMoving())
            {
                ReverseMove();
            }
        }

        public void Interact(Interactable target)
        {
            if (IsNear(target) && IsFacing(target))
            {
                target.OnInteract();
            }
        }

        public override void Update()
        {
            if (fading)
                opacity += fadeStep;
            if (fading && (opacity < 0 || opacity > 1))
            {
                fading = false;
                opacity = (opacity < 0) ? 0 : 1;
            }

            if (!locked)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    MoveLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    MoveRight();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    MoveUp();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    MoveDown();
                }
                else
                {
                    Stop();
                }

                //DEBUG 
                if(Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Globals.world.Reset();
                }

                //DEBUG

                if (IsMoving())
                {
                    switch (CURRENTSTATE)
                    {
                        case STATE.WALKDOWN:
                            destRec.Y++;
                            Globals.CAMY-=(int)Globals.SCALE;
                            posy++;
                            break;
                        case STATE.WALKUP:
                            destRec.Y--;
                            Globals.CAMY+=(int)Globals.SCALE;
                            posy--;
                            break;
                        case STATE.WALKLEFT:
                            destRec.X--;
                            Globals.CAMX+=(int)Globals.SCALE;
                            posx--;
                            break;
                        case STATE.WALKRIGHT:
                            destRec.X++;
                            Globals.CAMX-=(int)Globals.SCALE;
                            posx++;
                            break;
                    }
                }

                Animate();
            }

            collRec.X = destRec.X + cPosx;
            collRec.Y = destRec.Y + cPosy;
        }

        public override void SetPosition(int posx, int posy)
        {
            if(TransitionHandler.IsActive())
            {
                nextPosx = posx;
                nextPosy = posy;
            }
            else
            {
                base.SetPosition(posx, posy);
                Globals.CAMX = Globals.SIZEX / 2 - posx * (int)Globals.SCALE;
                Globals.CAMY = Globals.SIZEY / 2 - posy * (int)Globals.SCALE;
            }
        }

        public void UpdatePosition()
        {
            base.SetPosition(nextPosx, nextPosy);
            Globals.CAMX = Globals.SIZEX / 2 - posx * (int)Globals.SCALE;
            Globals.CAMY = Globals.SIZEY / 2 - posy * (int)Globals.SCALE;
        }

        public override string ToString()
        {
            return posx + " " + posy + " " + dir;
        }
    }

    
    
}
