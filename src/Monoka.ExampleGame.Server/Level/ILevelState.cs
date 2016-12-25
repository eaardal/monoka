using System.Collections.Generic;

namespace Monoka.ExampleGame.Server.Level
{
    internal interface ILevelState
    {
        List<ITile> Tiles { get; }
        Player Player { get; set; }
        void ResetAllState();
        ITile GetStartTile();
        ITile GetEndTile();
    }
}