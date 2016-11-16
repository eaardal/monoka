using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.ExampleGame.Client.Scenes.InGame.Screens;

namespace Monoka.ExampleGame.Client.Scenes.InGame
{
    class GameScene : Scene
    {
        private readonly GameScreen _gameScreen;

        public GameScene(ScreenManager screenManager, GameScreen gameScreen) : base(screenManager)
        {
            if (gameScreen == null) throw new ArgumentNullException(nameof(gameScreen));
            _gameScreen = gameScreen;
        }

        public override bool ShowFor(string gameState)
        {
            return gameState == GameState.InGame;
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            _gameScreen.LoadContent();
        }

        public override void UnloadContent()
        {
            _gameScreen.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ScreenManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Draw(spriteBatch);
        }

        public override void ActivateScene()
        {
            ScreenManager.ActivateScreen(_gameScreen);
        }
    }
}
