
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace NoNameGame.Managers
{
    /// <summary>
    /// Eine Klasse, welche alle Eingaben handhabt.
    /// </summary>
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

        /// <summary>
        /// Gibt an, ob verschiedene Taste gedrückt wurded sind.
        /// </summary>
        /// <param name="keys">die Tasten</param>
        /// <returns>wurde eine der Tasten gedrückt</returns>
        public bool KeyDown (params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (currentKeyboardState.IsKeyDown(key))
                    return true;

            return false;
        }

        /// <summary>
        /// Gibt an, ob verschiedene Tasten gerade erst gedrückt wurden sind.
        /// </summary>
        /// <param name="keys">die Tasten</param>
        /// <returns>wurde eine Taste gerade gedrückt</returns>
        public bool KeyPressed (params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (prevKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key))
                    return true;
            return false;
        }

        /// <summary>
        /// Gibt an, ob verschiedene Tasten gerade losgelassen wurden sind.
        /// </summary>
        /// <param name="keys">die Tasten</param>
        /// <returns>wurde eine Taste gerade losgelassen</returns>
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
