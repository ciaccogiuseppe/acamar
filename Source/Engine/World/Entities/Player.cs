using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Entities.LightSources;
using acamar.Source.Engine.World.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Penumbra;
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

        public Player(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {
            animActive = true;

            animationLength[0] = 5;
            animationLength[1] = 5;
            animationLength[2] = 5;
            animationLength[3] = 5;
            animationLength[4] = 5;

            animationStep[0] = 1;
            animationStep[1] = 1;
            animationStep[2] = 1;
            animationStep[3] = 1;
            animationStep[4] = 1;

            animationLoop[0] = true;
            animationLoop[1] = true;
            animationLoop[2] = true;
            animationLoop[3] = true;
            animationLoop[4] = true;
        }

#if PENUMBRA
        public PointLight Light { get; } = new PointLight
        {
            Scale = new Vector2(400f), // Range of the light source (how far the light will travel)
            Color = Color.Orange,
            ShadowType = ShadowType.Occluded
        };
#endif

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
#if PENUMBRA
            Main.penumbra.Lights.Remove(Light);
            Main.penumbra.Lights.Add(Light);
#endif

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


#if PENUMBRA
            Random r = new Random();
            Light.Scale = new Vector2(400f + r.Next(1,59));
            Light.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) ;
#endif

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
