using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public class Screen
    {
        protected readonly ContentManager ContentManager;

        public Screen(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {
            ContentManager.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
