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

        private bool started = false;
        private bool ended = false;

        private TELETYPE type;

        private enum TELETYPE
        {
            TELELVL,
            TELEMAP,
            TELEPOS
        }

        public TeleportAction(World world, int levelID, int mapID, int posX, int posY, Character target)
        {
            this.world = world;
            this.levelID = levelID;
            this.mapID = mapID;

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
            started = true;
            switch(type)
            {
                case TELETYPE.TELELVL:
                    world.SetLevel(levelID);
                    world.SetMap(mapID);
                    break;
                case TELETYPE.TELEMAP:
                    Map preMap = level.GetCurrentMap();
                    level.SetMap(mapID);
                    Map postMap = level.GetCurrentMap();
                    TransitionHandler.prevMap = preMap;
                    TransitionHandler.nextMap = postMap;
                    TransitionHandler.preTransition = TransitionHandler.FADEPRE;
                    TransitionHandler.postTransition = TransitionHandler.FADEPOST;
                    TransitionHandler.Activate();
                    break;
                default:
                    break;
            }
            target.SetPosition(posX, posY);
        }

        public override bool IsEnded()
        {
            if (started && !TransitionHandler.IsActive())
            {
                ended = true;
            }
            return ended;
        }

        public override void Reset()
        {
            started = false;
            ended = false;
        }
    }
}
