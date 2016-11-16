namespace Monoka.Common.Network
{
    public class ServerConnectionInfo
    {
        public string ActorSystemName { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Transport { get; set; }
        public string ActorSystemPath => $"akka.{Transport}://{ActorSystemName}@{Hostname}:{Port}/";
    }
}
