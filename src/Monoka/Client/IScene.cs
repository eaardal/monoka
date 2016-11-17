using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public interface IScene
    {
        string Id { get; }
        void Initialize();
        void LoadContent();
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}