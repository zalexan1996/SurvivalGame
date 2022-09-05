
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Animation.StateMachine
{
    /// <summary>
    /// Animation state implementation. This state represents one animation in the sequencer.
    /// </summary>
    public class AnimationState : IAnimationState
    {
        public string AnimationName { get; set; } = "";
        public List<IAnimationBranch> Branches { get; set; } = new();

        public event IAnimationState.BranchSatisfiedHandler? BranchSatisfied;

        /// <summary>
        /// Adds a branch to transition logic to another state.
        /// </summary>
        /// <param name="animationState">The state to transition to</param>
        /// <param name="where">The conditional that needs to be satisfied for the state to transition</param>
        public void AddBranch(IAnimationState animationState, Func<bool> where)
        {
            Branches.Add(new AnimationBranch(animationState, where));
        }

        /// <summary>
        /// Adds a branch to transition logic to another state.
        /// </summary>
        /// <param name="animationState">The state to transition to</param>
        /// <param name="where">The conditional that needs to be satisfied for the state to transition</param>
        /// <param name="unless">An 'unless' conditional that will be checked if Where is true.</param>
        public void AddBranch(IAnimationState animationState, Func<bool> where, Func<bool> unless)
        {
            Branches.Add(new AnimationBranch(animationState, where, unless));
        }

        /// <summary>
        /// Checks if any of the states are satisfied. If so, transition to the first one.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            IAnimationBranch? satisfiedBranch = Branches.FirstOrDefault(b => b.IsSatisfied());
            if (satisfiedBranch != null)
            {
                OnBranchSatisfied(this, satisfiedBranch.TransitionsTo);
            }
        }

        /// <summary>
        /// Internal function used to invoke the BranchSatisfied event in child classes.
        /// </summary>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        protected void OnBranchSatisfied(IAnimationState oldState, IAnimationState newState)
        {
            BranchSatisfied?.Invoke(oldState, newState);
        }
    }
}
