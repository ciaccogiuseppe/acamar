using acamar.Source.Engine;
using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Settings;
using acamar.Source.Engine.World;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace acamar
{
    
    public class Main : Game
    {
        
        private GraphicsDeviceManager _graphics;

        private Texture2D leftRightBorder;
        private Texture2D upDownBorder;


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
            inGameMenu = new InGameMenu();


            Globals.world.SetLevel(0);

            Color borderColor = new Color(32, 32, 32);

            leftRightBorder = new Texture2D(_graphics.GraphicsDevice, 400, 400);
            Color[] data = new Color[400 * 400];
            for (int i = 0; i < data.Length; ++i) data[i] = borderColor;
            leftRightBorder.SetData(data);

            upDownBorder = new Texture2D(_graphics.GraphicsDevice, 800, 400);
            Color[] data2 = new Color[800 * 400];
            for (int i = 0; i < data2.Length; ++i) data2[i] = borderColor;
            upDownBorder.SetData(data2);
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
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/

            if (/*this.IsActive*/ true)
            {
                if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU)
                {
                    Globals.mainMenu.Update();
                }
                else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING)
                {
                    if (MyKeyboard.IsPressedNotCont(Keys.Escape))
                        Globals.CURRENTSTATE = Globals.STATE.INGAMEMENU;


                    Globals.runningTime += DateTime.Now - Globals.lastTime;
                    Globals.world.Update();
                }
                else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
                {
                    Globals.runningTime += DateTime.Now - Globals.lastTime;
                    //Globals.world.Update();
                    //inGameMenu.Update();
                    if (MessageHandler.IsActive())
                    {
                        MessageHandler.Update();
                    }
                }

                else if (Globals.CURRENTSTATE == Globals.STATE.MAINMENUPROMPT || Globals.CURRENTSTATE == Globals.STATE.RUNNINGPROMPT)
                {
                    if (PromptHandler.IsActive())
                    {
                        PromptHandler.Update();
                    }
                }

                else if (Globals.CURRENTSTATE == Globals.STATE.TRANSITION)
                {
                    Globals.runningTime += DateTime.Now - Globals.lastTime;
                    if (TransitionHandler.IsActive())
                    {
                        TransitionHandler.Update();
                    }
                }
                else if (Globals.CURRENTSTATE == Globals.STATE.INGAMEMENU)
                {
                    inGameMenu.Update();
                }
                else if (Globals.CURRENTSTATE == Globals.STATE.EXIT)
                {
                    Exit();
                }


                // TODO: Add your update logic here

                base.Update(gameTime);

                MyKeyboard.Reset();
            }


            if(GlobalSettings.CHANGEDRES)
            {
                _graphics.PreferredBackBufferWidth = Globals.SIZEX;
                _graphics.PreferredBackBufferHeight = Globals.SIZEY;
                _graphics.ApplyChanges();
                GlobalSettings.CHANGEDRES = false;
                Window.Position = new Point((
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (_graphics.PreferredBackBufferWidth / 2),
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (_graphics.PreferredBackBufferHeight / 2));
            }


            Globals.lastTime = DateTime.Now;
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.AntiqueWhite);
            GraphicsDevice.Clear(new Color(64,64,64));

            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,  Matrix.CreateScale(0.5f));

            //Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); SamplerState.PointClamp

            Globals._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(Globals.CAMX, Globals.CAMY, 0)*Matrix.CreateScale(Globals.SCALE)* Matrix.CreateTranslation(Globals.OFFX, Globals.OFFY, 0));
            Globals._overBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(Globals.OFFX, Globals.OFFY, 0) * Matrix.CreateScale(Globals.SCALE));

            if (Globals.CURRENTSTATE == Globals.STATE.MAINMENU || Globals.CURRENTSTATE == Globals.STATE.MAINMENUPROMPT)
            {
                Globals.mainMenu.Draw(Globals._overBatch);
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.RUNNING || Globals.CURRENTSTATE == Globals.STATE.RUNNINGPROMPT)
            {
                Globals.world.Draw(Globals._spriteBatch);
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.PAUSE)
            {
                Globals.world.Draw(Globals._spriteBatch);
                //inGameMenu.Draw();
            }
            else if (Globals.CURRENTSTATE == Globals.STATE.INGAMEMENU)
            {
                Globals.world.Draw(Globals._spriteBatch);
                inGameMenu.Draw(Globals._overBatch);
            }

            Globals._overBatch.Draw(leftRightBorder, new Vector2(-400, 0), Color.White);
            Globals._overBatch.Draw(leftRightBorder, new Vector2(400, 0), Color.White);
            Globals._overBatch.Draw(upDownBorder, new Vector2(-200, -400), Color.White);
            Globals._overBatch.Draw(upDownBorder, new Vector2(-200, 400), Color.White);



            if (MessageHandler.IsActive())
            {
                MessageHandler.Draw(Globals._overBatch);
            }

            if(TransitionHandler.IsActive())
            {
                TransitionHandler.Draw(Globals._spriteBatch, Globals._overBatch);
            }

            if(PromptHandler.IsActive())
            {
                PromptHandler.Draw(Globals._overBatch);
            }

            Globals._spriteBatch.End();
            Globals._overBatch.End();

            //Globals._spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
