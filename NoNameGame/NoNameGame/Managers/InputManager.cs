using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace NoNameGame.Managers
{
    public class InputManager
    {
        private static InputManager instance;
        KeyboardState prevKeyboardState;
        KeyboardState currentKeyboardState;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();
                return instance;
            }
        }

        private InputManager ()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public bool KeyDown (params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (currentKeyboardState.IsKeyDown(key))
                    return true;

            return false;
        }

        public bool KeyPressed (params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (prevKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key))
                    return true;
            return false;
        }

        public bool KeyReleased (params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (prevKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key))
                    return true;
            return false;
        }

        public void Update (GameTime gameTime)
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
    }
}
