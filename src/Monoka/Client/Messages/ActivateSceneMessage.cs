namespace Monoka.Client.Messages
{
    public class ActivateSceneMessage
    {
        public string SceneId { get; }

        public ActivateSceneMessage(string sceneId)
        {
            SceneId = sceneId;
        }
    }
}