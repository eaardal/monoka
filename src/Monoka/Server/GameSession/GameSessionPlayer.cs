using System;

namespace Monoka.Server.GameSession
{
    class GameSessionPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool FinishedLoading { get; set; }
    }
}
