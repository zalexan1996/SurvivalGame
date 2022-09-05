using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Z.Engine.Source.Interfaces
{
    /// <summary>
    /// Defines an animation state. The caller uses the AnimationName property to determine
    /// what animation this state is deciding to use. 
    /// </summary>
    public interface IAnimationState
    {
        /// <summary>
        /// Notifies the calling environment that one of this state's branches was satisfied and that
        /// we should transition to newState from oldState.
        /// </summary>
        public event BranchSatisfiedHandler? BranchSatisfied;


        public delegate void BranchSatisfiedHandler(IAnimationState oldState, IAnimationState newState);

        /// <summary>
        /// The animation name for this state. If the child class is just a wrapper for a Sprite or AnimatedSprite,
        /// this property will never change. But a blendspace will alter this property depending on its input.
        /// </summary>
        public string AnimationName { get; set; }

        /// <summary>
        /// The connected branches and the conditions required for this state to transition to another.
        /// </summary>
        public List<IAnimationBranch> Branches { get; set; }

        /// <summary>
        /// Checks to make sure that this state should continue being the active one or if we should switch to
        /// another.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime);

    }
}
