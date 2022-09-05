using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Sprites;

namespace Z.Engine.Source._2D.Animation
{
    /// <summary>
    /// Controls the storing and playing of animations. 
    /// </summary>
    public class AnimationSequencer : IUpdateable
    {
        private Dictionary<string, AnimatedSprite> _animationProperties = new Dictionary<string, AnimatedSprite>();
        private AnimatedSprite? _currentAnimation = null;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public AnimatedSprite? CurrentAnimation { get => _currentAnimation; protected set => _currentAnimation = value; }

        public bool Enabled { get; set; }

        public int UpdateOrder { get; set; }

        public AnimationSequencer()
        {

        }

        /// <summary>
        /// Adds an animation to the sequencer.
        /// </summary>
        /// <param name="animatedSprite"></param>
        public void AddAnimation(AnimatedSprite animatedSprite)
        {
            _animationProperties.Add(animatedSprite.Name, animatedSprite);
        }

        /// <summary>
        /// Trys to get an animation from the sequencer
        /// </summary>
        /// <param name="animationName">The name of the animation</param>
        /// <returns>Returns the animation or null if it wasn't found.</returns>
        public AnimatedSprite? GetAnimation(string animationName)
        {
            _animationProperties.TryGetValue(animationName, out AnimatedSprite? animatedSprite);
            return animatedSprite;
        }

        /// <summary>
        /// Plays an animation and stops the current animation if it exists.
        /// </summary>
        /// <param name="animationName">The name of the animation</param>
        /// <exception cref="Z.Engine.Source.Core.Exceptions.AnimationNotFoundException"></exception>
        public void Play(string animationName)
        {
            AnimatedSprite? animation = null;
            if (_animationProperties.TryGetValue(animationName, out animation))
            {
                _currentAnimation?.Stop();
                animation.Play();
                _currentAnimation = animation;

                Console.WriteLine($"Playing: {animationName}");
            }
            else
            {
                throw new Z.Engine.Source.Core.Exceptions.AnimationNotFoundException(animationName);
            }
        }

        /// <summary>
        /// Calls the current animation's update function.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _currentAnimation?.Update(gameTime);
        }


    }
}
