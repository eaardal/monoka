using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monoka.Common.Infrastructure.Exceptions;
using Monoka.Common.Infrastructure.Logging;

namespace Monoka.Client
{
    public class ScreenManager
    {
        private Screen _currentScreen;
        
        public void ActivateScreen(Screen screen)
        {
            Log.Msg(this, l => l.Info("Activating screen {@Screen}", screen.GetType().FullName));

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
