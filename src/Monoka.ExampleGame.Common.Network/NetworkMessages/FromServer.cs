using System;

namespace Monoka.ExampleGame.Common.Network.NetworkMessages
{
    public class FromServer
    {
        public class HandshakeResult
        {
            public HandshakeResult(bool success, Guid playerId)
            {
                Success = success;
                PlayerId = playerId;
            }
            
            public bool Success { get; }
            public Guid PlayerId { get; }
        }

        public class HandleCommandResponse
        {
            public string[] Result { get; }

            public HandleCommandResponse(string[] result)
            {
                Result = result;
            }
        }
    }
}
