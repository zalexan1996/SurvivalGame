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
        public AnimationStateMachine AnimationStateMachine { get; set; } = new AnimationStateMachine();
        public Player(Game game, IPosition parent) : base(game, parent)
        {

        }

        /// <summary>
        /// Add animations to the internal sequencer.
        /// </summary>
        /// <param name="sequencer"></param>
        public abstract void InitAnimations();

        /// <summary>
        /// Setup key conditions here to control which animations are played.
        /// </summary>
        public abstract void ProcessInput();


        public override void Initialize()
        {
            InitAnimations();
            base.Initialize();
            AnimationStateMachine.Start(true);
        }


        public override void Update(GameTime gameTime)
        {
            ProcessInput();
            AnimationStateMachine.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            // Try to draw the current animation's frame.
            AnimationStateMachine.ActiveSprite?.Draw(gameTime);
        }



    }
}
