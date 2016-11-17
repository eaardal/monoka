using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Common.Infrastructure.Exceptions;
using Monoka.Common.Infrastructure.Logging;

namespace Monoka.Client
{
    public class SceneRenderer
    {
        private IScene _currentScene;

        public void ActivateScene(IScene scene)
        {
            Log.Msg(this, l => l.Info("Activating scene {@Scene}", scene.GetType().FullName));

            _currentScene = scene;
        }

        public void UnloadContent()
        {
            EnsureSceneIsSet();

            _currentScene.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            EnsureSceneIsSet();

            _currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnsureSceneIsSet();

            _currentScene.Draw(spriteBatch);
        }

        private void EnsureSceneIsSet()
        {
            if (_currentScene == null)
            {
                throw new MonokaException($"No scene set in {nameof(SceneRenderer)}");
            }
        }
    }

}
