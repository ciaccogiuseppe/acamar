using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class TeleportAction : EventAction
    {
        private World world;
        private Level level;
        private Character target;
        private int levelID;
        private int mapID;
        private int posX;
        private int posY;

        private TELETYPE type;

        private enum TELETYPE
        {
            TELELVL,
            TELEMAP,
            TELEPOS
        }

        public TeleportAction(World world, int levelID, int posX, int posY, Character target)
        {
            this.world = world;
            this.levelID = levelID;

            this.posX = posX;
            this.posY = posY;

            this.target = target;

            type = TELETYPE.TELELVL;
        }

        public TeleportAction(Level level, int mapID, int posX, int posY, Character target)
        {
            this.level = level;
            this.mapID = mapID;

            this.posX = posX;
            this.posY = posY;

            this.target = target;

            type = TELETYPE.TELEMAP;
        }

        public TeleportAction(int posX, int posY, Character target)
        {
            this.posX = posX;
            this.posY = posY;
            this.target = target;
            type = TELETYPE.TELEPOS;
        }

        public override void Trigger()
        {
            switch(type)
            {
                case TELETYPE.TELELVL:
                    world.SetLevel(levelID);
                    break;
                case TELETYPE.TELEMAP:
                    Map preMap = level.GetCurrentMap();
                    level.SetMap(mapID);
                    Map postMap = level.GetCurrentMap();
                    TransitionHandler.prevMap = preMap;
                    TransitionHandler.nextMap = postMap;
                    TransitionHandler.preTransition = TransitionHandler.LTORPRE;
                    TransitionHandler.postTransition = TransitionHandler.LTORPOST;
                    TransitionHandler.Activate();
                    break;
                default:
                    break;
            }
            target.SetPosition(posX, posY);
        }
        
    }
}
