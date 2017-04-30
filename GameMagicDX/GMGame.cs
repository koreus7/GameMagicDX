using System.Linq;
using GameMagic.ComponentSystem.Implementation;
using GameMagic.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameMagic
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GMGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private World world;
        private KeyboardState newKeyState;
        private KeyboardState oldKeyState;
        public static Effect lightEffect;
        public static Effect post1;
        public static Effect mouseEffect;
        public static Effect repelatronEffect;
        public static Effect hubEffect;
        public static Effect sinkEffect;
        public static Effect colliderEffect;
        public static Effect boostEffect;
        public static Effect gooEffect;
        public static Song backingTrack;

        public static SpriteFont mainFont;

        RenderTarget2D rt;

        public int Width => graphics.GraphicsDevice.Viewport.Width;
        public int Height => graphics.GraphicsDevice.Viewport.Height;

        public static bool DebugOverlay = false;

        public GMGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            world = new MainWorld(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            StaticImg.asprite = Content.Load<Texture2D>("img/asprite");
            StaticImg.sprite512 = Content.Load<Texture2D>("img/sprite512");
            StaticImg.sprite1024 = Content.Load<Texture2D>("img/sprite1024");

            StaticImg.expandIcon = Content.Load<Texture2D>("img/expandIcon");
            StaticImg.counterClockwise = Content.Load<Texture2D>("img/CounterClockwise");
            StaticImg.clockwise = Content.Load<Texture2D>("img/clockwise");
            StaticImg.speed = Content.Load<Texture2D>("img/speed");

            StaticSound.absorb = Content.Load<SoundEffect>("snd/Absorb");
            StaticSound.absorbBad = Content.Load<SoundEffect>("snd/AbsorbBad");
            StaticSound.win = Content.Load<SoundEffect>("snd/Win");
            StaticSound.click = Content.Load<SoundEffect>("snd/Click");
            backingTrack = Content.Load<Song>("snd/track1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backingTrack);

            lightEffect = Content.Load<Effect>("fx/Light");
            lightEffect.Parameters["res"].SetValue(Vector2.One);
            gooEffect = Content.Load<Effect>("fx/Goo");
            post1 = Content.Load<Effect>("fx/Post1");
            mouseEffect = Content.Load<Effect>("fx/Mouse");
            repelatronEffect = Content.Load<Effect>("fx/Repelatron");
            hubEffect = Content.Load<Effect>("fx/Hub");
            sinkEffect = Content.Load<Effect>("fx/Sink");
            colliderEffect = Content.Load<Effect>("fx/Collider");
            boostEffect = Content.Load<Effect>("fx/Boost");

            mainFont = Content.Load<SpriteFont>("fnt/mainFont");

            rt = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, true, graphics.PreferredBackBufferFormat, graphics.PreferredDepthStencilFormat);

            world.Init();
            Clear();
            Load();
        }

        public void Clear()
        {
            world = new MainWorld(this);
            world.Init();
            world.Load($@"Levels/LevelEmpty.json");
        }

        public static int LevelCounter { get; set; } = 0;

        public void Load()
        {
            world = new MainWorld(this);
            world.Init();
            world.Load($@"Levels/Level{GMGame.LevelCounter}.json");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (KeyPressed(Keys.F3))
            {
                DebugOverlay = !DebugOverlay;
            }

            if (KeyPressed(Keys.F5))
            {
                world.Save();
            }

            if (KeyPressed(Keys.F6))
            {
                Clear();
            }

            if (KeyPressed(Keys.R))
            {
                Load();
            }


            MouseState ms = Mouse.GetState();

            if (KeyPressed(Keys.Q))
            {
                world.AddEntity(new Hub(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.W))
            {
                world.AddEntity(new Repelatron(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.E))
            {
                world.AddEntity(new Planet(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.Z))
            {
                world.AddEntity(new ReversePlanet(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.A))
            {
                world.AddEntity(new SpeedBoost(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.S))
            {
                world.AddEntity(new Source(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.D))
            {
                world.AddEntity(new Sink(world, new Vector2(ms.X, ms.Y)));
            }
            if (KeyPressed(Keys.G))
            {
                world.AddEntity(new Goo(world, new Vector2(ms.X, ms.Y)));
            }

            if (KeyPressed(Keys.F))
            {
                graphics.ToggleFullScreen();
            }



            if (KeyPressed(Keys.M))
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
                else
                {
                    MediaPlayer.Resume();
                }
            }

            if (KeyPressed(Keys.T))
            {
                world.ReturnOrbs();
            }

            world.Update(gameTime);

            oldKeyState = newKeyState;
            base.Update(gameTime);
        }

        public bool KeyPressed(Keys k)
        {
            return newKeyState.IsKeyDown(k) && oldKeyState.IsKeyUp(k);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.Black);


            //spriteBatch.Begin();
            //Color[] pixels = new Color[Width * Height];
            //for (int y = 0; y < Height; y++)
            //{
            //    for (int x = 0; x < Width; x++)
            //    {
            //        float val = Noise.Generate(x / 300.0f + 1000.0f, y / 300.0f + 1000.0f);
            //        pixels[(y * Width) + x] = new Color(val, val, val, 1.0f);
            //    }
            //}

            //Texture2D myTex = new Texture2D(
            //  graphics.GraphicsDevice,
            //  Width,
            //  Height,
            //  false,
            //  SurfaceFormat.Color);

            //myTex.SetData<Color>(pixels);

            //spriteBatch.Draw(myTex, new Vector2(0, 0), Color.White);

            //spriteBatch.End();

            //lightEffect.Parameters["res"].SetValue(new Vector2(Width, Height));

            //  lightEffect.Parameters["time"].SetValue(gameTime.TotalGameTime.Milliseconds/1000.0f);

            //spriteBatch.Begin(SpriteSortMode.Deferred, 
            //    BlendState.NonPremultiplied, 
            //    SamplerState.PointClamp,DepthStencilState.Default, 
            //    RasterizerState.CullNone, 
            //    lightEffect);
            world.Draw(gameTime, spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(0, null, null, null, null, post1);
            spriteBatch.Draw(rt, new Vector2(0, 0), Color.White);
            spriteBatch.End();




            base.Draw(gameTime);
        }

        private static Texture2D rect;

        public void DrawRectangle(Rectangle coords, Color color)
        {
            if (rect == null)
            {
                rect = new Texture2D(GraphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }
            spriteBatch.Draw(rect, coords, color);
        }
    }
}
