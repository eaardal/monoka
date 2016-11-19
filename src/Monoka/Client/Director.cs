using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Util.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client.Messages;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Exceptions;
using Monoka.Common.Infrastructure.Logging;

namespace Monoka.Client
{
    public class Director
    {
        private readonly IEnumerable<IScene> _scenes;
        private readonly SceneRenderer _sceneRenderer;

        public Director(IEnumerable<IScene> scenes, SceneRenderer sceneRenderer, IMessageBus messageBus)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));
            if (sceneRenderer == null) throw new ArgumentNullException(nameof(sceneRenderer));
            if (messageBus == null) throw new ArgumentNullException(nameof(messageBus));
            _scenes = scenes;
            _sceneRenderer = sceneRenderer;
            
            messageBus.Subscribe<LoadSceneMessage>(msg => ActivateScene(msg.SceneId));
        }
        
        public void Initialize(string sceneId)
        {
            ActivateScene(sceneId);

            _scenes.ForEach(scene => scene.Initialize());
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

            _sceneRenderer.ActivateScene(sceneToActivate);
        }
    }
}
