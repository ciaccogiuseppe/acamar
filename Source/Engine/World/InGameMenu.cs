using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
using acamar.Source.Engine.Settings;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World
{
    class InGameMenu
    {
        //pages

        internal class MenuEntry
        {
            public enum TYPE
            {
                NULL,
                CHANGEPAGE,
                CHANGEMAP,
                CHANGELEVEL,
                CHANGERESOLUTION,
                KEYSET,
                EXIT
            }
            protected string name;
            protected string value;
            protected TYPE type;
            protected bool hasValue = false;
            protected bool selected = false;
            protected int nextPage = -1;
            protected InGameMenu selfMenu;
            protected bool toUpdate = false;

            public MenuEntry(string name, TYPE type, InGameMenu selfMenu)
            {
                this.name = name;
                this.selfMenu = selfMenu;
                this.type = type;

                switch (type)
                {
                    case TYPE.CHANGERESOLUTION:
                        hasValue = true;
                        value = GlobalSettings.resolutionDict.GetValueOrDefault(GlobalSettings.CURRENTRESOLUTION);
                        break;
                }
            }

            public void SetValue(string value)
            {
                hasValue = true;
                this.value = value;
            }

            public override string ToString()
            {
                if (hasValue)
                    return name + " : " + value;
                else
                    return name;
            }

            public bool IsSelected()
            {
                return selected;
            }

            public void Select()
            {
                selected = true;
            }

            public void Deselect()
            {
                selected = false;
            }

            public void SetNextPage(int page)
            {
                nextPage = page;
            }

            public void Update()
            {
                switch (type)
                {
                    case TYPE.CHANGEPAGE:
                        break;
                    case TYPE.CHANGELEVEL:
                    case TYPE.CHANGEMAP:
                        if (MyKeyboard.IsPressedNotCont(Keys.Left))
                            value = (int.Parse(value) - 1).ToString();
                        else if (MyKeyboard.IsPressedNotCont(Keys.Right))
                            value = (int.Parse(value) + 1).ToString();
                        break;
                    case TYPE.CHANGERESOLUTION:
                        if (MyKeyboard.IsPressedNotCont(Keys.Left))
                            GlobalSettings.CURRENTRESOLUTION = (GlobalSettings.RESOLUTION)
                                    (((int)GlobalSettings.CURRENTRESOLUTION - 1) < 0 ?
                                    ((int)GlobalSettings.RESOLUTION.COUNT-1) :
                                    ((int)GlobalSettings.CURRENTRESOLUTION - 1));
                        else if (MyKeyboard.IsPressedNotCont(Keys.Right))
                            GlobalSettings.CURRENTRESOLUTION = (GlobalSettings.RESOLUTION)
                                    (((int)GlobalSettings.CURRENTRESOLUTION + 1) % (int)GlobalSettings.RESOLUTION.COUNT);
                        value = GlobalSettings.resolutionDict.GetValueOrDefault(GlobalSettings.CURRENTRESOLUTION);
                        GlobalSettings.SetResolution(GlobalSettings.CURRENTRESOLUTION);
                        break;
                    case TYPE.KEYSET:
                        if(toUpdate && !MyKeyboard.IsPressedNotCont(Globals.MENUKEY))
                        {
                            Keys k = Globals.MENUKEY;
                            foreach (Keys key in Keys.GetValues(typeof(Keys)))
                            {
                                if (MyKeyboard.IsPressedNotCont(key) && key != Keys.None) k = key;
                                    
                            }
                            if (k == Globals.MENUKEY) break;

                            if (!Globals.ASSIGNEDKEYS.Contains(k))
                            {
                                if (value == Globals.MOVEUP.ToString())
                                {
                                    Globals.ASSIGNEDKEYS.Remove(Globals.MOVEUP);
                                    Globals.MOVEUP = k;
                                    Globals.ASSIGNEDKEYS.Add(k);
                                    value = k.ToString();
                                }
                                else if (value == Globals.MOVEDOWN.ToString())
                                {
                                    Globals.ASSIGNEDKEYS.Remove(Globals.MOVEDOWN);
                                    Globals.MOVEDOWN = k;
                                    Globals.ASSIGNEDKEYS.Add(k);
                                    value = k.ToString();
                                }
                                else if (value == Globals.MOVELEFT.ToString())
                                {
                                    Globals.ASSIGNEDKEYS.Remove(Globals.MOVELEFT);
                                    Globals.MOVELEFT = k;
                                    Globals.ASSIGNEDKEYS.Add(k);
                                    value = k.ToString();
                                }
                                else if (value == Globals.MOVERIGHT.ToString())
                                {
                                    Globals.ASSIGNEDKEYS.Remove(Globals.MOVERIGHT);
                                    Globals.MOVERIGHT = k;
                                    Globals.ASSIGNEDKEYS.Add(k);
                                    value = k.ToString();
                                }
                            }
                            toUpdate = false;
                        }
                        break;
                }
            }

            public void Act()
            {
                switch (type)
                {
                    case TYPE.CHANGEPAGE:
                        //if(nextPage >= 0)
                        selfMenu.SetCurrentPage(nextPage);
                        break;
                    case TYPE.CHANGELEVEL:
                        Globals.world.SetLevel(int.Parse(value));
                        Globals.world.SetMap(0);
                        break;
                    case TYPE.CHANGEMAP:
                        Globals.world.SetMap(int.Parse(value));
                        break;
                    case TYPE.KEYSET:
                        toUpdate = true;
                        break;
                    case TYPE.EXIT:
                        Globals.CURRENTSTATE = Globals.STATE.EXIT;
                        break;
                }
                
            }
        }


        internal class MenuPage
        {
            private int parentPage = -1;
            private int number;
            private InGameMenu selfMenu;
            private List<MenuEntry> entries = new List<MenuEntry>();
            private Font curFont;
            private Font selFont;
            private int currentEntry = 0;

            public MenuPage(int number, int parentPage, InGameMenu selfMenu)
            {
                this.selfMenu = selfMenu;
                this.parentPage = parentPage;
                this.number = number;
                switch (number)
                {
                    case 0:
                        entries.Add(new MenuEntry("Entry 1",    MenuEntry.TYPE.NULL,        selfMenu));
                        entries.Add(new MenuEntry("Entry 2",    MenuEntry.TYPE.CHANGEPAGE,  selfMenu));
                        entries.Add(new MenuEntry("Level",      MenuEntry.TYPE.CHANGELEVEL, selfMenu));
                        entries.Add(new MenuEntry("Map",        MenuEntry.TYPE.CHANGEMAP,   selfMenu));
                        entries.Add(new MenuEntry("Inventory",  MenuEntry.TYPE.CHANGEPAGE,  selfMenu));
                        entries.Add(new MenuEntry("Settings",   MenuEntry.TYPE.CHANGEPAGE,  selfMenu));
                        entries.Add(new MenuEntry("Back",       MenuEntry.TYPE.CHANGEPAGE,  selfMenu));
                        entries.Add(new MenuEntry("Exit",       MenuEntry.TYPE.EXIT,        selfMenu));


                        entries[1].SetNextPage(1);
                        entries[2].SetValue("0");
                        entries[3].SetValue("0");
                        entries[4].SetNextPage(2);
                        entries[5].SetNextPage(3);
                        entries[6].SetNextPage(-1);
                        break;
                    case 1:
                        entries.Add(new MenuEntry("Entry 3", MenuEntry.TYPE.NULL, selfMenu));
                        break;
                    case 2:
                        List<string> inventory = Globals.player.GetInventory();
                        foreach(string s in inventory)
                        {
                            entries.Add(new MenuEntry(s, MenuEntry.TYPE.NULL, selfMenu));
                        }
                        entries.Add(new MenuEntry("Back", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries[entries.Count - 1].SetNextPage(parentPage);
                        break;
                    case 3:
                        entries.Add(new MenuEntry("Resolution", MenuEntry.TYPE.CHANGERESOLUTION, selfMenu));
                        entries.Add(new MenuEntry("Controls", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries[1].SetNextPage(4);
                        break;
                    case 4:
                        entries.Add(new MenuEntry("Move Up    ", MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry("Move Left  ", MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry("Move Right ", MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry("Move Down  ", MenuEntry.TYPE.KEYSET, selfMenu));

                        entries[0].SetValue(Globals.MOVEUP.ToString());
                        entries[1].SetValue(Globals.MOVELEFT.ToString());
                        entries[2].SetValue(Globals.MOVERIGHT.ToString());
                        entries[3].SetValue(Globals.MOVEDOWN.ToString());
                        break;
                }
                entries[0].Select();


                curFont = FontConstants.FONT1;
                selFont = FontConstants.FONT2;
            }

            public void Reset()
            {
                entries[currentEntry].Deselect();
                currentEntry = 0;
                entries[currentEntry].Select();
            }

            public int GetParent()
            {
                return parentPage;
            }

            public void Update()
            {
                if(number == 2) //inventory
                {
                    entries.Clear();
                    List<string> inventory = Globals.player.GetInventory();
                    foreach (string s in inventory)
                    {
                        entries.Add(new MenuEntry(s, MenuEntry.TYPE.NULL, selfMenu));
                    }
                    entries.Add(new MenuEntry("Back", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                    entries[entries.Count - 1].SetNextPage(parentPage);
                    entries[currentEntry].Select();
                }

                entries[currentEntry].Update();
                if (MyKeyboard.IsPressedNotCont(Keys.Down))
                {
                    entries[currentEntry].Deselect();
                    currentEntry = (currentEntry + 1) % entries.Count;
                    entries[currentEntry].Select();
                }

                else if (MyKeyboard.IsPressedNotCont(Keys.Up))
                {
                    entries[currentEntry].Deselect();
                    if (currentEntry > 0)
                        currentEntry = (currentEntry - 1);
                    else
                        currentEntry = entries.Count - 1;
                    entries[currentEntry].Select();
                }
                else if (MyKeyboard.IsPressedNotCont(Keys.Z))
                {
                    entries[currentEntry].Act();
                }
                else if (MyKeyboard.IsPressedNotCont(Keys.Escape))
                {
                    selfMenu.SetCurrentPage(selfMenu.GetCurrentPage().GetParent());
                }
            }

            public void Draw(SpriteBatch batch)
            {
                int posx = 10;
                int posy = 50;
                foreach(MenuEntry entry in entries)
                {
                    if(entry.IsSelected())
                        selFont.Draw(entry.ToString(), posx, posy, batch, 1);
                    else
                        curFont.Draw(entry.ToString(), posx, posy, batch, 1);
                    posy += 20;
                }
            }
        }

        private int currentPage;
        private List<MenuPage> menuPages;

        public InGameMenu()
        {
            menuPages = new List<MenuPage>();
            currentPage = 0;
            menuPages.Add(new MenuPage(0, -1, this));
            menuPages.Add(new MenuPage(1,  0, this));
            menuPages.Add(new MenuPage(2, 0, this));
            menuPages.Add(new MenuPage(3, 0, this));
            menuPages.Add(new MenuPage(4, 3, this));
        }

        public void Draw(SpriteBatch batch)
        {
            menuPages[currentPage].Draw(batch);
        }

        internal void Update()
        {
            menuPages[currentPage].Update();
            //if (MyKeyboard.IsPressedNotCont(Keys.Escape))
            //{
            //    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
            //    menuPages[currentPage].Reset();
            //    currentPage = 0;
            //    menuPages[currentPage].Reset();
            //}
        }

        public MenuPage GetCurrentPage()
        {
            return menuPages[currentPage];
        }

        public void SetCurrentPage(int page)
        {
            if(page == -1)
            {
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                menuPages[currentPage].Reset();
                currentPage = 0;
                menuPages[currentPage].Reset();
            }
            else
            {
                menuPages[currentPage].Reset();
                currentPage = page;
                menuPages[currentPage].Reset();
            }
        }
    }
}
