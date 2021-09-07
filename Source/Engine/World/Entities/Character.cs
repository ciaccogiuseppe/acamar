using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    public class Character : Interactable
    {
        protected enum STATE
        {
            IDLEUP,
            IDLEDOWN,
            IDLELEFT,
            IDLERIGHT,
            WALKUP,
            WALKDOWN,
            WALKLEFT,
            WALKRIGHT
        }

        private const int WALKDURATION = 140;
        private const int ANIMDURATION = 20;
        private int animlen = 5;
        protected STATE CURRENTSTATE;
        private int walkingCount = WALKDURATION;
        private bool isFreezed = false;
        private Random random;

        private int destPosX;
        private int destPosY;

        private int animCount = ANIMDURATION;

        public Character (int entid, int sprid, int posx, int posy, int dir):
            base(entid, sprid, posx, posy, dir)
        {
            random = new Random();
            SetSourceRectangle(new Rectangle(0, 0, 16, 16));
        }

        public void MoveLeft()
        {
            CURRENTSTATE = STATE.WALKLEFT;
            SetAnimation(2);
        }

        public void MoveRight()
        {
            CURRENTSTATE = STATE.WALKRIGHT;
            SetAnimation(3);
        }

        public void MoveUp()
        {
            CURRENTSTATE = STATE.WALKUP;
            SetAnimation(0);
        }

        public void MoveDown()
        {
            CURRENTSTATE = STATE.WALKDOWN;
            SetAnimation(1);
        }

        public override void Stop()
        {
            switch(CURRENTSTATE)
            {
                case STATE.WALKDOWN:
                    CURRENTSTATE = STATE.IDLEDOWN;
                    break;
                case STATE.WALKLEFT:
                    CURRENTSTATE = STATE.IDLELEFT;
                    break;
                case STATE.WALKRIGHT:
                    CURRENTSTATE = STATE.IDLERIGHT;
                    break;
                case STATE.WALKUP:
                    CURRENTSTATE = STATE.IDLEUP;
                    break;
            }
        }
    
        private void RandomWalk()
        {
            switch(random.Next(0,4))
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
            }
        }
    
        public void FaceCharacter(Character character)
        {
            Rectangle A = GetCollisionBox();
            Rectangle B = character.GetCollisionBox();
            int distX = A.Right <= B.Left ? 
                B.Left - A.Right:
                A.Left - B.Right;

            int distY = A.Bottom <= B.Top ?
                B.Top - A.Bottom :
                A.Top - B.Bottom;

            if (distX > distY)
            {
                if (GetCollisionBox().Right <= character.GetCollisionBox().Left)
                    CURRENTSTATE = STATE.IDLERIGHT;
                if (GetCollisionBox().Left >= character.GetCollisionBox().Right)
                    CURRENTSTATE = STATE.IDLELEFT;
            }
            else
            {
                if (GetCollisionBox().Top >= character.GetCollisionBox().Bottom)
                    CURRENTSTATE = STATE.IDLEUP;
                if (GetCollisionBox().Bottom <= character.GetCollisionBox().Top)
                    CURRENTSTATE = STATE.IDLEDOWN;
            }

            Animate();
            //if (posy < character.GetPosY() && character.GetPosY() - posy < Math.Abs(character.GetPosX() - posx))
            //    CURRENTSTATE = STATE.IDLEUP;
            //if (posy > character.GetPosY() && posy - character.GetPosY() < Math.Abs(character.GetPosX() - posx))
            //    CURRENTSTATE = STATE.IDLEDOWN;
        }

        private void Talk(Character character)
        {
            FaceCharacter(character);
            Freeze();
            //CurrentMessage = new Message(...);
        }

        public void Freeze()
        {
            isFreezed = true;
        }

        public void Unfreeze()
        {
            isFreezed = false;
        }

        public void MoveToY(int destY)
        {
            if (destRec.Y < destY)
                CURRENTSTATE = STATE.WALKDOWN;
            else
                CURRENTSTATE = STATE.WALKUP;
            destPosY = destY;
        }

        public void MoveToX(int destX)
        {
            if (destRec.X < destX)
                CURRENTSTATE = STATE.WALKRIGHT;
            else
                CURRENTSTATE = STATE.WALKLEFT;
            destPosX = destX;
        }

        protected override bool IsMoving()
        {
            if (CURRENTSTATE == STATE.WALKDOWN) return true;
            if (CURRENTSTATE == STATE.WALKLEFT) return true;
            if (CURRENTSTATE == STATE.WALKUP) return true;
            if (CURRENTSTATE == STATE.WALKRIGHT) return true;
            return false;
        }

        public virtual void StateMachine()
        {
            if (!isFreezed)
            {
                if (IsMoving())
                {
                    walkingCount -= random.Next(1, 5); //(1,10)
                    if(walkingCount <= 0)
                    {
                        Stop();
                        walkingCount = WALKDURATION;
                    }
                }
                else
                {
                    //walkingCount -= random.Next(1, 10); //(1,5)
                    walkingCount = 0;

                    //Console.Write(walkingCount);

                    if (walkingCount <= 0)
                    {
                        if(random.Next(0,2) == 0)
                        {
                            //RandomWalk();
                            walkingCount = WALKDURATION;
                        }
                        else
                        {
                            walkingCount = WALKDURATION;
                        }
                    }
                }
            }
            else
            {
                //if (currentMessage.isEnded())
                {
                    Unfreeze();
                    walkingCount = WALKDURATION;
                    //currentMessage = null;
                }
            }
        }


        public override void Update()
        {
            if (!locked)
            { 
                base.Update();
                if (Globals.CURRENTSTATE != Globals.STATE.RUNNING) return;

                //StateMachine();
                if (IsMoving())
                {
                    switch (CURRENTSTATE)
                    {
                        case STATE.WALKDOWN:
                            //if(posy<400-sourceRec.Height)
                            if (posy < destPosY)
                            {
                                destRec.Y++;
                                posy++;
                            }
                            else
                            {
                                Stop();
                                walkingCount = WALKDURATION;
                            }
                            break;
                        case STATE.WALKUP:
                            //if(posy>0)
                            if (posy > destPosY)
                            {
                                destRec.Y--;
                                posy--;
                            }
                            else
                            {
                                Stop();
                                walkingCount = WALKDURATION;
                            }
                            break;
                        case STATE.WALKLEFT:
                            //if(posx>0)
                            if (posx > destPosX)
                            {
                                destRec.X--;
                                posx--;
                            }
                            else
                            {
                                Stop();
                                walkingCount = WALKDURATION;
                            }
                            break;
                        case STATE.WALKRIGHT:
                            //if(posx<400-sourceRec.Width)
                            if (posx < destPosX)
                            {
                                destRec.X++;
                                posx++;
                            }
                            else
                            {
                                Stop();
                                walkingCount = WALKDURATION;
                            }
                            break;
                    }
                }

                Animate();
            }
        }

        protected override void Animate()
        {
            if(IsMoving())
            {
                if (animCount == ANIMDURATION)
                { 
                    int animationLength = 4; //= ANIMLEN[sprid][currentAnimation]
                    sourceRec.X = (sourceRec.X + sourceRec.Width) % (animationLength * sourceRec.Width);
                    if (sourceRec.X == 0) sourceRec.X += sourceRec.Width;
                }
                animCount--;
                if (animCount == 0)
                {
                    animCount = ANIMDURATION;
                }
            }
            else
            {
                sourceRec.X = 0;
                animCount = ANIMDURATION;

                switch (CURRENTSTATE)
                {
                    case STATE.IDLEDOWN:
                        SetAnimation(1);
                        break;
                    case STATE.IDLELEFT:
                        SetAnimation(2);
                        break;
                    case STATE.IDLERIGHT:
                        SetAnimation(3);
                        break;
                    case STATE.IDLEUP:
                        SetAnimation(0);
                        break;
                }
            }
            
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }

        public override void ReverseMove()
        {
            switch (CURRENTSTATE)
            {
                case STATE.WALKDOWN:
                    destRec.Y--;
                    posy--;
                    //CURRENTSTATE = STATE.WALKUP;
                    break;
                case STATE.WALKLEFT:
                    destRec.X++;
                    posx++;
                    //CURRENTSTATE = STATE.WALKRIGHT;
                    break;
                case STATE.WALKRIGHT:
                    destRec.X--;
                    posx--;
                    //CURRENTSTATE = STATE.WALKLEFT;
                    break;
                case STATE.WALKUP:
                    destRec.Y++;
                    posy++;
                    //CURRENTSTATE = STATE.WALKDOWN;
                    break;
            }
        }

        ///ACTIONS from scripts
        ///
        public virtual bool IsFacing(Entity target)
        {
            if ((CURRENTSTATE == STATE.IDLEDOWN || CURRENTSTATE == STATE.WALKDOWN) && GetCollisionBox().Bottom <= target.GetCollisionBox().Top) return true;
            if ((CURRENTSTATE == STATE.IDLEUP || CURRENTSTATE == STATE.WALKUP) && GetCollisionBox().Top >= target.GetCollisionBox().Bottom) return true;
            if ((CURRENTSTATE == STATE.IDLELEFT || CURRENTSTATE == STATE.WALKLEFT) && GetCollisionBox().Left >= target.GetCollisionBox().Right) return true;
            if ((CURRENTSTATE == STATE.IDLERIGHT || CURRENTSTATE == STATE.WALKRIGHT) && GetCollisionBox().Right <= target.GetCollisionBox().Left) return true;

            return false;
        }


        
    }
}
