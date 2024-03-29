﻿using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class GiveItemAction : EventAction
    {
        //Player to give the item to
        private Player player;

        //Name of the item to give
        private string name;

        //Flag for action ended running
        private bool ended = false;

        public GiveItemAction(Player player, string name)
        {
            this.player = player;
            this.name = name;
        }

        //Activate action
        public override void Trigger()
        {
            player.GiveItem(name);
            ended = true;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            return ended;
        }

        //Reset action
        public override void Reset()
        {
            ended = false;
        }
    }
}
