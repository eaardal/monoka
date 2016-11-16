using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens;

namespace Monoka.ExampleGame.Client.Scenes.Menu
{
    class MenuScene : IScene
    {
        private readonly ScreenManager _screenManager;
        private readonly MenuScreen _menuScreen;

        public MenuScene(ScreenManager screenManager, MenuScreen menuScreen)
        {
            if (screenManager == null) throw new ArgumentNullException(nameof(screenManager));
            if (menuScreen == null) throw new ArgumentNullException(nameof(menuScreen));
            _screenManager = screenManager;
            _menuScreen = menuScreen;
        }

        public bool ShowFor(string gameState)
        {
            return gameState == GameState.Menu;
        }

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            _menuScreen.LoadContent();
        }

        public void UnloadContent()
        {
            _menuScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _screenManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _screenManager.Draw(spriteBatch);
        }

        public void ActivateScene()
        {
            _screenManager.SetScreen(_menuScreen);
        }
    }
}
