using System;
using System.Linq.Expressions;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Animation.StateMachine
{
    public class AnimationBranch : IAnimationBranch
    {
        public AnimationBranch(IAnimationState transitionsTo, Func<bool> when)
        {
            TransitionsTo = transitionsTo;
            When = when;
            Unless = () => false;
        }
        public AnimationBranch(IAnimationState transitionsTo, Func<bool> when, Func<bool> unless)
        {

            TransitionsTo = transitionsTo;
            When = when;
            Unless = unless;
        }
        public IAnimationState TransitionsTo { get; set; }

        /// <summary>
        /// When this branch is satisfied.
        /// </summary>
        public Func<bool> When { get; set; } = () => false;

        /// <summary>
        /// If When is true, Unless must be false in order for the branch to be satisfied.
        /// </summary>
        public Func<bool> Unless { get; set; } = () => false;

        /// <summary>
        /// Checks the result of When and Unless to determine if the branch is satisfied.
        /// </summary>
        /// <returns>Is the branch satisfied.</returns>
        public bool IsSatisfied()
        {
            return When() && !Unless();
        }
    }
}
