using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source._2D.Animation;
using Z.Engine.Source._2D.Sprites;
using Z.Engine.Source.Interfaces;
using Z.Engine.Source.World;

namespace Z.Engine.Source.Player
{
    public abstract class Player : WorldObject
    {
        private AnimationSequencer _sequencer = new AnimationSequencer();

        public Player(Game game, IPosition parent) : base(game, parent)
        {

        }

        /// <summary>
        /// Plays the provided animation. Will throw an exception if the animation was not found.
        /// Animation must have been added to the AnimationSequencer in the InitAnimations functions.
        /// </summary>
        /// <param name="animationName">The name of the animation to play.</param>
        public void PlayAnimation(string animationName)
        {
            _sequencer.Play(animationName);
        }

        /// <summary>
        /// Add animations to the internal sequencer.
        /// </summary>
        /// <param name="sequencer"></param>
        public abstract void InitAnimations(AnimationSequencer sequencer);

        /// <summary>
        /// Setup key conditions here to control which animations are played.
        /// </summary>
        public abstract void ProcessInput();


        public override void Initialize()
        {
            InitAnimations(_sequencer);
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            ProcessInput();
            _sequencer.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            // Try to draw the current animation's frame.
            _sequencer?.CurrentAnimation?.Draw(gameTime);
        }



        public string Name { get; set; } = "";
    }
}
