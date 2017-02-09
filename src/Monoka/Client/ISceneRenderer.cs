using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monoka.Client
{
    public interface ISceneRenderer
    {
        void RenderScene(IScene scene);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}