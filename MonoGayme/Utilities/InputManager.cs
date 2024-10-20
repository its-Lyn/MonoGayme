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
    /// Get first pressed key on the keyboard.
    /// </summary>
    public static Keys? GetFirstKey()
    {
        Keys[] keys = _currentState.GetPressedKeys();
        if (keys.Length > 0)
            return keys[0];

        return null;
    }
    
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
    /// Get first pressed button on the gamepad.
    /// </summary>
    public static Buttons? GetFirstButton()
    {
        // WHat the fuck
        if (_currentControllerState.Buttons.A == ButtonState.Pressed) return Buttons.A;
        if (_currentControllerState.Buttons.B == ButtonState.Pressed) return Buttons.B;
        if (_currentControllerState.Buttons.X == ButtonState.Pressed) return Buttons.X;
        if (_currentControllerState.Buttons.Y == ButtonState.Pressed) return Buttons.Y;
        if (_currentControllerState.Buttons.Start == ButtonState.Pressed) return Buttons.Start;
        if (_currentControllerState.Buttons.Back == ButtonState.Pressed) return Buttons.Back;
        if (_currentControllerState.Buttons.LeftShoulder == ButtonState.Pressed) return Buttons.LeftShoulder;
        if (_currentControllerState.Buttons.RightShoulder == ButtonState.Pressed) return Buttons.RightShoulder;
        if (_currentControllerState.Buttons.LeftStick == ButtonState.Pressed) return Buttons.LeftStick;
        if (_currentControllerState.Buttons.RightStick == ButtonState.Pressed) return Buttons.RightStick;

        if (_currentControllerState.DPad.Up == ButtonState.Pressed) return Buttons.DPadUp;
        if (_currentControllerState.DPad.Down == ButtonState.Pressed) return Buttons.DPadDown;
        if (_currentControllerState.DPad.Left == ButtonState.Pressed) return Buttons.DPadLeft;
        if (_currentControllerState.DPad.Right == ButtonState.Pressed) return Buttons.DPadRight;
        
        if (_currentControllerState.Triggers.Left > 0.5f) return Buttons.LeftTrigger;
        if (_currentControllerState.Triggers.Right > 0.5f) return Buttons.RightTrigger;
        if (_currentControllerState.ThumbSticks.Left.X < -0.5f) return Buttons.LeftThumbstickLeft;
        if (_currentControllerState.ThumbSticks.Left.X > 0.5f) return Buttons.LeftThumbstickRight;
        if (_currentControllerState.ThumbSticks.Left.Y < -0.5f) return Buttons.LeftThumbstickDown;
        if (_currentControllerState.ThumbSticks.Left.Y > 0.5f) return Buttons.LeftThumbstickUp;
        if (_currentControllerState.ThumbSticks.Right.X < -0.5f) return Buttons.RightThumbstickLeft;
        if (_currentControllerState.ThumbSticks.Right.X > 0.5f) return Buttons.RightThumbstickRight;
        if (_currentControllerState.ThumbSticks.Right.Y < -0.5f) return Buttons.RightThumbstickDown;
        if (_currentControllerState.ThumbSticks.Right.Y > 0.5f) return Buttons.RightThumbstickUp;
        
        return null;
    }
    
    /// <summary>
    /// Update the input device state. Must only be run once a frame.
    /// </summary>
    public static void GetState()
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();

        _previousControllerState = _currentControllerState;
        _currentControllerState = GamePad.GetState(PlayerIndex.One);
    }
}
