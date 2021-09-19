using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class GiveItemAction : EventAction
    {
        private bool ended = false;
        private Player player;
        private string name;
        public GiveItemAction(Player player, string name)
        {
            this.player = player;
            this.name = name;
        }

        public override void Trigger()
        {
            player.GiveItem(name);
            ended = true;
        }

        public override bool IsEnded()
        {
            return ended;
        }

        public override void Reset()
        {
            ended = false;
        }
    }
}
