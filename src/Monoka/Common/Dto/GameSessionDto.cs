using System;
using System.Collections.Generic;

namespace Monoka.Common.Dto
{
    public class GameSessionDto
    {
        public Guid SessionId { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public string Title { get; set; }
    }
}
