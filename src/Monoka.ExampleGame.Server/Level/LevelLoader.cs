using System;
using System.IO;

namespace Monoka.ExampleGame.Server.Level
{
    class LevelLoader
    {
        private readonly ILevelParser _levelParser;
        private readonly ILogger _logger;

        public LevelLoader(ILevelParser levelParser, ILogger logger)
        {
            if (levelParser == null) throw new ArgumentNullException(nameof(levelParser));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _levelParser = levelParser;
            _logger = logger;
        }

        public int FindNumberOfLevels()
        {
            var count = 0;

            try
            {
                var levelIndex = 1;
                while (true)
                {
                    using (var stream = GetLevel(levelIndex))
                    {
                        count++;
                    }
                    levelIndex++;
                }
            }
            catch (Exception)
            {
                return count;
            }
        }

        public LevelState LoadNextLevel(int currentLevelIndex)
        {
            var levelIndexToLoad = currentLevelIndex + 1;
            
            try
            {
                using (var stream = GetLevel(levelIndexToLoad))
                {
                    var tiles = _levelParser.LoadTiles(stream);

                    var levelState = new LevelState();
                    levelState.Tiles.AddRange(tiles);
                    return levelState;
                }
            }
            catch (Exception ex)
            {
                _logger.Msg(this, l => l.Error(ex));
                throw;
            }
        }

        private static Stream GetLevel(int index)
        {
            var exeLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            var location = exeLocation.Substring(0, exeLocation.LastIndexOf("\\", StringComparison.Ordinal));
            var levelPath = $"{location}\\Content\\Levels\\level{index}.txt";
            return new FileStream(levelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
