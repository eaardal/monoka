namespace Monoka.ExampleGame.Common.Network
{
    public class ServerConnectionInfo
    {
        public const string ActorSystemName = "peon-server";
        public const string Hostname = "localhost";
        public const int Port = 8000;
        public const string Transport = "tcp";
        public static readonly string ActorSystemPath = $"akka.{Transport}://{ActorSystemName}@{Hostname}:{Port}/";
    }
}