using System;

namespace Monoka.Common.Infrastructure
{
    public class RemoteActorMetadata : ActorMetadata
    {
        public string RemotePath { get; private set; }
        public string RemoteBasePath { get; private set; }
        public new RemoteActorMetadata Parent { get; private set; }

        public RemoteActorMetadata(string name, string remoteBasePath, RemoteActorMetadata parent = null)
            : base(name, parent)
        {
            Parent = parent;
            RemoteBasePath = remoteBasePath;
            RemotePath = FormatRemotePath(remoteBasePath);
        }

        private string FormatRemotePath(string remoteBasePath)
        {
            return $"{FormatPathEnd(remoteBasePath)}{Path}";
        }

        public string WithRemoteBasePath(string remoteBasePath)
        {
            return FormatRemotePath(remoteBasePath);
        }

        private static string FormatPathEnd(string remoteBasePath)
        {
            return remoteBasePath.EndsWith("/")
                ? remoteBasePath.Substring(0, remoteBasePath.LastIndexOf("/", StringComparison.Ordinal))
                : remoteBasePath;
        }
    }
}
