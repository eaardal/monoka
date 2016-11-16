using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public abstract class Scene : IScene
    {
        protected readonly ScreenManager ScreenManager;

        protected Scene(ScreenManager screenManager)
        {
            if (screenManager == null) throw new ArgumentNullException(nameof(screenManager));
            ScreenManager = screenManager;
        }

        public abstract bool ShowFor(string gameState);
        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void ActivateScene();
    }
}
