using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
using acamar.Source.Engine.MainMenu;
using acamar.Source.Engine.Settings;
using acamar.Source.Engine.Text;
using acamar.Source.Engine.World.Script;
using acamar.Source.Engine.World.Script.Prompts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World
{
    public class MainMenu
    {
        //public MainMenu()
        //{
        //    this.Initialize();
        //}


        //private int currentPage = 0;
        //private List<MenuPage> menuPages;
        //private Entity background;
        //private Entity title;

        //public void Draw(SpriteBatch batch)
        //{
        //    background.Draw(batch);
        //    title.Draw(batch);
        //    menuPages[currentPage].Draw(batch);
        //}

        //public void GoToPage(int pageNumber)
        //{
        //    currentPage = pageNumber;
        //}

        //internal void Update()
        //{
        //    menuPages[currentPage].Update();
        //}

        //public void Initialize()
        //{
        //    background = new MenuEntity();
        //    background.SetSourceRectangle(new Rectangle(0, 0, 400, 400));
        //    menuPages = new List<MenuPage>();

        //    title = new Entity(0, 2, 50, 20, 0);
        //    //title.SetSourceRectangle(new Rectangle(0, 0, 300, 100));

        //    //PAGE 0
        //    menuPages.Add(new MenuPage());
        //    MenuButton newGame = new MenuButton();
        //    newGame.SetSprite(1);
        //    newGame.SetPosition(150, 175);
        //    newGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
        //    newGame.SetType(MenuButton.TYPE.NEWGAME);
        //    menuPages[0].AddButton(newGame);
        //    newGame.Select();

        //    MenuButton loadGame = new MenuButton();
        //    loadGame.SetSprite(3);
        //    loadGame.SetPosition(150, 230);
        //    loadGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
        //    loadGame.SetType(MenuButton.TYPE.LOADGAME);
        //    loadGame.SetNextPage(1);
        //    menuPages[0].AddButton(loadGame);

        //    MenuButton options = new MenuButton();
        //    options.SetSprite(4);
        //    options.SetPosition(150, 285);
        //    options.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
        //    options.SetType(MenuButton.TYPE.OPTIONS);
        //    options.SetNextPage(1);
        //    menuPages[0].AddButton(options);

        //    //PAGE 1
        //    menuPages.Add(new MenuPage());
        //    MenuButton newGame2 = new MenuButton();
        //    newGame2.SetSprite(1);
        //    newGame2.SetPosition(150, 175);
        //    newGame2.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
        //    newGame2.SetType(MenuButton.TYPE.NEWGAME);
        //    menuPages[1].AddButton(newGame2);
        //    newGame2.Select();
        //}

        internal class MenuEntry
        {
            public enum TYPE
            {
                NULL,
                NEWGAME,
                CHANGEPAGE,
                CHANGEMAP,
                CHANGELEVEL,
                CHANGERESOLUTION,
                KEYSET,
                SAVESLOT,
                NEWSLOT,
                EXIT
            }
            protected string name;
            protected string value;
            protected TYPE type;
            protected bool hasValue = false;
            protected bool selected = false;
            protected int nextPage = -1;
            protected int saveSlot = 0;
            protected MainMenu selfMenu;
            protected bool toUpdate = false;
            protected bool waiting = false;

            public MenuEntry(string name, TYPE type, MainMenu selfMenu)
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

                switch (type)
                {
                    case TYPE.SAVESLOT:
                    case TYPE.NEWSLOT:
                        int slot = int.Parse(value);
                        saveSlot = slot;
                        switch(slot)
                        {
                            case 1:
                                Globals.SLOT1.Load();
                                this.value = Globals.SLOT1.ToString();
                                break;
                            case 2:
                                Globals.SLOT2.Load();
                                this.value = Globals.SLOT2.ToString();
                                break;
                            case 3:
                                Globals.SLOT3.Load();
                                this.value = Globals.SLOT3.ToString();
                                break;
                        }
                        break;
                }
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
                    case TYPE.SAVESLOT:
                    case TYPE.NEWSLOT:
                        switch (saveSlot)
                        {
                            case 1:
                                Globals.SLOT1.Load();
                                value = Globals.SLOT1.ToString();
                                if(waiting)
                                {
                                    waiting = false;
                                    if (Globals.PROMPTRESULT == 0)
                                    {
                                        Globals.SLOT1.Delete();
                                        Globals.CURRENTSAVESLOT = 1;
                                        Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                        Flag.ResetFlags();
                                        Globals.world.SetLevel(0);
                                        Globals.world.SetMap(0);
                                        Globals.world.Reset();
                                        Globals.player.Reset();
                                        Globals.runningTime = System.TimeSpan.Zero;
                                    }

                                }
                                break;
                            case 2:
                                Globals.SLOT2.Load();
                                value = Globals.SLOT2.ToString();
                                if(waiting)
                                {
                                    waiting = false;
                                    if (Globals.PROMPTRESULT == 0)
                                    {
                                        Globals.SLOT2.Delete();
                                        Globals.CURRENTSAVESLOT = 2;
                                        Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                        Flag.ResetFlags();
                                        Globals.world.SetLevel(0);
                                        Globals.world.SetMap(0);
                                        Globals.world.Reset();
                                        Globals.player.Reset();
                                        Globals.runningTime = System.TimeSpan.Zero;
                                    }
                                }
                                break;
                            case 3:
                                Globals.SLOT3.Load();
                                value = Globals.SLOT3.ToString();
                                if(waiting)
                                {
                                    waiting = false;
                                    if (Globals.PROMPTRESULT == 0)
                                    {
                                        Globals.SLOT3.Delete();
                                        Globals.CURRENTSAVESLOT = 3;
                                        Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                        Flag.ResetFlags();
                                        Globals.world.SetLevel(0);
                                        Globals.world.SetMap(0);
                                        Globals.world.Reset();
                                        Globals.player.Reset();
                                        Globals.runningTime = System.TimeSpan.Zero;
                                    }
                                }
                                break;
                        }
                        break;
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
                                    ((int)GlobalSettings.RESOLUTION.COUNT - 1) :
                                    ((int)GlobalSettings.CURRENTRESOLUTION - 1));
                        else if (MyKeyboard.IsPressedNotCont(Keys.Right))
                            GlobalSettings.CURRENTRESOLUTION = (GlobalSettings.RESOLUTION)
                                    (((int)GlobalSettings.CURRENTRESOLUTION + 1) % (int)GlobalSettings.RESOLUTION.COUNT);
                        value = GlobalSettings.resolutionDict.GetValueOrDefault(GlobalSettings.CURRENTRESOLUTION);
                        GlobalSettings.SetResolution(GlobalSettings.CURRENTRESOLUTION);
                        break;
                    case TYPE.KEYSET:
                        if (toUpdate && !MyKeyboard.IsPressedNotCont(Globals.MENUKEY))
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

            public void Reset()
            {
                switch(type)
                {
                    case TYPE.SAVESLOT:
                    case TYPE.NEWSLOT:
                        switch (saveSlot)
                        {
                            case 1:
                                Globals.SLOT1.Load();
                                value = Globals.SLOT1.ToString();
                                break;
                            case 2:
                                Globals.SLOT2.Load();
                                value = Globals.SLOT2.ToString();
                                break;
                            case 3:
                                Globals.SLOT3.Load();
                                value = Globals.SLOT3.ToString();
                                break;
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
                    case TYPE.NEWGAME:
                        Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                        Flag.ResetFlags();
                        Globals.world.SetLevel(0);
                        Globals.world.SetMap(0);
                        Globals.world.Reset();
                        Globals.player.Reset();
                        Globals.runningTime = System.TimeSpan.Zero;
                        break;
                    case TYPE.EXIT:
                        Globals.CURRENTSTATE = Globals.STATE.EXIT;
                        break;
                    case TYPE.NEWSLOT:
                        switch (saveSlot)
                        {
                            case 1:
                                Globals.SLOT1.Load();
                                if(Globals.SLOT1.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 1;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                    Flag.ResetFlags();
                                    Globals.world.SetLevel(0);
                                    Globals.world.SetMap(0);
                                    Globals.world.Reset();
                                    Globals.player.Reset();
                                    Globals.runningTime = System.TimeSpan.Zero;
                                    //TODO:
                                    //MAKE CODE ^ ONCE AND NOT REPEATED FOR ONLY SLOT TODO
                                }
                                else
                                {
                                    //PromptHandler.PREVSTATE = Globals.STATE.MAINMENU;
                                    PromptHandler.currentPrompt = new MenuPrompt(TextBank.GetStringFromBank(15), new List<string> { TextBank.GetStringFromBank(13), TextBank.GetStringFromBank(14) });
                                    PromptHandler.Activate();
                                    waiting = true;
                                }
                                break;
                            case 2:
                                Globals.SLOT2.Load();
                                if (Globals.SLOT2.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 2;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                    Flag.ResetFlags();
                                    Globals.world.SetLevel(0);
                                    Globals.world.SetMap(0);
                                    Globals.world.Reset();
                                    Globals.player.Reset();
                                    Globals.runningTime = System.TimeSpan.Zero;
                                }
                                else
                                {
                                    //PromptHandler.PREVSTATE = Globals.STATE.MAINMENU;
                                    PromptHandler.currentPrompt = new MenuPrompt(TextBank.GetStringFromBank(15), new List<string> { TextBank.GetStringFromBank(13), TextBank.GetStringFromBank(14) });
                                    PromptHandler.Activate();
                                    waiting = true;
                                }
                                break;
                            case 3:
                                Globals.SLOT3.Load();
                                if (Globals.SLOT3.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 3;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                    Flag.ResetFlags();
                                    Globals.world.SetLevel(0);
                                    Globals.world.SetMap(0);
                                    Globals.world.Reset();
                                    Globals.player.Reset();
                                    Globals.runningTime = System.TimeSpan.Zero;
                                }
                                else
                                {
                                    //PromptHandler.PREVSTATE = Globals.STATE.MAINMENU;
                                    PromptHandler.currentPrompt = new MenuPrompt(TextBank.GetStringFromBank(15), new List<string> { TextBank.GetStringFromBank(13), TextBank.GetStringFromBank(14) });
                                    PromptHandler.Activate();
                                    waiting = true;
                                }
                                break;
                        }
                        break;
                    case TYPE.SAVESLOT:
                        switch(saveSlot)
                        {
                            case 1:
                                Globals.SLOT1.Load();
                                Globals.SLOT1.Apply();
                                if (!Globals.SLOT1.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 1;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                }
                                break;
                            case 2:
                                Globals.SLOT2.Load();
                                Globals.SLOT2.Apply();
                                if (!Globals.SLOT2.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 2;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                }
                                break;
                            case 3:
                                Globals.SLOT3.Load();
                                Globals.SLOT3.Apply();
                                if (!Globals.SLOT3.IsEmpty())
                                {
                                    Globals.CURRENTSAVESLOT = 3;
                                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                                }
                                break;
                        }

                        break;
                }

            }
        }


        public class MenuPage
        {
            private int parentPage = -1;
            private int number;
            private MainMenu selfMenu;
            private List<MenuEntry> entries = new List<MenuEntry>();
            private Font curFont;
            private Font selFont;
            private int currentEntry = 0;

            public MenuPage(int number, int parentPage, MainMenu selfMenu)
            {
                this.selfMenu = selfMenu;
                this.parentPage = parentPage;
                this.number = number;
                switch (number)
                {
                    case 0:
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(0), MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(1), MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        //entries.Add(new MenuEntry("  Level", MenuEntry.TYPE.CHANGELEVEL, selfMenu));
                        //entries.Add(new MenuEntry("   Map", MenuEntry.TYPE.CHANGEMAP, selfMenu));
                        //entries.Add(new MenuEntry("Inventory", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(2), MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        //entries.Add(new MenuEntry("  Back", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(3), MenuEntry.TYPE.EXIT, selfMenu));

                        entries[0].SetNextPage(2);
                        entries[1].SetNextPage(1);
                        //entries[2].SetValue("0");
                        //entries[3].SetValue("0");
                        //entries[4].SetNextPage(2);
                        entries[2].SetNextPage(3);
                        entries[3].SetNextPage(-1);
                        break;
                    case 1:
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(4), MenuEntry.TYPE.SAVESLOT, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(5), MenuEntry.TYPE.SAVESLOT, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(6), MenuEntry.TYPE.SAVESLOT, selfMenu));

                        entries[0].SetValue("1");
                        entries[1].SetValue("2");
                        entries[2].SetValue("3");
                        break;
                    case 2:
                        //List<string> inventory = Globals.player.GetInventory();
                        //foreach (string s in inventory)
                        //{
                        //    entries.Add(new MenuEntry(s, MenuEntry.TYPE.NULL, selfMenu));
                        //}
                        //entries.Add(new MenuEntry("Back", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        //entries[entries.Count - 1].SetNextPage(parentPage);
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(4), MenuEntry.TYPE.NEWSLOT, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(5), MenuEntry.TYPE.NEWSLOT, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(6), MenuEntry.TYPE.NEWSLOT, selfMenu));

                        entries[0].SetValue("1");
                        entries[1].SetValue("2");
                        entries[2].SetValue("3");
                        break;
                    case 3:
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(7), MenuEntry.TYPE.CHANGERESOLUTION, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(8), MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                        entries[1].SetNextPage(4);
                        break;
                    case 4:
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(9), MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(10), MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(11), MenuEntry.TYPE.KEYSET, selfMenu));
                        entries.Add(new MenuEntry(TextBank.GetStringFromBank(12), MenuEntry.TYPE.KEYSET, selfMenu));

                        entries[0].SetValue(Globals.MOVEUP.ToString());
                        entries[1].SetValue(Globals.MOVELEFT.ToString());
                        entries[2].SetValue(Globals.MOVERIGHT.ToString());
                        entries[3].SetValue(Globals.MOVEDOWN.ToString());
                        break;
                }
                entries[0].Select();


                curFont = FontConstants.FONT4;
                selFont = FontConstants.FONT5;
            }

            public void Reset()
            {
                foreach(MenuEntry entry in entries)
                {
                    entry.Reset();
                }
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
                //if (number == 2) //inventory
                //{
                //    entries.Clear();
                //    List<string> inventory = Globals.player.GetInventory();
                //    foreach (string s in inventory)
                //    {
                //        entries.Add(new MenuEntry(s, MenuEntry.TYPE.NULL, selfMenu));
                //    }
                //    entries.Add(new MenuEntry("Back", MenuEntry.TYPE.CHANGEPAGE, selfMenu));
                //    entries[entries.Count - 1].SetNextPage(parentPage);
                //    entries[currentEntry].Select();
                //}


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
                int posx = 20;
                int posy = 200;
                foreach (MenuEntry entry in entries)
                {
                    if (entry.IsSelected())
                        selFont.Draw(entry.ToString(), posx, posy, batch, 1);
                    else
                        curFont.Draw(entry.ToString(), posx, posy, batch, 1);
                    posy += 25;
                }
            }
        }

        private int currentPage;
        private List<MenuPage> menuPages;
        private Entity background;

        public MainMenu()
        {

            background = new MenuEntity();
            background.SetSourceRectangle(new Rectangle(0, 0, 400, 400));
            menuPages = new List<MenuPage>();
            currentPage = 0;
            menuPages.Add(new MenuPage(0, -1, this));
            menuPages.Add(new MenuPage(1, 0, this));
            menuPages.Add(new MenuPage(2, 0, this));
            menuPages.Add(new MenuPage(3, 0, this));
            menuPages.Add(new MenuPage(4, 3, this));
        }

        public void Draw(SpriteBatch batch)
        {
            background.Draw(batch);
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
            if (page == -1)
            {
                Globals.CURRENTSTATE = Globals.STATE.EXIT;
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
