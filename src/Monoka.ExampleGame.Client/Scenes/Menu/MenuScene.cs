using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monoka.Client;
using Monoka.Client.Messages;
using Monoka.Common.Infrastructure;
using Monoka.ExampleGame.Client.Scenes.ExitGame;
using Monoka.ExampleGame.Client.Scenes.InGame;

namespace Monoka.ExampleGame.Client.Scenes.Menu
{
    public class MenuScene : IScene
    {
        private readonly MenuItemFactory _menuItemFactory;
        private readonly IMessageBus _messageBus;
        private readonly List<MenuItem> _menuItems;

        public MenuScene(MenuItemFactory menuItemFactory, IMessageBus messageBus)
        {
            if (menuItemFactory == null) throw new ArgumentNullException(nameof(menuItemFactory));
            if (messageBus == null) throw new ArgumentNullException(nameof(messageBus));
            _menuItemFactory = menuItemFactory;
            _messageBus = messageBus;

            _menuItems = new List<MenuItem>();
        }

        public string Id => Scene.Menu;
        
        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            _menuItemFactory.LoadContent();
            CreateMenuItems();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var menuItem in _menuItems)
            {
                menuItem.Draw(spriteBatch);
            }
        }
        
        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton != ButtonState.Pressed) return;


            var clickedMenuItem = _menuItems.SingleOrDefault(item => item.IsClicked(mouseState.X, mouseState.Y));

            if (clickedMenuItem != null)
            {
                var sceneId = clickedMenuItem.Scene.Id;
                _messageBus.Publish(new LoadSceneMessage(sceneId));
            }
        }

        private void CreateMenuItems()
        {
            CreateMenuItem<GameScene>();
            CreateMenuItem<ExitGameScene>();
        }

        private void CreateMenuItem<T>() where T : IScene
        {
            var menuItem = _menuItemFactory.CreateMenuItem<T>();
            _menuItems.Add(menuItem);
        }
    }
}