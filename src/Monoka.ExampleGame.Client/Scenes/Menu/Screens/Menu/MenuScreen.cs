using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monoka.Client;
using Monoka.ExampleGame.Client.Scenes.InGame.Screens;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens.ExitGame;

namespace Monoka.ExampleGame.Client.Scenes.Menu.Screens.Menu
{
    public class MenuScreen : Screen
    {
        private readonly MenuItemFactory _menuItemFactory;
        private readonly List<MenuItem> _menuItems;
        private readonly ScreenManager _screenManager;

        public MenuScreen(ScreenManager screenManager, ContentManager contentManager, MenuItemFactory menuItemFactory)
            : base(contentManager)
        {
            if (screenManager == null) throw new ArgumentNullException(nameof(screenManager));
            if (menuItemFactory == null) throw new ArgumentNullException(nameof(menuItemFactory));
            _screenManager = screenManager;
            _menuItemFactory = menuItemFactory;

            _menuItems = new List<MenuItem>();
        }

        public override void LoadContent()
        {
            CreateMenuItems();
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            _menuItems.ForEach(menuItem => menuItem.Draw(spriteBatch));
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton != ButtonState.Pressed) return;

            var clickedMenuItem = _menuItems.SingleOrDefault(item => item.IsClicked(mouseState.X, mouseState.Y));

            if (clickedMenuItem != null)
            {
                _screenManager.ActivateScreen(clickedMenuItem.Screen);
            }
        }

        private void CreateMenuItems()
        {
            CreateMenuItem<GameScreen>();
            CreateMenuItem<ExitGameScreen>();
        }

        private void CreateMenuItem<T>() where T : Screen
        {
            var menuItem = _menuItemFactory.CreateMenuItem<T>();
            _menuItems.Add(menuItem);
        }
    }
}