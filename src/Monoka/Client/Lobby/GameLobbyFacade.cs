using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Client.Lobby.Results;
using Monoka.Client.Model;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;

namespace Monoka.Client.Lobby
{
    public class GameLobbyFacade : ActorFacade, IGameLobbyFacade
    {
        private readonly IAutoMapperAdapter _mapper;

        public GameLobbyFacade(IAutoMapperAdapter mapper, ActorSystem actorSystem, ILogger logger) : base(actorSystem, logger)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
        }

        public async Task<GameLobby> PlayerReady(Guid playerId, Guid lobbyId)
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);
            
            var answer = await actor.Ask(new FromClient.PlayerReady(playerId, lobbyId));

            if (answer is FromServer.GameLobbyUpdated)
            {
                var result = answer as FromServer.GameLobbyUpdated;

                return _mapper.Map<GameLobbyDto, GameLobby>(result.GameLobby);
            }

            LogFailure(answer);

            return null;
        }

        public async Task<IEnumerable<GameLobby>> GetGameLobbies()
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);

            var answer = await actor.Ask(new FromClient.GetGameLobbies());

            if (answer is FromServer.GetGameLobbiesResult)
            {
                var result = answer as FromServer.GetGameLobbiesResult;

                return _mapper.Map<IEnumerable<GameLobbyDto>, IEnumerable<GameLobby>>(result.GameLobbies);
            }

            LogFailure(answer);

            return null;
        }

        public async Task<GameLobby> CreateAndJoinGameLobby(Guid playerId, string newLobbyName)
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);

            var answer = await actor.Ask(new FromClient.CreateAndJoinGameLobby(playerId, newLobbyName));

            if (answer is FromServer.CreateAndJoinGameLobbyResult)
            {
                var result = answer as FromServer.CreateAndJoinGameLobbyResult;

                return _mapper.Map<GameLobbyDto, GameLobby>(result.GameLobby);
            }

            LogFailure(answer);

            return null;
        }

        public void StartGame(Guid lobbyId)
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);

            actor.Tell(new FromClient.StartGame(lobbyId));
        }

        public async Task<JoinGameLobbyResult> JoinGameLobby(Guid playerId, Guid lobbyId)
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);

            var answer = await actor.Ask(new FromClient.JoinGameLobby(playerId, lobbyId));

            if (answer is FromServer.JoinGameLobbyResult)
            {
                var result = answer as FromServer.JoinGameLobbyResult;

                return _mapper.Map<FromServer.JoinGameLobbyResult, JoinGameLobbyResult>(result);
            }

            LogFailure(answer);

            return null;
        }

        public async Task<LookingForPlayersResult> IsAnyLobbyLookingForPlayers()
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.GameLobbyReceiver.Path);

            var answer = await actor.Ask(new FromClient.IsAnyLobbyLookingForPlayers());

            if (answer is FromServer.IsAnyLobbyLookingForPlayersResult)
            {
                var result = answer as FromServer.IsAnyLobbyLookingForPlayersResult;

                return _mapper.Map<FromServer.IsAnyLobbyLookingForPlayersResult, LookingForPlayersResult>(result);
            }

            LogFailure(answer);

            return null;
        }
    }
}