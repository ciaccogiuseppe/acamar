using acamar.Source.Engine.World;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    //TODO: REMOVE IF NOT USED
    class MenuPage
    {
        private List<MenuEntity> entities;
        private int entriesNumber;
        private int currentEntry;
        private int count;
        

        public MenuPage()
        {
            entities = new List<MenuEntity>();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Entity ent in entities)
            {
                ent.Draw(batch);
            }
        }

        public void Update()
        {
            //count = (count > 0)?((count + 1) % 15):0;
            //if (Keyboard.GetState().IsKeyDown(Keys.Down) && count == 0)
            //{
            //    count++;
            //    entities[currentEntry].Deselect();
            //    currentEntry = (currentEntry + 1) % entriesNumber;
            //    entities[currentEntry].Select();
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.Up) && count == 0)
            //{
            //    count++;
            //    entities[currentEntry].Deselect();
            //    if (currentEntry > 0)
            //        currentEntry = (currentEntry - 1);
            //    else
            //        currentEntry = entriesNumber - 1;
            //    entities[currentEntry].Select();
            //}


            //---------------------------------------------------------
            if (MyKeyboard.IsPressedNotCont(Keys.Down))
            {
                entities[currentEntry].Deselect();
                currentEntry = (currentEntry + 1) % entriesNumber;
                entities[currentEntry].Select();
            }

            else if (MyKeyboard.IsPressedNotCont(Keys.Up))
            {
                entities[currentEntry].Deselect();
                if (currentEntry > 0)
                    currentEntry = (currentEntry - 1);
                else
                    currentEntry = entriesNumber - 1;
                entities[currentEntry].Select();
            }
            //-----------------------------------------------------------z

            foreach (MenuEntity ent in entities)
            {
                ent.Update();
            }
        }

        public void AddButton(MenuButton button)
        {
            entities.Add(button);
            entriesNumber++;
        }
    }
}
