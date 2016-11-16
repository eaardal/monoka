namespace Monoka.Client.Messages
{
    public class GameStateChanged
    {
        public string NewGameState { get; }

        public GameStateChanged(string gameState)
        {
            NewGameState = gameState;
        }
    }
}