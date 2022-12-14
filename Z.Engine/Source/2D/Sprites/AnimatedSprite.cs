using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Sprites
{
    //TODO: Add animation playback modes (Forward, Reverse, Ping Pong)
    public class AnimatedSprite : Sprite
    {
        protected Rectangle _sourceRectangle = new Rectangle();
        protected int _currentFrame = 0;
        protected int _frameDelayCounter = 0;
        protected bool _isPlaying = false;
        protected bool _isHidden = true;

        public AnimatedSprite(Texture2D texture, Game game, IPosition parent) : base(texture, game, parent)
        {
            _sourceRectangle.X = StartX;
            _sourceRectangle.Y = StartY;
            _sourceRectangle.Width = FrameWidth;
            _sourceRectangle.Height = FrameHeight;
        }

        /// <summary>
        /// Plays the animation. Unhides the sprite if it's hidden.
        /// </summary>
        public virtual void Play()
        {
            IsPlaying = true;
            IsHidden = false;
        }

        /// <summary>
        /// Stops the animation and hides it.
        /// </summary>
        /// <param name="resetToFirstFrame">Whether to reset the frame counter to 0. This will cause the next Play() to start at frame 0.</param>
        public virtual void Stop(bool resetToFirstFrame = true)
        {
            IsPlaying = false;
            if (resetToFirstFrame) CurrentFrame = 0;
            IsHidden = true;
        }

        /// <summary>
        /// If the sprite is playing, does animation logic using PlaybackSpeed and NumFrames
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                // Increment our counter within 0-PlaybackSpeed
                _frameDelayCounter = (_frameDelayCounter + 1) % PlaybackSpeed;

                // Once a cycle, update the sprite's frame.
                if (_frameDelayCounter == 0)
                {
                    CurrentFrame = (CurrentFrame + 1) % NumFrames;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the current sprite frame using the _sourceRectange and IsHidden property.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (!IsHidden)
            {
                ZGame.Globals.SpriteBatch?.Draw(_texture, WorldPosition, _sourceRectangle, Color.White);
            }
        }

        /// <summary>
        /// Code used to change which frame is being pulled from the sprite sheet.
        /// </summary>
        protected void UpdateSourceRectangle()
        {
            _sourceRectangle.X = StartX + (CurrentFrame % NumFramesX) * FrameWidth;
            _sourceRectangle.Y = StartY + (CurrentFrame / NumFramesY) * FrameHeight;
        }

        /// <summary>
        /// The name of the animation. Used for indexing into the animation sequencer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether we are actively playing the animation.
        /// </summary>
        public bool IsPlaying { get => _isPlaying; protected set => _isPlaying = value; }

        /// <summary>
        /// Whether this sprite should be drawn.
        /// </summary>
        public bool IsHidden { get => _isHidden; protected set => _isHidden = value; }

        /// <summary>
        /// The total number of frames in the sprite sheet.
        /// </summary>
        public int NumFrames { get; set; }

        /// <summary>
        /// How many columns exist in the sprite sheet. Determined using TextureWidth and FrameWidth.
        /// </summary>
        protected int NumFramesX => StartX + TextureWidth / FrameWidth;

        /// <summary>
        /// How many rows exist in the sprite sheet. Determined using TextureHeight and FrameHeight.
        /// </summary>
        protected int NumFramesY => StartY + TextureHeight / FrameHeight;

        /// <summary>
        /// The width of each frame in the sprite sheet
        /// </summary>
        public int FrameWidth
        {
            get
            {
                return _frameWidth;
            }
            set
            {
                _frameWidth = value;
                _sourceRectangle.Width = value;
            }
        } private int _frameWidth = 0;

        /// <summary>
        /// The height of each frame in the sprite sheet
        /// </summary>
        public int FrameHeight
        {
            get
            {
                return _frameHeight;
            }
            set
            {
                _frameHeight = value;
                _sourceRectangle.Height = value;
            }
        } private int _frameHeight = 0;

        /// <summary>
        /// Represents where the frames start in the X direction.
        /// </summary>
        public int StartX
        {
            get
            {
                return _startX;
            }
            set
            {
                _startX = value;
                UpdateSourceRectangle();
            }
        } private int _startX;

        /// <summary>
        /// Represents where the frames start in the Y direction.
        /// </summary>
        public int StartY
        {
            get
            {
                return _startY;
            }
            set
            {
                _startY = value;
                UpdateSourceRectangle();
            }
        } private int _startY;

        /// <summary>
        /// The current frame of the animation. Updates every PlaybackSpeed.
        /// </summary>
        public int CurrentFrame
        {
            get
            {
                return _currentFrame;
            }
            protected set
            {
                _currentFrame = value;
                UpdateSourceRectangle();
            }
        }

        /// <summary>
        /// How many ticks have to pass before the current frame is incremented
        /// </summary>
        public int PlaybackSpeed { get; set; } = 15;


        public override string ToString()
        {
            return Name;
        }
    }
}
