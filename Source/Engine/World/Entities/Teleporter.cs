using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Teleporter : Entity
    {
        int sourceLevel;
        int destLevel;
        int sourceMap;
        int destMap;

        int destPosX;
        int destPosY;

        public Teleporter(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {

        }

        public void Teleport()
        {
            if (sourceLevel == destLevel)
                Globals.world.SetMap(destMap);
            else
            {
                Globals.world.SetLevel(destLevel);
                Globals.world.SetMap(destMap);
            }
        }

        public override void HandleCollision(Entity target)
        {
            Teleport();
            target.SetPosition(destPosX, destPosY);
        }
    }
}
