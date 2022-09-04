
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Animation.StateMachine
{
    public class AnimationState : IAnimationState
    {
        public string AnimationName { get; set; }
        public List<IAnimationBranch> Branches { get; set; }

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
        }
    }
}
