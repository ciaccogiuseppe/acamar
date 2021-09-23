using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Inventory
{
    public class InventoryItem
    {
        private ItemConstants.ITEMS type;
        private Picture itemPicture;
        private int count = 0;
        private string name;

        public InventoryItem(ItemConstants.ITEMS type, string name, int count)
        {
            this.type = type;
            this.name = name;
            this.count = count;
        }

        internal void Use()
        {
            throw new NotImplementedException();
        }

        public InventoryItem(ItemConstants.ITEMS type, string name)
        {
            this.type = type;
            this.name = name;
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

        public string GetName()
        {
            return name;
        }

        public string GetItemName()
        {
            return ItemConstants.names.GetValueOrDefault(type);
        }
    }
}
