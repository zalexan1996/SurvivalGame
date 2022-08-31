using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Z.Engine.Source.Interfaces;
using Z.Engine.Source.World;

namespace Z.Engine.Source._2D.Sprites
{
    public class Sprite : WorldObject
    {
        // Fields
        protected readonly Texture2D _texture;
        protected Rectangle _drawRectangle = new Rectangle();

        


        // Constructors
        public Sprite(Texture2D texture, Game game, IPosition parent) : base(game, parent)
        {
            _texture = texture;
        } 


        // Core Methods
        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            ZGame.Globals.SpriteBatch?.Draw(_texture, WorldPosition, Color.White);
        }


        // Properties
        public bool IsSpriteLoaded => _texture != null;

        public int TextureWidth => _texture.Width;
        public int TextureHeight => _texture.Height;
    }
}
