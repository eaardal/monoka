using System.Collections.Generic;

namespace Monoka.ExampleGame.Server.Level
{
    internal interface ITileParser
    {
        IEnumerable<ParsedTile> ExtractTiles(IReadOnlyList<string> lines);
    }
}