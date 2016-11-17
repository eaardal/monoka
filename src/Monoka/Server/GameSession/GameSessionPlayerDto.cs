using System;

namespace Monoka.Server.GameSession
{
    class GameSessionPlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool FinishedLoading { get; set; }
    }
}
