using acamar.Source.Engine;
using acamar.Source.Engine.World;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace acamar
{
    public static class Constants
    {
        public const int TILESIZE = 32;
    }
    public class Globals
    {
        public enum STATE
        {
            MAINMENU,
            RUNNING,
            PAUSE
        }

        public static SpriteBatch _spriteBatch;
        public static ContentManager Content;
        public static STATE CURRENTSTATE = STATE.MAINMENU;

        public static int clockCount = 30;

        public static int SIZEX = 400;
        public static int SIZEY = 400;

        public static World world;
    }
    public class Main : Game
    {
        
        private GraphicsDeviceManager _graphics;

        //private SpriteBatch _spriteBatch;
        
        //private World world;
        private MainMenu mainMenu;
        private InGameMenu inGameMenu;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            //Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();

            _graphics.PreferredBackBufferWidth = Globals.SIZEX;
            _graphics.PreferredBackBufferHeight = Globals.SIZEY;
            _graphics.ApplyChanges();

            Globals.world = new World();
            mainMenu = new MainMenu();


            Globals.world.SetLevel(0);

            
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals._spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU)
            {
                mainMenu.Update();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING)
            {
                Globals.world.Update();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
            {
                //Globals.world.Update();
                //inGameMenu.Update();
            }


            if (MessageHandler.IsActive())
            {
                MessageHandler.Update();
            }

            if (TransitionHandler.IsActive())
            {
                TransitionHandler.Update();
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,  Matrix.CreateScale(0.5f));

            //Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1.0f));

            if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU)
            {
                mainMenu.Draw();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING)
            {
                Globals.world.Draw();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
            {
                Globals.world.Draw();
                //inGameMenu.Draw();
            }

            if(MessageHandler.IsActive())
            {
                MessageHandler.Draw();
            }

            if(TransitionHandler.IsActive())
            {
                TransitionHandler.Draw();
            }

            Globals._spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
