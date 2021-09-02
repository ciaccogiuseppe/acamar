using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class MoveAction : EventAction
    {
        private int destX;
        private int destY;
        private Character target;

        public MoveAction(int destX, int destY, Character target)
        {
            this.destX = destX;
            this.destY = destY;
            this.target = target;
        }

        public override void Trigger()
        {
            if (destX == target.GetPosX()) target.MoveToY(destY);
            else if (destY == target.GetPosY()) target.MoveToX(destX);
        }

    }
}
