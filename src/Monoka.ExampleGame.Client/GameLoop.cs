﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.ExampleGame.Client.Startup;

namespace Monoka.ExampleGame.Client
{
    public class GameLoop : Game
    {
        private SpriteBatch _spriteBatch;
        private Director _director;

        public GraphicsDeviceManager Graphics { get; }

        public GameLoop()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = WindowSize.Height,
                PreferredBackBufferWidth = WindowSize.Width
            };
            Graphics.ApplyChanges();

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
            var ioc = ClientBootstrapper.Wire(this);

            IsMouseVisible = true;

            _director = ioc.Resolve<Director>();
            _director.Initialize(Scene.Menu);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _director.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            _director.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _director.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _director.Draw(_spriteBatch);
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
