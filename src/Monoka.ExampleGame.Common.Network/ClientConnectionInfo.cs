namespace Monoka.ExampleGame.Common.Network
{
    public class ClientConnectionInfo
    {
        public const string ActorSystemName = "peon-client";
        public const string Hostname = "localhost";
        public const int Port = 0; // OS assigns port
        public const string Transport = "tcp";
        public static readonly string ActorSystemPath = $"akka.{Transport}://{ActorSystemName}@{Hostname}/user/";
    }
}
