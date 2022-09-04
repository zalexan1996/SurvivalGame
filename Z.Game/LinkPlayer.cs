using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Z.Engine.Source;
using Z.Engine.Source._2D.Animation;
using Z.Engine.Source._2D.Animation.StateMachine;
using Z.Engine.Source._2D.Sprites;
using Z.Engine.Source.Core.Input;
using Z.Engine.Source.Interfaces;
using Z.Engine.Source.Player;

namespace Z.SurvivalGame
{
    public class LinkPlayer : Player
    {
        // Delay construction of the sprite sheet until the InitAnimations function.
        Texture2D? _spriteSheet = null;
        Vector2 InputVector = new Vector2();



        public LinkPlayer(Game game, IPosition parent) : base(game, parent)
        {

        }

        public override void InitAnimations()
        {
            LoadAnimations();


            BlendSpace<AnimatedSprite> WalkBlendSpace = new();
            WalkBlendSpace.Add(-1, 0, AnimationStateMachine.GetAnimation("WalkLeft")!);
            WalkBlendSpace.Add(1, 0, AnimationStateMachine.GetAnimation("WalkRight")!);
            WalkBlendSpace.Add(0, -1, AnimationStateMachine.GetAnimation("WalkUp")!);
            WalkBlendSpace.Add(0, 1, AnimationStateMachine.GetAnimation("WalkDown")!);

            BlendSpace<AnimatedSprite> IdleBlendSpace = new();
            IdleBlendSpace.Add(-1, 0, AnimationStateMachine.GetAnimation("IdleLeft")!);
            IdleBlendSpace.Add(1, 0, AnimationStateMachine.GetAnimation("IdleRight")!);
            IdleBlendSpace.Add(0, -1, AnimationStateMachine.GetAnimation("IdleUp")!);
            IdleBlendSpace.Add(0, 1, AnimationStateMachine.GetAnimation("IdleDown")!);

            AnimationBlendSpaceState WalkBlendSpaceState = new(WalkBlendSpace, () => { return BetterKeyboard.Inst.CurrentIV; })
            {
                AnimationName = "WalkDown"
            };
            AnimationBlendSpaceState IdleBlendSpaceState = new(IdleBlendSpace, () =>
            {
                return BetterKeyboard.Inst.LastNonZeroIV;
            })
            {
                AnimationName = "IdleDown"
            };

            WalkBlendSpaceState.AddBranch(IdleBlendSpaceState, () =>
            {
                return BetterKeyboard.Inst.CurrentIV.Length() == 0;
            });

            IdleBlendSpaceState.AddBranch(WalkBlendSpaceState, () =>
            {
                return BetterKeyboard.Inst.CurrentIV.Length() > 0;
            });

            AnimationStateMachine.AddState(WalkBlendSpaceState);
            AnimationStateMachine.AddState(IdleBlendSpaceState);



        }

        public override void ProcessInput()
        {
            
            // Make sure that we have a valid input vector before trying to add it to our position.
            RelativePosition += BetterKeyboard.Inst.CurrentIV;
        }


        public void LoadAnimations()
        {
            _spriteSheet = Game.Content.Load<Texture2D>("2D/Sprites/SS_LinksAwakeningMovement");

            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "WalkDown",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 0
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "WalkUp",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 2
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "WalkLeft",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 4
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "WalkRight",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 6
            });

            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "IdleDown",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 0
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "IdleUp",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 2
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "IdleLeft",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 4
            });
            AnimationStateMachine.AddAnimation(new AnimatedSprite(_spriteSheet, Game, this)
            {
                Name = "IdleRight",
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 6
            });
        }
    }
}
