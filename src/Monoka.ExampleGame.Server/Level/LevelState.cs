using System.Collections.Generic;
using System.Linq;

namespace Monoka.ExampleGame.Server.Level
{
    class LevelState : ILevelState
    {
        public List<ITile> Tiles { get; private set; }

        public List<Player> Players { get; private set; }

        public LevelState()
        {
            ResetAllState();
        }

        public void ResetAllState()
        {
            Tiles = new List<ITile>();
            Players = new List<Player>();
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
