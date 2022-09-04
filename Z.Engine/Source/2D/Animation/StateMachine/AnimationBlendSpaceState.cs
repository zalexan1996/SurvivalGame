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
    public class AnimationBlendSpaceState : IAnimationState
    {
        private readonly BlendSpace<AnimatedSprite> _blendSpace;
        private readonly Func<Vector2> _blendSpaceInput;
        public AnimationBlendSpaceState(BlendSpace<AnimatedSprite> blendSpace, Func<Vector2> blendSpaceInput)
        {
            _blendSpace = blendSpace;
            _blendSpaceInput = blendSpaceInput;
        }

        public string AnimationName { get; set; }
        public List<IAnimationBranch> Branches { get; set; } = new();

        public event IAnimationState.BranchSatisfiedHandler? BranchSatisfied;

        public void AddBranch(IAnimationState animationState, Func<bool> where)
        {
            Branches.Add(new AnimationBranch(animationState, where));
        }
        public void AddBranch(IAnimationState animationState, Func<bool> where, Func<bool> unless)
        {
            Branches.Add(new AnimationBranch(animationState, where, unless));
        }
        public void Update(GameTime gameTime)
        {
            IAnimationBranch? satisfiedBranch = Branches.FirstOrDefault(b => b.IsSatisfied());
            if (satisfiedBranch != null)
            {
                BranchSatisfied?.Invoke(this, satisfiedBranch.TransitionsTo);
            }

            Vector2 input = _blendSpaceInput();


            // set the current animation name to the current value for the blend space.
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
