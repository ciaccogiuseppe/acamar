using acamar.Source.Engine;
using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace acamar
{
    public class Globals
    {
        public enum STATE
        {
            MAINMENU,
            RUNNING,
            PAUSE,
            TRANSITION
        }

        public static SpriteBatch _spriteBatch;

        public static SpriteBatch _overBatch;

        public static ContentManager Content;
        public static STATE CURRENTSTATE = STATE.MAINMENU;

        public static int clockCount = 30;

        public static int SIZEX = 400;
        public static int SIZEY = 400;

        public static float SCALE = (float)SIZEX / 400.0f;

        public static World world;

        public static int CAMX = 0;
        public static int CAMY = 0;

        public static Player player;
        public static MainMenu mainMenu;

    }
    public class Main : Game
    {
        
        private GraphicsDeviceManager _graphics;

        //private SpriteBatch _spriteBatch;
        
        //private World world;
        //private MainMenu mainMenu;
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

            FontConstants.Initialize();

            _graphics.PreferredBackBufferWidth = Globals.SIZEX;
            _graphics.PreferredBackBufferHeight = Globals.SIZEY;
            _graphics.ApplyChanges();

            Globals.world = new World();
            Globals.mainMenu = new MainMenu();


            Globals.world.SetLevel(0);


            
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals._spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals._overBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Globals.player = new Player(1, 7, 200, 200, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU)
            {
                Globals.mainMenu.Update();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING)
            {
                Globals.world.Update();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
            {
                //Globals.world.Update();
                //inGameMenu.Update();
                if (MessageHandler.IsActive())
                {
                    MessageHandler.Update();
                }
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.TRANSITION)
            {
                if (TransitionHandler.IsActive())
                {
                    TransitionHandler.Update();
                }
            }


            // TODO: Add your update logic here

            base.Update(gameTime);

            MyKeyboard.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.AntiqueWhite);
            GraphicsDevice.Clear(new Color(64,64,64));

            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,  Matrix.CreateScale(0.5f));

            //Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); SamplerState.PointClamp

            Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Globals.SCALE) * Matrix.CreateTranslation(Globals.CAMX, Globals.CAMY, 0));
            Globals._overBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Globals.SCALE));


            if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU)
            {
                Globals.mainMenu.Draw(Globals._overBatch);
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING)
            {
                Globals.world.Draw(Globals._spriteBatch);
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
            {
                Globals.world.Draw(Globals._spriteBatch);
                //inGameMenu.Draw();
            }

            

            

            if (MessageHandler.IsActive())
            {
                MessageHandler.Draw(Globals._overBatch);
            }

            if(TransitionHandler.IsActive())
            {
                TransitionHandler.Draw(Globals._spriteBatch, Globals._overBatch);
            }

            Globals._spriteBatch.End();
            Globals._overBatch.End();

            //Globals._spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
