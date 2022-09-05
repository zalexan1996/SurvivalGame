using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Z.Engine.Source.Interfaces
{
    /// <summary>
    /// Child classes define transition logic to see if the containing state should transition to another state.
    /// 
    /// Child classes could be things like: WhenAnimFinished, WhenConditional, XTimes, OnEvent, Infinite
    /// </summary>
    public interface IAnimationBranch
    {
        /// <summary>
        /// The state that this branch transitions control to.
        /// </summary>
        public IAnimationState TransitionsTo { get; set; }


        /// <summary>
        /// Determines whether TransitionsTo should be activated.
        /// </summary>
        /// <returns></returns>
        public bool IsSatisfied();

    }
}
