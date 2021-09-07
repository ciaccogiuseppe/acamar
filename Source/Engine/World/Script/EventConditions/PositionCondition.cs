using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class PositionCondition : EventCondition
    {
        public enum POSTYPE
        {
            POSNEAR,
            POSTOUCH,
            POSTOUCHFACING
        }

        private POSTYPE type;
        private int posX;
        private int posY;
        private int radius;

        private Character target;

        private Character source;

        public PositionCondition(Character target, int posX, int posY, int radius, POSTYPE type)
        {
            this.target = target;
            this.posX = posX;
            this.posY = posY;
            this.radius = radius;

            this.type = type;
        }

        public PositionCondition(Character target, Character source, POSTYPE type)
        {
            this.target = target;
            this.source = source;
            this.type = type;
        }

        public override bool IsVerified()
        {
            switch(type)
            {
                case POSTYPE.POSNEAR:
                    if (
                        (posX - target.GetCenterX()) * (posX - target.GetCenterX()) +
                        (posY - target.GetCenterY()) * (posY - target.GetCenterY())
                        <= radius * radius) //change to GetCenterX, GetCenterY
                        return true;
                    break;
                case POSTYPE.POSTOUCH:
                    if (Touching(target.GetCollisionBox(), source.GetCollisionBox()))
                        return true;
                    break;
                case POSTYPE.POSTOUCHFACING:
                    if (Touching(target.GetCollisionBox(), source.GetCollisionBox()) && target.IsFacing(source))
                        return true;
                    break;
                
            }
            return false;
        }


        private bool Touching(Rectangle A, Rectangle B)
        {
            Rectangle aux = A;
            if (A.Intersects(B) || A.Contains(B) || B.Contains(A)) return false;
            A.X++;
            if (A.Intersects(B)) return true;
            A.X -= 2;
            if (A.Intersects(B)) return true;
            A.X++;

            A.Y++;
            if (A.Intersects(B)) return true;
            A.Y -= 2;
            if (A.Intersects(B)) return true;
            A.Y++;

            return false;
        }
    }
}
