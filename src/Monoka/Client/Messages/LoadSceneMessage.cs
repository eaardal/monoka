using System;

namespace Monoka.Client.Messages
{
    public class LoadSceneMessage
    {
        public string SceneId { get; }

        public LoadSceneMessage(string sceneId)
        {
            SceneId = sceneId;
        }
    }
}
