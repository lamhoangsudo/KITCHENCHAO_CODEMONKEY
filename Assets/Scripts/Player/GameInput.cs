using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_KEY_CONTROLLER = "PlayerKeyControll";
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPause;
    private PlayerInput inputs;
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlt,
        Pause
    }
    public static GameInput gameInput { get; private set; }
    private void Awake()
    {
        //screen change this obj isn't destroy auto 
        //that obj is try to show the obj previous is already destroy
        inputs = new PlayerInput();
        if (PlayerPrefs.HasKey(PLAYER_KEY_CONTROLLER))
        {
            inputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_KEY_CONTROLLER));
        }
        gameInput = this;
        inputs.Player.Enable();
        inputs.Player.Interact.performed += Interract_performed;
        inputs.Player.InteractAlternate.performed += InteractAlternate_performed;
        inputs.Player.Pause.performed += Pause_performed;
    }
    //FIX:
    //unsubscribe all action previous
    //or
    //dispose obj when this obj is destroy
    //OnDestroy
    private void OnDestroy()
    {
        inputs.Player.Interact.performed -= Interract_performed;
        inputs.Player.InteractAlternate.performed -= InteractAlternate_performed;
        inputs.Player.Pause.performed -= Pause_performed;
        inputs.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetVectorMovementnormalized()
    {
        Vector2 inputVector = inputs.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public string GetBinding(Binding binding)
    {
        return binding switch
        {
            Binding.MoveDown => inputs.Player.Move.bindings[2].ToDisplayString(),
            Binding.MoveLeft => inputs.Player.Move.bindings[3].ToDisplayString(),
            Binding.MoveRight => inputs.Player.Move.bindings[4].ToDisplayString(),
            Binding.Interact => inputs.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlt => inputs.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.Pause => inputs.Player.Pause.bindings[0].ToDisplayString(),
            _ => inputs.Player.Move.bindings[1].ToDisplayString(),
        };
    }
    public void RebindBinding(Binding binding, Action action)
    {
        inputs.Player.Disable();
        InputAction inputAction;
        int index;
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = inputs.Player.Move;
                index = 1;
                break;
            case Binding.MoveDown:
                inputAction = inputs.Player.Move;
                index = 2;
                break;
            case Binding.MoveLeft:
                inputAction = inputs.Player.Move;
                index = 3;
                break;
            case Binding.MoveRight:
                inputAction = inputs.Player.Move;
                index = 4;
                break;
            case Binding.Interact:
                inputAction = inputs.Player.Interact;
                index = 0;
                break;
            case Binding.InteractAlt:
                inputAction = inputs.Player.InteractAlternate;
                index = 0;
                break;
            case Binding.Pause:
                inputAction = inputs.Player.Pause;
                index = 0;
                break;
        }
        inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
        {
            callback.Dispose();
            inputs.Player.Enable();
            action();
            PlayerPrefs.SetString(PLAYER_KEY_CONTROLLER, inputs.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
