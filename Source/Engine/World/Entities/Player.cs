﻿using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Entities.LightSources;
using acamar.Source.Engine.World.Inventory;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    public class Player : Character
    {
        private int nextPosx;
        private int nextPosy;
        private List<InventoryItem> inventory = new List<InventoryItem>();
        private List<ItemConstants.ITEMS> itemsInInventory = new List<ItemConstants.ITEMS>();

        private LightSource torch;

        public Player(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {
            animActive = true;

            //torch = new LightSource(posx, posy, 10000);
            torch = new Sonar(posx, posy, 150);
        }

        public override void StateMachine()
        {

        }

        public override void ReverseMove()
        {
            switch (CURRENTSTATE)
            {
                case STATE.WALKDOWN:
                    destRec.Y--;
                    posy--;
                    Globals.CAMY += 1;// (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKLEFT:
                    destRec.X++;
                    posx++;
                    Globals.CAMX -= 1;// (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKRIGHT:
                    destRec.X--;
                    posx--;
                    Globals.CAMX += 1;// (int)Globals.SCALE;
                    Stop();
                    break;
                case STATE.WALKUP:
                    destRec.Y++;
                    posy++;
                    Globals.CAMY -= 1;// (int)Globals.SCALE;
                    Stop();
                    break;
            }
        }

        public override void HandleCollision(Entity target)
        {
            if (!target.IsCollidable() || !collidable) return;
            if (IsMoving())
            {
                ReverseMove();
            }
        }

        public void Interact(Interactable target)
        {
            if (IsNear(target) && IsFacing(target))
            {
                target.OnInteract();
            }
        }

        public override void Update()
        {
            if (fading)
                opacity += fadeStep;
            if (fading && (opacity < 0 || opacity > 1))
            {
                fading = false;
                opacity = (opacity < 0) ? 0 : 1;
            }

            if (!locked)
            {
                if (Keyboard.GetState().IsKeyDown(Globals.MOVELEFT))
                {
                    MoveLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Globals.MOVERIGHT))
                {
                    MoveRight();
                }
                else if (Keyboard.GetState().IsKeyDown(Globals.MOVEUP))
                {
                    MoveUp();
                }
                else if (Keyboard.GetState().IsKeyDown(Globals.MOVEDOWN))
                {
                    MoveDown();
                }
                else
                {
                    Stop();
                }

                //DEBUG 
                if(Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Globals.world.Reset();
                }

                //DEBUG

                if (IsMoving())
                {
                    switch (CURRENTSTATE)
                    {
                        case STATE.WALKDOWN:
                            destRec.Y++;
                            Globals.CAMY-= 1;// (int)Globals.SCALE;
                            posy++;
                            break;
                        case STATE.WALKUP:
                            destRec.Y--;
                            Globals.CAMY+= 1;// (int)Globals.SCALE;
                            posy--;
                            break;
                        case STATE.WALKLEFT:
                            destRec.X--;
                            Globals.CAMX+= 1;// (int)Globals.SCALE;
                            posx--;
                            break;
                        case STATE.WALKRIGHT:
                            destRec.X++;
                            Globals.CAMX -= 1;// (int)Globals.SCALE;
                            posx++;
                            break;
                    }
                }

                Animate();
            }

            collRec.X = destRec.X + cPosx;
            collRec.Y = destRec.Y + cPosy;

            torch.SetPosition(GetCenterX(), GetCenterY());
            if (CURRENTSTATE == STATE.IDLEDOWN || CURRENTSTATE == STATE.WALKDOWN)
            {
                torch.SetDirection(LightSource.DIRECTION.DOWN);
                //torch.SetPosition(GetCenterX(), collRec.Bottom);
            }
            if (CURRENTSTATE == STATE.IDLEUP || CURRENTSTATE == STATE.WALKUP)
            {
                torch.SetDirection(LightSource.DIRECTION.UP);
                //torch.SetPosition(GetCenterX(), collRec.Top);
            }
                
            if (CURRENTSTATE == STATE.IDLELEFT || CURRENTSTATE == STATE.WALKLEFT)
            {
                torch.SetDirection(LightSource.DIRECTION.LEFT);
                //torch.SetPosition(collRec.Left, GetCenterY());
            }

            if (CURRENTSTATE == STATE.IDLERIGHT || CURRENTSTATE == STATE.WALKRIGHT)
            {
                torch.SetDirection(LightSource.DIRECTION.RIGHT);
                //torch.SetPosition(collRec.Right, GetCenterY());
            }
        }

        public LightSource GetLight()
        {
            return torch;
        }

        public override void SetPosition(int posx, int posy)
        {
            if(TransitionHandler.IsActive())
            {
                nextPosx = posx;
                nextPosy = posy;
            }
            else
            {
                base.SetPosition(posx, posy);
                Globals.CAMX = Globals.GSIZEX / 2 - posx;// * (int)Globals.SCALE;
                Globals.CAMY = Globals.GSIZEY / 2 - posy;// * (int)Globals.SCALE;
            }
        }

        public void UpdatePosition()
        {
            base.SetPosition(nextPosx, nextPosy);
            Globals.CAMX = Globals.GSIZEX / 2 - posx;// * (int)Globals.SCALE;
            Globals.CAMY = Globals.GSIZEY / 2 - posy;// * (int)Globals.SCALE;
        }

        public override string ToString()
        {
            return posx + " " + posy + " " + dir;
        }

        public bool HasItem(string name)
        {
            ItemConstants.ITEMS type = ItemConstants.itemDict.GetValueOrDefault(name);
            return itemsInInventory.Contains(type);
        }

        public void RemoveItem(string name)
        {
            ItemConstants.ITEMS type = ItemConstants.itemDict.GetValueOrDefault(name);
            if (itemsInInventory.Contains(type))
            {
                foreach (InventoryItem item in inventory)
                {
                    if (item.GetItemType() == type)
                    {
                        int count = item.GetCount();
                        if(count > 1)
                        {
                            item.Remove();
                        }
                        else if (count == 1)
                        {
                            item.Remove();
                            inventory.Remove(item);
                            itemsInInventory.Remove(type);
                        }
                    }
                }
            }
        }

        public void GiveItem(string name)
        {
            ItemConstants.ITEMS type = ItemConstants.itemDict.GetValueOrDefault(name);
            if(itemsInInventory.Contains(type))
            {
                foreach(InventoryItem item in inventory)
                {
                    if(item.GetItemType() == type)
                    {
                        item.Add();
                    }
                }
            }
            else
            {
                InventoryItem item = new InventoryItem(type, ItemConstants.itemNames.GetValueOrDefault(name));
                inventory.Add(item);
                item.Add();
                itemsInInventory.Add(type);
            }
        }

        public List<string> GetInventory()
        {
            List<string> res = new List<string>();

            foreach(InventoryItem item in inventory)
            {
                res.Add(item.GetName() + " : " + item.GetCount());
            }
            return res;
        }

        public List<InventoryItem> GetInventoryItems()
        {
            return inventory;
        }

        public void Reset()
        {
            inventory.Clear();
            itemsInInventory.Clear();
        }

        public int GetDir()
        {
            return dir;
        }

        public void SetInventory(List<InventoryItem> inventory)
        {
            this.inventory.Clear();
            itemsInInventory.Clear();
            foreach(InventoryItem item in inventory)
            {
                this.inventory.Add(item);
                itemsInInventory.Add(item.GetItemType());
            }
        }
    }

    
    
}
