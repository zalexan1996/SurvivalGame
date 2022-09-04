using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Z.Engine.Source.Interfaces
{
    public interface IAnimationState
    {
        /// <summary>
        /// Notifies the calling environment that one of this state's branches was satisfied and that
        /// we should transition to newState from oldState.
        /// </summary>
        public event BranchSatisfiedHandler? BranchSatisfied;


        public delegate void BranchSatisfiedHandler(IAnimationState oldState, IAnimationState newState);

        /// <summary>
        /// The animation name for this state.
        /// </summary>
        public string AnimationName { get; set; }

        /// <summary>
        /// The connected branches and the conditions required for this state to transition to another.
        /// </summary>
        public List<IAnimationBranch> Branches { get; set; }

        public void Update(GameTime gameTime);

    }
}
