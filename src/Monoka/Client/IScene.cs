using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public interface IScene
    {
        bool ShowFor(string gameState);
        void Initialize();
        void LoadContent();
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ActivateScene();
    }
}