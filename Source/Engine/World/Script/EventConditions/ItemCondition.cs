﻿using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventConditions
{
    class ItemCondition : EventCondition
    {
        private Player player;
        private string item;
        private TYPE type;

        public enum TYPE
        {
            HASITEM,
            HASNOTITEM
        }

        public ItemCondition(Player player, string item, TYPE type)
        {
            this.player = player;
            this.item = item;
            this.type = type;
        }

        public override bool IsVerified()
        {
            bool verified = false;
            switch(type)
            {
                case TYPE.HASITEM:
                    verified = player.HasItem(item);
                    break;
                case TYPE.HASNOTITEM:
                    verified = !player.HasItem(item);
                    break;
            }
            return verified;
        }
    }
}
