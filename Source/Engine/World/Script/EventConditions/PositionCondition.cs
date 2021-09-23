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

        //Condition type (near, touching, touching&facing)
        private POSTYPE type;

        //Position and distance (for near check)
        private int posX;
        private int posY;
        private int radius;

        //Character to check the condition on; condition subject
        private Character target;

        //Entity from which the target can be near/touching/touching&facing; condition object
        private Entity source;

        //Condition relative to fixed point
        public PositionCondition(Character target, int posX, int posY, int radius, POSTYPE type)
        {
            this.target = target;
            this.posX = posX;
            this.posY = posY;
            this.radius = radius;

            this.type = type;
        }

        //Condition relative to entity
        public PositionCondition(Character target, Entity source, POSTYPE type)
        {
            this.target = target;
            this.source = source;
            this.type = type;
        }


        //Check if condition is verified
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

        //Check if two rectangles are touching
        //TODO: can move in a utility library
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
