using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Common.Infrastructure.Exceptions;

namespace Monoka.Client
{
    public class ScreenManager
    {
        private Screen _currentScreen;
        
        public void SetScreen(Screen screen)
        {
            _currentScreen = screen;
        }

        public void UnloadContent()
        {
            EnsureCurrentScreenIsSet();

            _currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            EnsureCurrentScreenIsSet();

            _currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnsureCurrentScreenIsSet();

            _currentScreen.Draw(spriteBatch);
        }

        private void EnsureCurrentScreenIsSet()
        {
            if (_currentScreen == null)
            {
                throw new MonokaException("No screen set in ScreenManager");
            }
        }
    }
}
