using System.Collections.Generic;

namespace Monoka.ExampleGame.Server.Level
{
    internal interface ILevelState
    {
        List<ITile> Tiles { get; }
        List<Player> Players { get; }
        void ResetAllState();
        ITile GetStartTile();
        ITile GetEndTile();
    }
}