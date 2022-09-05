using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Z.Engine.Source.Interfaces;
using Z.Engine.Source.World;

namespace Z.Engine.Source._2D.Sprites
{
    public class Sprite : WorldObject
    {
        /// <summary>
        /// The texture to draw every frame. Subclasses can override how this texture is to be interpreted and drawn.
        /// </summary>
        protected readonly Texture2D _texture;

        /// <summary>
        /// Represents the bounds of the sprite on the screen with screen-coordinates.
        /// </summary>
        protected Rectangle _drawRectangle = new Rectangle();

        


        /// <summary>
        /// Creates a new sprite.
        /// </summary>
        /// <param name="texture">The texture that this sprite represents</param>
        /// <param name="game">The game this sprite exists in.</param>
        /// <param name="parent">
        /// The parent of this sprite. 
        /// Used so I can calculate world location as an accumulation of parent's RelativeLocations.
        /// Also used for propogating of positions. I can have a character with a position that has multiple
        /// sprites, light sources, sound players, etc. and have their positions set JUST by setting the parent's 
        /// location.
        /// </param>
        public Sprite(Texture2D texture, Game game, IPosition parent) : base(game, parent)
        {
            _texture = texture;
        } 


        /// <summary>
        /// The base sprite doesn't have anything to update. But child classes can add their own logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the sprite. Child classes can change how this is done if necessary.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            ZGame.Globals.SpriteBatch?.Draw(_texture, WorldPosition, Color.White);
        }


        /// <summary>
        /// Is there a valid _texture associated with this sprite.
        /// </summary>
        public bool IsSpriteLoaded => _texture != null;

        /// <summary>
        /// The total width of this texture.
        /// </summary>
        public int TextureWidth => _texture.Width;

        /// <summary>
        /// The total height of this texture.
        /// </summary>
        public int TextureHeight => _texture.Height;
    }
}
