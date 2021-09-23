using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class MoveAction : EventAction
    {
        //Destination position
        private int destX;
        private int destY;

        //Character moving to destination
        private Character target;

        //Flag for action ended running
        private bool ended = false;

        public MoveAction(int destX, int destY, Character target)
        {
            this.destX = destX;
            this.destY = destY;
            this.target = target;
        }

        //Activate action
        public override void Trigger()
        {
            if (destX == target.GetPosX()) target.MoveToY(destY);
            else if (destY == target.GetPosY()) target.MoveToX(destX);
        }
        
        //Check if action is ended
        public override bool IsEnded()
        {
            if (target.GetPosX() == destX && target.GetPosY() == destY) ended = true;
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            ended = false;
        }
    }
}
