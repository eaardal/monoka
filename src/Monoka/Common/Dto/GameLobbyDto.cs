using System;
using System.Collections.Generic;

namespace Monoka.Common.Dto
{
    public class GameLobbyDto
    {
        public Guid Id { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public string Title { get; set; }
    }
}
