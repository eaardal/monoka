using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monoka.Client.GameLobby.Results;

namespace Monoka.Client.GameLobby
{
    public interface IGameLobbyFacade
    {
        Task<GameLobby> PlayerReady(Guid playerId, Guid lobbyId);
        Task<IEnumerable<GameLobby>> GetGameLobbies();
        Task<GameLobby> CreateAndJoinGameLobby(Guid playerId, string newLobbyName);
        void StartGame(Guid lobbyId);
        Task<JoinGameLobbyResult> JoinGameLobby(Guid playerId, Guid lobbyId);
        Task<LookingForPlayersResult> IsAnyLobbyLookingForPlayers();
    }
}