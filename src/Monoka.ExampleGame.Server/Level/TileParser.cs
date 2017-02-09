using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Monoka.ExampleGame.Server.Level
{
    class TileParser : ITileParser
    {
        public IEnumerable<ParsedTile> ExtractTiles(IReadOnlyList<string> lines)
        {
            var xCounter = 0;
            var yCounter = 0;

            var tiles = new List<ParsedTile>();

            for (var y = 0; y < lines.Count(); y++)
            {
                yCounter++;

                var matches = new Regex(@"([a-zA-Z0-9\$\-])+").Matches(lines[y]);

                for (var x = 0; x < matches.Count; x++)
                {
                    xCounter++;

                    var match = matches[x];

                    if (!match.Success)
                    {
                        continue;
                    }

                    tiles.Add(new ParsedTile
                    {
                        TileType = match.Value,
                        X = xCounter,
                        Y = yCounter
                    });
                }

                xCounter = 0;
            }

            return tiles;
        }
    }
}
