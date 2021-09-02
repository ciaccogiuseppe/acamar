using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Player : Character
    {
        public Player(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {

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
                    Stop();
                    break;
                case STATE.WALKLEFT:
                    destRec.X++;
                    Stop();
                    break;
                case STATE.WALKRIGHT:
                    destRec.X--;
                    Stop();
                    break;
                case STATE.WALKUP:
                    destRec.Y++;
                    Stop();
                    break;
            }
        }

        public override void HandleCollision(Entity target)
        {
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
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
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



            if (IsMoving())
            {
                switch (CURRENTSTATE)
                {
                    case STATE.WALKDOWN:
                        destRec.Y++;
                        posy++;
                        break;
                    case STATE.WALKUP:
                        destRec.Y--;
                        posy--;
                        break;
                    case STATE.WALKLEFT:
                        destRec.X--;
                        posx--;
                        break;
                    case STATE.WALKRIGHT:
                        destRec.X++;
                        posx++;
                        break;
                }
            }

            Animate();


        }
    }

    
}
