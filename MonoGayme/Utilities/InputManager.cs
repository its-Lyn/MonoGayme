using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGayme.Utilities;

public static class InputManager
{
    private static KeyboardState _previousState;
    private static KeyboardState _currentState;

    private static GamePadState _previousControllerState;
    private static GamePadState _currentControllerState;

    static InputManager()
    {
        _currentState = Keyboard.GetState();
        _previousState = _currentState;

        _currentControllerState = GamePad.GetState(PlayerIndex.One);
        _previousControllerState = _currentControllerState;
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
    /// Check if a controller button is down.
    /// </summary>
    public static bool IsGamePadDown(Buttons btn) => _currentControllerState.IsButtonDown(btn);
    
    /// <summary>
    /// Check if a controller button is up.
    /// </summary>
    public static bool IsGamePadUp(Buttons btn) => _currentControllerState.IsButtonUp(btn);

    /// <summary>
    /// Check if a controller button is being pressed. 
    /// </summary>
    public static bool IsGamePadPressed(Buttons btn) => _currentControllerState.IsButtonDown(btn) && _previousControllerState.IsButtonUp(btn);

    /// <summary>
    /// Update the input device state. Must only be ran once a frame.
    /// </summary>
    public static void GetState()
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();

        _previousControllerState = _currentControllerState;
        _currentControllerState = GamePad.GetState(PlayerIndex.One);
    }
}
