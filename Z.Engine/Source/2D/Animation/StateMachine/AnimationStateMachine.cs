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
    public class AnimationStateMachine : IAnimationStateMachine, IUpdateable
    {
        private IAnimationState? _activeState = null;
        private IAnimationState? _startingState = null;

        public AnimationStateMachine(AnimationSequencer? animationSequencer = null)
        {
            AnimationSequencer = animationSequencer ?? new AnimationSequencer();
            StateChanged += OnStateChanged;
        }

        public AnimationSequencer AnimationSequencer {get; set;}
        public List<IAnimationState> States { get; set; } = new List<IAnimationState>();
        
        public IAnimationState? ActiveState
        {
            get
            {
                return _activeState;
            }
            set
            {
                IAnimationState? oldState = _activeState;
                _activeState = value;

                StateChanged?.Invoke(oldState, _activeState);
            }
        }

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



        public AnimatedSprite AddAnimation(AnimatedSprite resource)
        {
            AnimationSequencer.AddAnimation(resource);
            return GetAnimation(resource.Name)!;
        }

        public AnimatedSprite? GetAnimation(string name)
        {
            return AnimationSequencer.GetAnimation(name);
        }

        public void AddState(IAnimationState state, bool isStartingState = false)
        {
            States.Add(state);
            if (isStartingState)
                _startingState = state;

            state.BranchSatisfied += OnBranchSatisfied;
        }


        public void Start(bool resetToStart = true)
        {
            IsPlaying = true;
            if (resetToStart || _activeState == null)
            {
                ActiveState = _startingState ?? States[0];
            }
        }

        public void Stop()
        {
            IsPlaying = false;
        }
        public void Skip(string animationName)
        {
            IAnimationState? newState = States.SingleOrDefault(s => s.AnimationName == animationName);
            if (newState != null)
            {
                ActiveState = newState;
            }
        }

        public void Update(GameTime gameTime)
        {
            // Run the update loop only on the active state.
            _activeState?.Update(gameTime);

            if (_activeState != null && AnimationSequencer.CurrentAnimation != null && _activeState.AnimationName != AnimationSequencer.CurrentAnimation.Name)
            {
                AnimationSequencer.Play(_activeState.AnimationName);
            }
        }

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



        protected void OnBranchSatisfied(IAnimationState oldState, IAnimationState newState)
        {
            ActiveState = newState;
        }
        protected void OnStateChanged(IAnimationState? oldState, IAnimationState? newState)
        {
            if (newState != null)
            {
                AnimationSequencer.Play(newState.AnimationName);
            }
        }



        #region bloatFromUpdatable
        public bool Enabled => throw new NotImplementedException();
        public int UpdateOrder => throw new NotImplementedException();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        #endregion
    }
}
