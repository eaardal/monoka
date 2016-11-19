using System;

namespace Monoka.Client.GameLobby.Results
{
    public class HandshakeResult
    {
        public bool Success { get; set; }
        public Guid PlayerId { get; set; }
    }
}