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


        public void AddAnimation(AnimatedSprite animatedSprite)
        {
            _animationProperties.Add(animatedSprite.Name, animatedSprite);
        }

        public AnimatedSprite? GetAnimation(string animationName)
        {
            _animationProperties.TryGetValue(animationName, out AnimatedSprite? animatedSprite);
            return animatedSprite;
        }
        public void Play(string animationName)
        {
            AnimatedSprite? animation = null;
            if (_animationProperties.TryGetValue(animationName, out animation))
            {
                _currentAnimation?.Stop();
                animation.Play();
                _currentAnimation = animation;
            }
            else
            {
                throw new Z.Engine.Source.Core.Exceptions.AnimationNotFoundException(animationName);
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentAnimation?.Update(gameTime);
        }


    }
}
