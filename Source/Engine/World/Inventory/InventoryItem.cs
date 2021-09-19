using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Inventory
{
    class InventoryItem
    {
        private ItemConstants.ITEMS type;
        private Picture itemPicture;
        private int count = 0;

        public InventoryItem(ItemConstants.ITEMS type)
        {
            this.type = type;
        }

        public ItemConstants.ITEMS GetItemType()
        {
            return type;
        }

        public void Add()
        {
            count++;
        }

        public int GetCount()
        {
            return count;
        }

        public void Remove()
        {
            count--;
        }
    }
}
