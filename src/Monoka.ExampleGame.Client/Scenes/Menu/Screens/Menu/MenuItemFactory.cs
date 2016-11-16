using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.Common.Infrastructure;
using Monoka.ExampleGame.Client.Scenes.InGame.Screens;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens.ExitGame;

namespace Monoka.ExampleGame.Client.Scenes.Menu.Screens.Menu
{
    internal class MenuItemFactory
    {
        private readonly ContentManager _contentManager;
        private Texture2D _exitGameTexture;
        private Texture2D _gameScreenTexture;

        private int _menuItemCounter = 1;

        public MenuItemFactory(ContentManager contentManager)
        {
            if (contentManager == null) throw new ArgumentNullException(nameof(contentManager));
            _contentManager = contentManager;
        }

        public void LoadContent()
        {
            _exitGameTexture = _contentManager.Load<Texture2D>("Screens\\Menu\\ExitGameMenuItem");
            _gameScreenTexture = _contentManager.Load<Texture2D>("Screens\\Menu\\GameScreenMenuItem");
        }

        public MenuItem CreateMenuItem<T>() where T : Screen
        {
            var texture = GetTextureForScreen(typeof(T));
            var location = ConstructLocation<T>(texture);
            var gameScreen = (Screen)IoC.Instance.Resolve<T>();

            var menuItem = new MenuItem(gameScreen, texture, location);
            
            _menuItemCounter++;

            return menuItem;
        }

        private Vector2 ConstructLocation<T>(Texture2D texture) where T : Screen
        {
            var x = (int) (WindowSize.Width/2.0f - texture.Width/2.0f);
            var y = 100*_menuItemCounter;
            return new Vector2(x, y);
        }

        private Texture2D GetTextureForScreen(Type gameScreenType)
        {
            if (gameScreenType == typeof(ExitGameScreen))
                return _exitGameTexture;

            if (gameScreenType == typeof(GameScreen))
                return _gameScreenTexture;

            throw new Exception("Could not find the texture for screen " + gameScreenType.FullName);
        }
    }
}