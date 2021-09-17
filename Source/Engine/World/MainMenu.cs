using acamar.Source.Engine.MainMenu;
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
        public MainMenu()
        {
            this.Initialize();
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
            background = new MenuEntity();
            background.SetSourceRectangle(new Rectangle(0, 0, 400, 400));
            menuPages = new List<MenuPage>();

            title = new Entity(0, 2, 50, 20, 0);
            //title.SetSourceRectangle(new Rectangle(0, 0, 300, 100));

            //PAGE 0
            menuPages.Add(new MenuPage());
            MenuButton newGame = new MenuButton();
            newGame.SetSprite(1);
            newGame.SetPosition(150, 175);
            newGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            newGame.SetType(MenuButton.TYPE.NEWGAME);
            menuPages[0].AddButton(newGame);
            newGame.Select();

            MenuButton loadGame = new MenuButton();
            loadGame.SetSprite(3);
            loadGame.SetPosition(150, 230);
            loadGame.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            loadGame.SetType(MenuButton.TYPE.LOADGAME);
            loadGame.SetNextPage(1);
            menuPages[0].AddButton(loadGame);

            MenuButton options = new MenuButton();
            options.SetSprite(4);
            options.SetPosition(150, 285);
            options.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            options.SetType(MenuButton.TYPE.OPTIONS);
            options.SetNextPage(1);
            menuPages[0].AddButton(options);

            //PAGE 1
            menuPages.Add(new MenuPage());
            MenuButton newGame2 = new MenuButton();
            newGame2.SetSprite(1);
            newGame2.SetPosition(150, 175);
            newGame2.SetSourceRectangle(new Rectangle(0, 0, 100, 50));
            newGame2.SetType(MenuButton.TYPE.NEWGAME);
            menuPages[1].AddButton(newGame2);
            newGame2.Select();
        }
    }
}
