using System;

namespace Monoka.Client.Lobby.Results
{
    public class HandshakeResult
    {
        public bool Success { get; set; }
        public Guid PlayerId { get; set; }
    }
}