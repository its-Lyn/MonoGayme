using System;
using Microsoft.Xna.Framework.Input;

namespace MonoGayme.Utilities;

public static class InputManager {
    private static KeyboardState _previousState;
    private static KeyboardState _currentState;

    static InputManager() {
        _currentState = Keyboard.GetState();
        _previousState = _currentState;
    }

    /// <summary>
    /// Check if a key is being held down continuously.
    /// </summary>
    public static bool IsKeyDown(Keys key) => _currentState[key] == KeyState.Down;
    
    /// <summary>
    /// Check if a key is not currently held down.
    /// </summary>
    public static bool IsKeyUp(Keys key) => _currentState[key] == KeyState.Up;

    /// <summary>
    /// Check if a key has been pressed.
    /// </summary>
    public static bool IsKeyPressed(Keys key) =>_currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);

    /// <summary>
    /// Update the keyboard state. Must only be ran once a frame.
    /// </summary>
    public static void GetState() {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();
    }
}
