using System;

namespace Monoka.Common.Dto
{
    public class ClientDto
    {
        public string ActorSystemAddress { get; set; }
        public Guid AssignedId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Accepted { get; set; }
        public string Reason { get; set; }
        public string Username { get; set; }
    }
}
