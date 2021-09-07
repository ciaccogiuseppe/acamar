using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World
{
    class MainMenu
    {
        public MainMenu()
        {
            this.Initialize();
        }

        internal class MenuEntity : Entity
        {
            public MenuEntity(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
            {

            }

            protected bool selected = false;
            
            public virtual void Select()
            {
                selected = true;
            }

            public virtual void Deselect()
            {
                selected = false;
            }
        }

        internal class Scroller : MenuEntity
        {

            private Entity scrollerTag; //name of scroller:   ScrollerTag      Options↕
            private int optionsNumber;
            private int currentOption;
            private bool active;

            public Scroller(int entid, int sprid, int posx, int posy) :
            base(entid, sprid, posx, posy, 0)
            {

            }

            public override void Draw(SpriteBatch batch)
            {
                scrollerTag.Draw(batch);
                base.Draw(batch);
            }

            public void MoveUp()
            {
                int animationLength = 0; //[TODO] see Entity
                currentAnimation = (currentAnimation - 1) % animationLength;
                if (currentOption != 0)
                    currentOption = (currentOption - 1);
                else
                    currentOption = optionsNumber - 1;
            }

            public void MoveDown()
            {
                int animationLength = 0; //[TODO] see Entity
                currentAnimation = (currentAnimation + 1) % animationLength;
                currentOption = (currentOption + 1) % optionsNumber;
            }
        }

        internal class Button : MenuEntity
        {
            int cnt = 0;
            int type;

            private bool active;
            public Button(int sprid, int posx, int posy) :
            base(-1, sprid, posx, posy, 0)
            {
                
            }

            public void SetType(int type)
            {
                this.type = type;
            }

            public override void Update()
            {
                //if(Keyboard.GetState().IsKeyDown(Keys.Enter) && type == 1 && selected)
                if(MyKeyboard.IsPressedNotCont(Keys.Z) && type == 1 && selected)
                {
                    Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                }
            }

            public override void Select()
            {
                if (selected == false)
                {
                    base.Select();
                    Animate();
                }
            }

            public override void Deselect()
            {
                if (selected == true)
                {
                    base.Deselect();
                    Animate();
                }
            }

            public void OnPress()
            {

            }
        }

        internal class Slider : MenuEntity
        {
            public Slider(int entid, int sprid, int posx, int posy) :
            base(entid, sprid, posx, posy, 0)
            {

            }
            private bool active;
            private Entity sliderBar;
            private Entity sliderTag; //name of slider:   SliderTag       |------<>-----------|
            private int step;
            private int min;
            private int max;
            private int current;

            public override void Draw(SpriteBatch batch)
            {
                sliderTag.Draw(batch);
                sliderBar.Draw(batch);
                base.Draw(batch);
            }

            public void MoveLeft()
            {
                if(current > min)
                {
                    current -= step;
                }
            }

            public void MoveRight()
            {
                if(current < max)
                {
                    current += step;
                }
            }
        }

        internal class MenuPage
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
                if(MyKeyboard.IsPressedNotCont(Keys.Down))
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

            public void AddButton(Button button)
            {
                entities.Add(button);
                entriesNumber++;
            }
        }

        private int currentPage = 0;
        private List<MenuPage> menuPages;
        private Entity background;
        private Entity title;

        public void Draw(SpriteBatch batch)
        {
            background.Draw(batch);
            title.Draw(batch);
            menuPages[currentPage].Draw(batch);
        }

        public void GoToPage(int pageNumber)
        {
            currentPage = pageNumber;
        }

        internal void Update()
        {
            menuPages[currentPage].Update();
        }

        public void Initialize()
        {
            background = new MenuEntity(0, 0, 0, 0, 0);
            menuPages = new List<MenuPage>();

            title = new Entity(0, 2, 50, 20, 0);

            //PAGE 0
            menuPages.Add(new MenuPage());
            Button newGame = new Button(1, 150, 175);
            newGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            newGame.SetType(1);
            menuPages[0].AddButton(newGame);
            newGame.Select();

            Button loadGame = new Button(3, 150, 230);
            loadGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            menuPages[0].AddButton(loadGame);

            Button options = new Button(4, 150, 285);
            options.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            menuPages[0].AddButton(options);
        }
    }
}
