﻿namespace Monoka.Common.Network
{
    public abstract class ServerConnectionInfo
    {
        public abstract string ActorSystemName { get; }
        public abstract string Hostname { get; }
        public abstract int Port { get; }
        public abstract string Transport { get; }
        public string ActorSystemPath => $"akka.{Transport}://{ActorSystemName}@{Hostname}:{Port}/";
    }
}
