using System.Collections.Generic;
using System.IO;

namespace Monoka.ExampleGame.Server.Level
{
    internal interface ILevelParser
    {
        IEnumerable<ITile> LoadTiles(Stream fileStream);
    }
}