using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Source.Control;
using Snake.Source.Graphic;
using Snake.Source.Option;
using Snake.Source.State;
using System.Diagnostics;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private InputManager inputManager;
        private State currentState;
        private IGameState currentGameState;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameOptions.SCREEN_WIDTH,
                PreferredBackBufferHeight = GameOptions.SCREEN_HEIGHT
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            inputManager = InputManager.Instance;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Drawer drawer = Drawer.Instance;
            drawer.graphics = graphics;
            drawer.spriteBatch = spriteBatch;
            drawer.spriteFont = Content.Load<SpriteFont>(@"Fonts\Arial");
            drawer.LoadContent();

            UpdateGameState(State.IN_GAME);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputManager.Update();
            State newState = currentGameState.Update(gameTime);

            if (newState != currentState)
            {
                spriteBatch.Begin();
                currentGameState.Draw();
                spriteBatch.End();

                UpdateGameState(newState);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            currentGameState.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateGameState(State newState)
        {
            currentGameState = StateFactory.GetGameState(newState);
            currentState = newState;
        }
    }
}
