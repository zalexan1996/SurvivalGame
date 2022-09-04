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
        public Func<bool> When { get; set; } = () => false;
        public Func<bool> Unless { get; set; } = () => false;

        public bool IsSatisfied()
        {
            return When() && !Unless();
        }
    }
}
