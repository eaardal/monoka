using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monoka.ExampleGame.Server.Level
{
    class LevelParser : ILevelParser
    {
        private readonly IEnumerable<ITile> _tileDefinitions;
        private readonly ITileParser _tileParser;

        public LevelParser(IEnumerable<ITile> tileDefinitions, ITileParser tileParser)
        {
            if (tileDefinitions == null) throw new ArgumentNullException(nameof(tileDefinitions));
            if (tileParser == null) throw new ArgumentNullException(nameof(tileParser));
            _tileDefinitions = tileDefinitions;
            _tileParser = tileParser;
        }

        public IEnumerable<ITile> LoadTiles(Stream fileStream)
        {
            var lines = ReadLines(fileStream);

            var matches = _tileParser.ExtractTiles(lines.ToList()).ToList();

            var tiles = new List<ITile>();

            foreach (var match in matches)
            {
                var tileDef = _tileDefinitions.SingleOrDefault(t => t.TileTypes.Contains(match.TileType));
                if (tileDef != null)
                {
                    var tile = _tileDefinitions.Single(t => t.GetType().FullName == tileDef.GetType().FullName); // IoC.Resolve<ITile>(tileDef.GetType().FullName);
                    tile.Initialize(match);

                    tiles.Add(tile);

                    //if (TileKinds.Collectables.Contains(match.TileType))
                    //{
                    //    ICollectable collectable = null;

                    //    if (match.TileType == TileKinds.Valuable)
                    //    {
                    //        collectable = _ioc.Resolve<IValuable>();
                    //    }
                    //    else if (TileKinds.Doors.Contains(match.TileType))
                    //    {
                    //        collectable = _ioc.Resolve<IDoor>();
                    //        tile.Collision = TileCollision.Impassable;
                    //    }
                    //    else if (TileKinds.Keys.Contains(match.TileType))
                    //    {
                    //        collectable = _ioc.Resolve<IDoorKey>();
                    //    }
                    //    else if (TileKinds.HazardProtections.Contains(match.TileType))
                    //    {
                    //        collectable = _ioc.Resolve<IHazardProtection>();
                    //    }

                    //    if (collectable != null)
                    //    {
                    //        collectable.Initialize(match);
                    //        _levelState.Collectables.Add(collectable);
                    //    }
                    //}
                }
                else
                {
                    throw new Exception("Tile for " + match.TileType + " tile type missing");
                }
            }

            EnsureHasStartPosition(tiles);
            EnsureHasOnlyOneStartPosition(tiles);

            EnsureHasEndPosition(tiles);
            EnsureHasOnlyOneEndPosition(tiles);

            //if (!_levelState.Collectables.Any(c => c is IValuable))
            //{
            //    var endTile = _levelState.GetEndTile();
            //    endTile.Collision = TileCollision.Passable;
            //}

            return tiles;
        }

        private static void EnsureHasOnlyOneStartPosition(IEnumerable<ITile> tiles)
        {
            var nrOfTiles = tiles.Count(tile => tile.IsStartTile);
            if (nrOfTiles > 1)
            {
                throw new Exception("Level has too many start positions (" + nrOfTiles + "). Can only have one.");
            }
        }

        private static void EnsureHasOnlyOneEndPosition(IEnumerable<ITile> tiles)
        {
            var nrOfTiles = tiles.Count(tile => tile.IsEndTile);
            if (nrOfTiles > 1)
            {
                throw new Exception("Level has too many end positions (" + nrOfTiles + "). Can only have one.");
            }
        }

        private static void EnsureHasStartPosition(IEnumerable<ITile> tiles)
        {
            var hasTile = tiles.Any(tile => tile.IsStartTile);
            if (!hasTile)
            {
                throw new Exception("Missing start position tile");
            }
        }

        private static void EnsureHasEndPosition(IEnumerable<ITile> tiles)
        {
            var hasTile = tiles.Any(tile => tile.IsEndTile);
            if (!hasTile)
            {
                throw new Exception("Missing end position tile");
            }
        }
        
        private static IEnumerable<string> ReadLines(Stream fileStream)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("#") && !line.StartsWith("//"))
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines;
        }
    }
}
