using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Z.Engine.Source;
using Z.Engine.Source._2D.Animation;
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

        public override void InitAnimations(AnimationSequencer sequencer)
        {
            _spriteSheet = Game.Content.Load<Texture2D>("2D/Sprites/SS_LinksAwakeningMovement");


            sequencer.AddAnimation("IdleDown", new AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 0
            });

            sequencer.AddAnimation("IdleUp", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 2
            });

            sequencer.AddAnimation("IdleLeft", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 4
            });

            sequencer.AddAnimation("IdleRight", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 1,
                StartX = 16 * 6
            });





            sequencer.AddAnimation("WalkDown", new AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 0
            });

            sequencer.AddAnimation("WalkUp", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 2
            });

            sequencer.AddAnimation("WalkLeft", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 4
            });

            sequencer.AddAnimation("WalkRight", new Engine.Source._2D.Sprites.AnimatedSprite(_spriteSheet, Game, this)
            {
                FrameWidth = 16,
                FrameHeight = 16,
                NumFrames = 2,
                StartX = 16 * 6
            });


            BetterKeyboard.Inst.AddKeyListener(Keys.W, WalkUp);
            BetterKeyboard.Inst.AddKeyListener(Keys.S, WalkDown);
            BetterKeyboard.Inst.AddKeyListener(Keys.A, WalkLeft);
            BetterKeyboard.Inst.AddKeyListener(Keys.D, WalkRight);
        }
        public void WalkUp(BetterKeyboard.InputEventType eventType)
        {
            if (eventType == BetterKeyboard.InputEventType.Pressed)
            {
                PlayAnimation("WalkUp");
            }
            else
            {
                PlayAnimation("IdleUp");
            }
        }
        public void WalkDown(BetterKeyboard.InputEventType eventType)
        {
            if (eventType == BetterKeyboard.InputEventType.Pressed)
            {
                PlayAnimation("WalkDown");
            }
            else
            {
                PlayAnimation("IdleDown");
            }
        }
        public void WalkLeft(BetterKeyboard.InputEventType eventType)
        {
            if (eventType == BetterKeyboard.InputEventType.Pressed)
            {
                PlayAnimation("WalkLeft");
            }
            else
            {
                PlayAnimation("IdleLeft");
            }
        }
        public void WalkRight(BetterKeyboard.InputEventType eventType)
        {
            if (eventType == BetterKeyboard.InputEventType.Pressed)
            {
                PlayAnimation("WalkRight");
            }
            else
            {
                PlayAnimation("IdleRight");
            }
        }
        public override void ProcessInput()
        {
            InputVector = Vector2.Zero;
            if (BetterKeyboard.Inst.IsKeyPressed(Keys.W))
                InputVector.Y = -1;
            else if (BetterKeyboard.Inst.IsKeyPressed(Keys.S))
                InputVector.Y = 1;
            if (BetterKeyboard.Inst.IsKeyPressed(Keys.A))
                InputVector.X = -1;
            else if (BetterKeyboard.Inst.IsKeyPressed(Keys.D))
                InputVector.X = 1;

            // Input InputVector so that when we go diagonal, we go the same speed as if we were going up or left.
            InputVector.Normalize();
            
            // Make sure that we have a valid input vector before trying to add it to our position.
            RelativePosition += (InputVector.Length() > 0) ? InputVector : Vector2.Zero;
        }
    }
}
