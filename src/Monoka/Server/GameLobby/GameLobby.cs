using System;
using System.Collections.Generic;
using System.Linq;

namespace Monoka.Server.GameLobby
{
    public class GameLobby
    {
        private readonly List<GameLobbyPlayerDto> _players;

        public GameLobby()
        {
            _players = new List<GameLobbyPlayerDto>();
            RequiredNrOfPlayers = 1;
            Title = "New Game Lobby";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public IEnumerable<GameLobbyPlayerDto> Players => _players;
        public string Title { get; set; }
        public bool AreAllPlayersReady => Players.All(p => p.IsReady);
        public static int RequiredNrOfPlayers { get; set; }

        public void Join(GameLobbyPlayerDto playerDto)
        {
            _players.Add(playerDto);
        }

        public void Leave(Guid playerId)
        {
            var player = _players.SingleOrDefault(p => p.Id == playerId);

            if (player != null)
            {
                _players.Remove(player);
            }
        }
    }
}
