using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;

namespace Monoka.Server.GameLobby
{
    class GameLobbyEmitter : LoggingReceiveActor
    {
        private readonly IAutoMapperAdapter _mapper;
        private readonly ActorSelection _clientRegistry;

        public GameLobbyEmitter(ILogger log, IAutoMapperAdapter mapper) : base(log)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;

            _clientRegistry = Context.ActorSelection(ActorRegistry.ClientRegistry);

            ReceiveAsync<PlayerJoinedGameLobby>(async msg => await OnPlayerJoinedGameLobby(msg));
        }

        private async Task OnPlayerJoinedGameLobby(PlayerJoinedGameLobby msg)
        {
            try
            {
                var answer = await _clientRegistry.Ask(new ClientRegistry.GetClients(msg.GameLobby.Players.Select(p => p.Id)));

                if (answer is IEnumerable<ClientDto>)
                {
                    var clients = answer as IEnumerable<ClientDto>;

                    foreach (var client in clients)
                    {
                        var player = msg.GameLobby.Players.Single(p => p.Id == msg.PlayerId);

                        var playerDto = _mapper.Map<GameLobbyPlayerDto, PlayerDto>(player);

                        var clientGameLobbyActorPath =
                            RemoteActorRegistry.Client.GameLobbyReceiver.WithRemoteBasePath(client.ActorSystemAddress);

                        var clientGameLobby = Context.ActorSelection(clientGameLobbyActorPath);

                        var playerJoined = new FromServer.PlayerJoinedGameLobby(playerDto, msg.GameLobby.Id, true);

                        Log.Msg(this, l => l.Debug($"Sending {playerJoined.GetType().FullName} to {clientGameLobbyActorPath}"));

                        clientGameLobby.Tell(playerJoined);
                    }
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        internal class PlayerJoinedGameLobby
        {
            public PlayerJoinedGameLobby(Guid playerId, GameLobby gameLobby)
            {
                PlayerId = playerId;
                GameLobby = gameLobby;
            }

            public Guid PlayerId { get; }
            public GameLobby GameLobby { get; }
        }
    }
}
