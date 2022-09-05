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
    /// Base interface for a state machine. I might make multiple types of state machines later on.
    /// </summary>
    public interface IAnimationStateMachine
    {
        public void AddState(IAnimationState state, bool isStartingState = false);
        /// <summary>
        /// Signals to the state machine to start at the StartState.
        /// Should signal event for AnimationStateChanged(oldState, newState) so listeners can make adjustments
        /// when it's at a state, it constantly checks its path conditionals
        /// </summary>
        public void Start(bool resetToStart = true);

        /// <summary>
        /// Signals to the state machine that it should stop the process loop.
        /// </summary>
        /// <param name="resetToStart">Whether to reset the current state to the StartState.</param>
        public void Stop();

        /// <summary>
        /// Signals to the state machine that it should ignore whatever conditionals are in affect and where in the
        /// graph it is, and skip to the specified animation.
        /// </summary>
        /// <exception cref="">Throws an AnimationNotFoundException if animation wasn't found.</exception>
        /// <param name="animationName">The name of the animation to skip to.</param>
        public void Skip(string animationName);
        
        
        /// <summary>
        /// The animation sequencer that this state machine works with.
        /// </summary>
        public AnimationSequencer AnimationSequencer { get; set; }

        /// <summary>
        /// Collection of states. The order they're stored in or the data structure's efficiency doesn't matter
        /// because each state has references to the states they need to transition to. This just exists
        /// to have them all in a central place so we don't run into garbage collection issues.
        /// </summary>
        public List<IAnimationState> States { get; set; }
    }
}
