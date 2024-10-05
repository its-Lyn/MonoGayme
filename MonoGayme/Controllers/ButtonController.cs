using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGayme.Components;
using MonoGayme.UI;
using MonoGayme.Utilities;

namespace MonoGayme.Controllers;

/// <param name="allowInput">Allow buttons to be naviagted via keyboard or controller</param>
public class ButtonController(bool allowNavigation) : Component
{
    private List<Button> _buttons = [];
    private readonly HashSet<Button> _toRemove = [];

    private Keys? _kbUp;
    private Keys? _kbDown;
    private Keys? _kbAccept;

    private Buttons? _gpUp;
    private Buttons? _gpDown;
    private Buttons? _gpAccept;

    private int _activeIdx = 0;

    /// <summary>
    /// Ran after the active button is changed.
    /// </summary>
    public Action<Button>? OnActiveUpdated;

    /// <summary>
    /// Ran before the active button is changed
    /// </summary>
    public Action<Button>? OnActiveUpdating;

    /// <summary>
    /// Set the keyboard UI navigation keys. Should only be called if allowNavigation is true. 
    /// </summary>
    public void SetKeyboardButtons(Keys kbUp, Keys kbDown, Keys kbAccept)
    {
        if (!allowNavigation)
        {
            Console.Error.WriteLine("Keyboard navigation disabled! Skipping.");
            return;
        }

        _kbUp = kbUp;
        _kbDown = kbDown;
        _kbAccept = kbAccept;
    }

    /// <summary>
    /// Set the controoler UI navigation keys. Should only be called if allowNavigation is true.
    /// </summary>
    public void SetControllerButtons(Buttons gpUp, Buttons gpDown, Buttons gpAccept)
    {
        if (!allowNavigation)
        {
            Console.Error.WriteLine("Controller navigation disabled! Skipping.");
            return;
        }

        _gpDown = gpDown;
        _gpUp = gpUp;
        _gpAccept = gpAccept;
    }

    public void Add<T>(T button) where T : Button
    {
        _buttons.Add(button);

        if (allowNavigation && _buttons.Count == 1)
        {
            OnActiveUpdated?.Invoke(_buttons[_activeIdx]);
        }
    }

    public void QueueRemoveAll()
    {
        foreach (Button button in _buttons)
        {
            _toRemove.Add(button);
        }
    }

    public void Update(Vector2 mouse)
    {
        if (_activeIdx > _buttons.Count - 1)
        {
            _activeIdx = _buttons.Count - 1;
        }

        foreach (Button button in _buttons)
        {
            button.Update(mouse);
        }

        if (_toRemove.Count > 0)
        {
            _buttons.RemoveAll(_toRemove.Contains);
            _toRemove.Clear();
        }

        if (!allowNavigation) return;

        if (_gpDown.HasValue && _gpUp.HasValue && _gpAccept.HasValue)
        {
            if (InputManager.IsGamePadPressed(_gpAccept.Value))
            {
                _buttons[_activeIdx].RunAction();
            }

            if (InputManager.IsGamePadPressed(_gpUp.Value))
            {
                OnActiveUpdating?.Invoke(_buttons[_activeIdx]);

                _activeIdx--;
                if (_activeIdx < 0)
                {
                    _activeIdx = 0;
                }

                OnActiveUpdated?.Invoke(_buttons[_activeIdx]);
            }

            if (InputManager.IsGamePadPressed(_gpDown.Value))
            {
                OnActiveUpdating?.Invoke(_buttons[_activeIdx]);

                _activeIdx++;
                if (_activeIdx > _buttons.Count - 1)
                {
                    _activeIdx = _buttons.Count - 1;
                }

                OnActiveUpdated?.Invoke(_buttons[_activeIdx]);
            }
        }

        if (_kbDown.HasValue && _kbUp.HasValue && _kbAccept.HasValue)
        {
            if (InputManager.IsKeyPressed(_kbAccept.Value))
            {
                _buttons[_activeIdx].RunAction();
            }

            if (InputManager.IsKeyPressed(_kbUp.Value))
            {
                OnActiveUpdating?.Invoke(_buttons[_activeIdx]);

                _activeIdx--;
                if (_activeIdx < 0)
                {
                    _activeIdx = 0;
                }

                OnActiveUpdated?.Invoke(_buttons[_activeIdx]);
            }

            if (InputManager.IsKeyPressed(_kbDown.Value))
            {
                OnActiveUpdating?.Invoke(_buttons[_activeIdx]);

                _activeIdx++;
                if (_activeIdx > _buttons.Count - 1)
                {
                    _activeIdx = _buttons.Count - 1;
                }

                OnActiveUpdated?.Invoke(_buttons[_activeIdx]);
            }
        }
    }

    public void Draw(SpriteBatch batch, Camera2D? camera = null)
    {
        foreach (Button button in _buttons)
            button.Draw(batch, camera);
    }
}
