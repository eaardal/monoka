using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Client;
using Monoka.Client.EventQueue;
using Monoka.Client.Login;

namespace Monoka.ExampleGame.Client.Scenes.Matchmaking
{
    class MatchmakingScene : IScene
    {
        private readonly IMatchmakerFacade _matchmakerFacade;
        private readonly ILoginFacade _loginFacade;
        private readonly IPlayerRegistry _playerRegistry;
        private readonly IQueueBroker _queueBroker;
        private bool _isFindingGame;

        public MatchmakingScene(IMatchmakerFacade matchmakerFacade, ILoginFacade loginFacade, IPlayerRegistry playerRegistry, IQueueBroker queueBroker)
        {
            if (matchmakerFacade == null) throw new ArgumentNullException(nameof(matchmakerFacade));
            if (loginFacade == null) throw new ArgumentNullException(nameof(loginFacade));
            if (playerRegistry == null) throw new ArgumentNullException(nameof(playerRegistry));
            if (queueBroker == null) throw new ArgumentNullException(nameof(queueBroker));
            _matchmakerFacade = matchmakerFacade;
            _loginFacade = loginFacade;
            _playerRegistry = playerRegistry;
            _queueBroker = queueBroker;
        }

        public string Id => Scene.Matchmaking;

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            var playerName = "Bob_" + Guid.NewGuid().ToString().Substring(0, 4);
            var handshake = _loginFacade.Handshake(playerName);
            _queueBroker.DispatchEvent(handshake);

            if (!_isFindingGame)
            {
                var playerId = _playerRegistry.GetLocalPlayerId();
                _matchmakerFacade.FindGame(playerId);
                _isFindingGame = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
