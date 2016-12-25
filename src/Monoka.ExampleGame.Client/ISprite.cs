﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monoka.ExampleGame.Client
{
    interface ISprite
    {
        Rectangle Boundaries { get; }
        Texture2D Texture { get; }
        Vector2 Position { get; }
        Vector2 Center { get; }
        Vector2 CenterLocation { get; }
        Vector2 Origin { get; }
        float Rotation { get; }
        Color Color { get; }
        Rectangle? DrawRectangle { get; }

        /// <summary>
        /// SourceRectangle's X/Y value is based in the current Position.X/Y.
        /// If the current Position is 100x,100y and SourceRectangle is 0x,50y, it will draw a rectangle at 100x,150y
        /// </summary>
        Rectangle? SourceRectangle { get; }

        Vector2? Scale { get; }
        SpriteEffects SpriteEffect { get; }
        float Depth { get; }
        bool UseCenterAsOrigin { get; }
        void LoadContent();
        void UnloadContent();
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void DrawWithAllSettings(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, KeyboardState keyboardState);
    }
}
