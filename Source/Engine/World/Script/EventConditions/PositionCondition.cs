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
            POSTOUCH
        }

        private POSTYPE type;
        private int posX;
        private int posY;
        private int radius;

        Character conditionTarget;

        public PositionCondition(Character conditionTarget, int posX, int posY, int radius, POSTYPE type)
        {
            this.conditionTarget = conditionTarget;
            this.posX = posX;
            this.posY = posY;
            this.radius = radius;

            this.type = type;
        }

        public override bool IsVerified()
        {
            switch(type)
            {
                case POSTYPE.POSNEAR:
                    if (
                        (posX - conditionTarget.GetCenterX()) * (posX - conditionTarget.GetCenterX()) +
                        (posY - conditionTarget.GetCenterY()) * (posY - conditionTarget.GetCenterY())
                        <= radius * radius) //change to GetCenterX, GetCenterY
                        return true;
                    break;
                
            }
            return false;
        }
    }
}
