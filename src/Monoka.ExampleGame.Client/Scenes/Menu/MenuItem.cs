using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;

namespace Monoka.ExampleGame.Client.Scenes.Menu
{
    public class MenuItem : Sprite
    {
        public MenuItem(IScene scene, Texture2D texture, Vector2 location) : base(texture, location)
        {
            if (scene == null) throw new ArgumentNullException(nameof(scene));
            Scene = scene;
        }

        public IScene Scene { get; }
        
        public override void Update(GameTime gameTime)
        {
            
        }

        public bool IsClicked(int x, int y)
        {
            return Boundaries.Contains(x, y);
        }
    }
}