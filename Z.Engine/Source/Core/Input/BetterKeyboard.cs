using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Z.Engine.Source.Core.Input
{

    /// <summary>
    /// A wrapper for the MonoGame keyboard. Event-based.
    /// </summary>
    public class BetterKeyboard
    {
        public enum InputEventType
        {
            Pressed,
            Released
        }

        /// <summary>
        /// Singleton access. It doesn't make sense be able to instantiate more than one keyboard.
        /// </summary>
        public static BetterKeyboard Inst = new BetterKeyboard();

        protected KeyboardState _state;
        protected Keys[]? _keysDown;

        protected Dictionary<Keys, Action<InputEventType>> KeyBindings = new Dictionary<Keys, Action<InputEventType>>();


        public void AddKeyListener(Keys key, Action<InputEventType> boundAction)
        {
            KeyBindings.Add(key, boundAction);
        }

        public void Update()
        {
            _state = Keyboard.GetState();
            Keys[] _newKeysDown = _state.GetPressedKeys();

            if (_keysDown != null)
            {
                foreach (Keys key in _newKeysDown)
                {
                    // Check if we even have any bounded events to this key. No need to do any checks if there aren't any.
                    if (!KeyBindings.TryGetValue(key, out Action<InputEventType>? boundAction))
                        continue;

                    // Check for newly pressed keys
                    if (!_keysDown.Contains(key) && _newKeysDown.Contains(key))
                    {
                        boundAction(InputEventType.Pressed);
                    }
                }

                // Check for newly released keys
                var releasedKeys = _keysDown.Where(old => !_newKeysDown.Contains(old));
                foreach (Keys key in releasedKeys)
                {
                    if (KeyBindings.TryGetValue(key, out Action<InputEventType>? boundAction))
                    {
                        boundAction(InputEventType.Released);
                    }
                }
            }

            _keysDown = _newKeysDown;


            PreviousIV = CurrentIV;
            CurrentIV = GetInputVector();

            if (CurrentIV.Length() > 0)
                LastNonZeroIV = CurrentIV;
        }

        public Vector2 PreviousIV { get; protected set; }
        public Vector2 CurrentIV { get; protected set; }
        public Vector2 LastNonZeroIV { get; protected set; }
        protected Vector2 GetInputVector()
        {
            Vector2 inputVector = new Vector2();
            inputVector.X = inputVector.X + (BetterKeyboard.Inst.IsKeyPressed(Keys.A) ? -1 : 0);
            inputVector.X = inputVector.X + (BetterKeyboard.Inst.IsKeyPressed(Keys.D) ? 1 : 0);
            inputVector.Y = inputVector.Y + (BetterKeyboard.Inst.IsKeyPressed(Keys.W) ? -1 : 0);
            inputVector.Y = inputVector.Y + (BetterKeyboard.Inst.IsKeyPressed(Keys.S) ? 1 : 0);

            inputVector.Normalize();

            return (inputVector.Length() > 0) ? inputVector : Vector2.Zero;
        }

        public bool IsKeyPressed(Keys key)
        {
            return _state.IsKeyDown(key);
        }
    }
}
