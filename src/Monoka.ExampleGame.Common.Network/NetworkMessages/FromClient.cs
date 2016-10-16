namespace Monoka.ExampleGame.Common.Network.NetworkMessages
{
    public class FromClient
    {
        public class Handshake
        {
            public string Username { get; private set; }

            public Handshake(string username)
            {
                Username = username;
            }
        }

        public class HandleCommandRequest
        {
            public string Command { get; }
            public string[] Args { get; }

            public HandleCommandRequest(string command, string[] args)
            {
                Command = command;
                Args = args;
            }
        }
    }
}
