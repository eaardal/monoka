using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.Common.Infrastructure.Logging;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens.Menu;

namespace Monoka.ExampleGame.Client.Scenes.Menu
{
    public class MenuScene : Scene
    {
        private readonly MenuScreen _menuScreen;

        public MenuScene(ScreenManager screenManager, MenuScreen menuScreen) : base(screenManager)
        {
            if (menuScreen == null) throw new ArgumentNullException(nameof(menuScreen));
            _menuScreen = menuScreen;
        }

        public override bool ShowFor(string gameState)
        {
            return gameState == GameState.Menu;
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            _menuScreen.LoadContent();
        }

        public override void UnloadContent()
        {
            _menuScreen.UnloadContent();
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
            Log.Msg(this, l => l.Info("Activating menu screen"));
            ScreenManager.ActivateScreen(_menuScreen);
        }
    }
}
