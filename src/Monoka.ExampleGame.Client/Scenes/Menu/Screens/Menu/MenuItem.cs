using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;

namespace Monoka.ExampleGame.Client.Scenes.Menu.Screens.Menu
{
    public class MenuItem : Sprite
    {
        public MenuItem(Screen screen, Texture2D texture, Vector2 location) : base(texture, location)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screen = screen;
        }

        public Screen Screen { get; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public bool IsClicked(int x, int y)
        {
            return Boundaries.Contains(x, y);
        }
    }
}