using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Sprites;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Animation.StateMachine
{
    /// <summary>
    /// Represents an animation state that is a blendspace.
    /// </summary>
    public class AnimationBlendSpaceState : AnimationState
    {
        // Local blend space reference/
        private readonly BlendSpace<AnimatedSprite> _blendSpace;

        // Called every frame to get the latest AnimatedSprite from the blend space.
        private readonly Func<Vector2> _blendSpaceInput;
        public AnimationBlendSpaceState(BlendSpace<AnimatedSprite> blendSpace, Func<Vector2> blendSpaceInput)
        {
            _blendSpace = blendSpace;
            _blendSpaceInput = blendSpaceInput;
        }

        /// <summary>
        /// Checks to see if any of this state's branches are satisfied. If not, 
        /// get's the latest animated sprite from the BlendSpace and calls its update function.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Potentially switch to a new state.
            base.Update(gameTime);


            // Get our blend space input.
            Vector2 input = _blendSpaceInput();

            // Set the current animation name to the current value for the blend space.
            AnimatedSprite? sprite = _blendSpace.Get(input.X, input.Y);
            if (sprite != null)
            {
                AnimationName = sprite.Name;

                // Update the sprite too.
                sprite.Update(gameTime);
            }
        }
    }
}
