using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
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
            private string name;
            private string value;
            private bool hasValue = false;
            private bool selected = false;

            public MenuEntry(string name)
            {
                this.name = name;
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
        }

        internal class MenuPage
        {
            private List<MenuEntry> entries = new List<MenuEntry>();
            private Font curFont;
            private Font selFont;
            private int currentEntry = 0;

            public MenuPage()
            {
                entries.Add(new MenuEntry("Entry 1"));
                entries.Add(new MenuEntry("Entry 2"));

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

            public void Update()
            {
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
            menuPages.Add(new MenuPage());
        }

        public void Draw(SpriteBatch batch)
        {
            menuPages[currentPage].Draw(batch);
        }

        internal void Update()
        {
            menuPages[currentPage].Update();
            if (MyKeyboard.IsPressedNotCont(Keys.Escape))
            {
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                menuPages[currentPage].Reset();
                currentPage = 0;
                menuPages[currentPage].Reset();
            }
        }
    }
}
