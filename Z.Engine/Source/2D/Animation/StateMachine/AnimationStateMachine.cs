using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Animation.StateMachine;
using Z.Engine.Source._2D.Sprites;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source._2D.Animation
{
    /// <summary>
    /// Implementation of an animation state machine. Constructs a connected graph of States and controls how control should 
    /// flow between the connected states.
    /// </summary>
    public class AnimationStateMachine : IAnimationStateMachine, IUpdateable
    {
        /// <summary>
        /// The actively playing state.
        /// </summary>
        private IAnimationState? _activeState = null;

        /// <summary>
        /// The state that the state machine starts at. If the machine is reset, this will be the first active state.
        /// </summary>
        private IAnimationState? _startingState = null;


        /// <summary>
        /// Creates a new state machine with an optional animationSequencer. 
        /// All states will look for animations
        /// </summary>
        /// <param name="animationSequencer">
        /// The animationSequencer that this state machine will get animations from. If you supply null, 
        /// it will create a new one.
        /// </param>
        public AnimationStateMachine(AnimationSequencer? animationSequencer = null)
        {
            AnimationSequencer = animationSequencer ?? new AnimationSequencer();
            StateChanged += OnStateChanged;
        }

        /// <summary>
        /// The AnimationSequencer that this state machine will get animations from.
        /// </summary>
        public AnimationSequencer AnimationSequencer {get; set;}

        public List<IAnimationState> States { get; set; } = new List<IAnimationState>();
        
        /// <summary>
        /// The currently playing state. When a new state is set, it invokes the StateChanged event.
        /// </summary>
        public IAnimationState? ActiveState
        {
            get
            {
                return _activeState;
            }
            protected set
            {
                IAnimationState? oldState = _activeState;
                _activeState = value;

                StateChanged?.Invoke(oldState, _activeState);
            }
        }

        /// <summary>
        /// If the current state should be played. "Play" in this context means running the state's Update function every frame.
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            protected set
            {
                _isPlaying = true;
            }
        } private bool _isPlaying = false;



        public event StateChangedHandler? StateChanged;
        public delegate void StateChangedHandler(IAnimationState? oldState, IAnimationState? newState);


        /// <summary>
        /// Adds an AnimatedSprite to the AnimationSequencer.
        /// </summary>
        /// <param name="resource">The animated sprite to add to the sequencer.</param>
        /// <returns>Returns a reference to the animated sprite.</returns>
        public AnimatedSprite AddAnimation(AnimatedSprite resource)
        {
            AnimationSequencer.AddAnimation(resource);
            return GetAnimation(resource.Name)!;
        }

        /// <summary>
        /// Tries to get an animation from the internal AnimationSequencer by name.
        /// </summary>
        /// <param name="name">The name of the animation to get.</param>
        /// <returns>eturns the sprite. Or null if it wasn't found.</returns>
        public AnimatedSprite? GetAnimation(string name)
        {
            return AnimationSequencer.GetAnimation(name);
        }

        /// <summary>
        /// Adds a state to the state machine.
        /// </summary>
        /// <param name="state">The state to add.</param>
        /// <param name="isStartingState">
        /// Starting state means that when this machine is Started or Reset, 
        /// this state will be the first to be ran.
        /// </param>
        public void AddState(IAnimationState state, bool isStartingState = false)
        {
            States.Add(state);
            if (isStartingState)
                _startingState = state;

            state.BranchSatisfied += OnBranchSatisfied;
        }

        /// <summary>
        /// Starts the state machine. "Start" in this context means that it will make the ActiveState the StartingState.
        /// If there is no StartingState, it makes the first state the ActiveState.
        /// </summary>
        /// <param name="resetToStart">
        /// Whether the ActiveState should be set to the StartingState. 
        /// If false, it'll play whatever was the previous ActiveState.
        /// </param>
        public void Start(bool resetToStart = true)
        {
            IsPlaying = true;
            if (resetToStart || _activeState == null)
            {
                ActiveState = _startingState ?? States[0];
            }
        }

        /// <summary>
        /// Stops processing of the animation state machine. The ActiveState stays the same, but it will no longer
        /// have its Update function called every frame.
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Bypass state execution logic and calls the state that controls when animationName is to be played.
        /// This will be upgraded later to an event-based bypass for situations like "player was damaged, stop doing 
        /// whatever you're doing and play the take damage animation, then go back to the previous state".
        /// </summary>
        /// <param name="animationName"></param>
        public void Skip(string animationName)
        {
            IAnimationState? newState = States.SingleOrDefault(s => s.AnimationName == animationName);
            if (newState != null)
            {
                ActiveState = newState;
            }
        }

        /// <summary>
        /// Called every frame and calls the ActiveState's Update function. 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Run the update loop only on the active state.
            _activeState?.Update(gameTime);

            // Checks if the active state's AnimationName has changed. If so, Play the new animation
            // TODO: Implement as event.
            if (_activeState != null && AnimationSequencer.CurrentAnimation != null && _activeState.AnimationName != AnimationSequencer.CurrentAnimation.Name)
            {
                AnimationSequencer.Play(_activeState.AnimationName);
            }

        }
        /// <summary>
        /// The actively playing sprite. Fetched by getting the ActiveState's AnimationName from the AnimationSequencer.
        /// </summary>
        public AnimatedSprite? ActiveSprite
        {
            get
            {
                if (_activeState != null)
                {
                    return AnimationSequencer.GetAnimation(_activeState.AnimationName);
                }
                return null;
            }
        }


        /// <summary>
        /// Called when the active state has a satisfied branch.
        /// All it does is change the ActiveState to the new state as reported by the branch.
        /// </summary>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        protected void OnBranchSatisfied(IAnimationState oldState, IAnimationState newState)
        {
            ActiveState = newState;
        }

        /// <summary>
        /// Called when a state changes. Just plays the new state's animation.
        /// </summary>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        protected void OnStateChanged(IAnimationState? oldState, IAnimationState? newState)
        {
            if (newState != null)
            {
                AnimationSequencer.Play(newState.AnimationName);
            }
        }


        //TODO: Implement this.
        #region bloatFromUpdatable
        public bool Enabled => throw new NotImplementedException();
        public int UpdateOrder => throw new NotImplementedException();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        #endregion
    }
}
