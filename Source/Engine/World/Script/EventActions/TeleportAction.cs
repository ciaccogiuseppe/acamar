using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class TeleportAction : EventAction
    {
        //TODO: Add transition type set option
        //TODO: Cleaner handling using only world and not level entity

        //Target: world > level > map > position
        private World world;
        private Level level;
        private int levelID;
        private int mapID;
        private int posX;
        private int posY;

        //Type of teleport (level/map/position)
        private TYPE type;

        //Character to teleport
        private Character target;

        //Flag for action started running
        private bool started = false;

        //Flag for action ended running
        private bool ended = false;

        

        private enum TYPE
        {
            TELELVL,
            TELEMAP,
            TELEPOS
        }

        //Teleport to map in different level
        public TeleportAction(World world, int levelID, int mapID, int posX, int posY, Character target)
        {
            this.world = world;
            this.levelID = levelID;
            this.mapID = mapID;

            this.posX = posX;
            this.posY = posY;

            this.target = target;

            type = TYPE.TELELVL;
        }

        //Teleport to map in same level
        public TeleportAction(Level level, int mapID, int posX, int posY, Character target)
        {
            this.level = level;
            this.mapID = mapID;

            this.posX = posX;
            this.posY = posY;

            this.target = target;

            type = TYPE.TELEMAP;
        }

        //Teleport in the same map
        public TeleportAction(int posX, int posY, Character target)
        {
            this.posX = posX;
            this.posY = posY;
            this.target = target;
            type = TYPE.TELEPOS;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            switch(type)
            {
                case TYPE.TELELVL:
                    world.SetLevel(levelID);
                    world.SetMap(mapID);
                    break;
                //TODO: better transition handling
                case TYPE.TELEMAP:
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

        //Check if action is ended
        public override bool IsEnded()
        {
            if (started && !TransitionHandler.IsActive())
            {
                ended = true;
            }
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            started = false;
            ended = false;
        }
    }
}
