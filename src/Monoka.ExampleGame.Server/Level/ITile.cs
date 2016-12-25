using System.Collections.Generic;

namespace Monoka.ExampleGame.Server.Level
{
    internal interface ITile : ISprite
    {
        void Initialize(ParsedTile match);
        List<string> TileTypes { get; }
        bool IsStartTile { get; }
        bool IsEndTile { get; }
        int TileLayoutX { get; set; }
        int TileLayoutY { get; set; }
        bool IsBlockTile { get; }
        bool IsDefaultTile { get; }
        bool HasCollectable { get; }
        bool IsConveyorBeltTile { get; }
        TileCollision Collision { get; set; }
        bool IsTeleportTile { get; }
        bool IsDoorTile { get; }
    }
}
