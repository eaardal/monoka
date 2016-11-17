using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Util.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client.Messages;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;

namespace Monoka.Client
{
    public class Director
    {
        private readonly IEnumerable<IScene> _scenes;
        private string _currentGameState;

        public Director(IEnumerable<IScene> scenes, IMessageBus messageBus)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));
            if (messageBus == null) throw new ArgumentNullException(nameof(messageBus));
            _scenes = scenes;
            
            messageBus.Subscribe<GameStateChanged>(msg => _currentGameState = msg.NewGameState);
        }

        public void SetGameState(string state)
        {
            Log.Msg(this, l => l.Info("Setting game state {@GameState}", state));

            _currentGameState = state;

            _scenes
                .Where(s => s.ShowFor(state))
                .ForEach(s =>
                {
                    Log.Msg(this, l => l.Info("Activating scene {@Scene}", s.GetType().FullName));
                    s.ActivateScene();
                });
        }

        public void Initialize()
        {
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
            _scenes
                .Where(scene => scene.ShowFor(_currentGameState))
                .ForEach(scene => scene.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _scenes
                .Where(scene => scene.ShowFor(_currentGameState))
                .ForEach(scene => scene.Draw(spriteBatch));
        }
    }
}
