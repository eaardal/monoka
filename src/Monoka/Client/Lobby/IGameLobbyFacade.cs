using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monoka.Client.Lobby.Results;
using Monoka.Client.Model;

namespace Monoka.Client.Lobby
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