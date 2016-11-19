using System;
using Monoka.Client.Model;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network.Messages;

namespace Monoka.Client.GameLobby
{
    class GameLobbyReceiver : LoggingReceiveActor
    {
        private readonly PlayerRegistry _playerRegistry;
        private readonly IAutoMapperAdapter _mapper;

        public GameLobbyReceiver(PlayerRegistry playerRegistry, ILogger log, IAutoMapperAdapter mapper) : base(log)
        {
            if (playerRegistry == null) throw new ArgumentNullException(nameof(playerRegistry));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _playerRegistry = playerRegistry;
            _mapper = mapper;

            Receive<FromServer.PlayerJoinedGameLobby>(msg => OnPlayerJoinedGameLobby(msg));
        }

        private void OnPlayerJoinedGameLobby(FromServer.PlayerJoinedGameLobby msg)
        {
            var localPlayer = _playerRegistry.GetLocalPlayer();

            if (msg.Player.Id != localPlayer.Id && !_playerRegistry.IsKnownPlayer(msg.Player.Id))
            {
                var player = _mapper.Map<PlayerDto, Player>(msg.Player);

                _playerRegistry.AddPlayer(player);

                Log.Msg(this, l => l.Debug($"Player {msg.Player.Name} ({msg.Player.Id}) joined the game lobby"));
            }
        }
    }
}
