using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public abstract class Scene : IScene
    {
        protected readonly ScreenRenderer ScreenRenderer;

        protected Scene(ScreenRenderer screenRenderer)
        {
            if (screenRenderer == null) throw new ArgumentNullException(nameof(screenRenderer));
            ScreenRenderer = screenRenderer;
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
