﻿using acamar.Source.Engine.World.Entities;
using acamar.Source.Engine.World.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class UseItemAction : EventAction
    {
        //TODO: implement InventoryItem.Use() method
        
        //Item to use
        private InventoryItem item;

        //Flag for action ended running
        private bool started = false;
        private bool ended = false;


        public UseItemAction(InventoryItem item)
        {
            this.item = item;
        }

        //Activate action
        public override void Trigger()
        {
            started = true;
            item.Use();
            ended = true;
        }

        //Reset action
        public override void Reset()
        {
            started = false;
            ended = false;
        }

        //Check if action is ended
        public override bool IsEnded()
        {
            return ended;
        }

        public override bool IsStarted()
        {
            return started;
        }

        public override bool GetEnded()
        {
            return ended;
        }
    }
}
