using System.Collections.Generic;
using System.Linq;

namespace Monoka.ExampleGame.Server.Level
{
    class LevelState : ILevelState
    {
        public List<ITile> Tiles { get; private set; }

        public Player Player { get; set; }

        public LevelState()
        {
            ResetAllState();
        }

        public void ResetAllState()
        {
            Tiles = new List<ITile>();
            Player = null;
        }

        public ITile GetStartTile()
        {
            return Tiles.Single(tile => tile.IsStartTile);
        }

        public ITile GetEndTile()
        {
            return Tiles.Single(tile => tile.IsEndTile);
        }
    }
}
