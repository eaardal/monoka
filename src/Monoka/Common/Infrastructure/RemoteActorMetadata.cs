using System;

namespace Monoka.Common.Infrastructure
{
    public class RemoteActorMetadata : ActorMetadata
    {
        public new RemoteActorMetadata Parent { get; private set; }

        public RemoteActorMetadata(string name, RemoteActorMetadata parent = null) : base(name, parent)
        {
            Parent = parent;
        }

        public string WithRemoteBasePath(string remoteBasePath)
        {
            return FormatRemotePath(remoteBasePath);
        }

        private string FormatRemotePath(string remoteBasePath)
        {
            return $"{FormatPathEnd(remoteBasePath)}{Path}";
        }
        
        private static string FormatPathEnd(string remoteBasePath)
        {
            return remoteBasePath.EndsWith("/")
                ? remoteBasePath.Substring(0, remoteBasePath.LastIndexOf("/", StringComparison.Ordinal))
                : remoteBasePath;
        }
    }
}
