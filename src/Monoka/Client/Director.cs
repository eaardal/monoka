using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Util.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Common.Infrastructure.Exceptions;

namespace Monoka.Client
{
    public class Director : IDirector
    {
        private readonly IEnumerable<IScene> _scenes;
        private readonly SceneRenderer _sceneRenderer;

        public Director(IEnumerable<IScene> scenes, SceneRenderer sceneRenderer)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));
            if (sceneRenderer == null) throw new ArgumentNullException(nameof(sceneRenderer));
            _scenes = scenes;
            _sceneRenderer = sceneRenderer;
        }
        
        public void Initialize(string sceneId)
        {
            _scenes.ForEach(scene => scene.Initialize());

            ActivateScene(sceneId);
        }

        public void LoadContent()
        {
            _scenes.ForEach(scene => scene.LoadContent());
        }

        public void UnloadContent()
        {
            _scenes.ForEach(scene => scene.UnloadContent());
        }

        public void Update(GameTime gameTime)
        {
            _sceneRenderer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sceneRenderer.Draw(spriteBatch);
        }

        public void ActivateScene(string sceneId)
        {
            var sceneToActivate = _scenes.SingleOrDefault(scene => scene.Id == sceneId);

            if (sceneToActivate == null)
            {
                throw new MonokaException($"No scene found matching sceneId {sceneId}");
            }

            _sceneRenderer.RenderScene(sceneToActivate);
        }
    }
}
