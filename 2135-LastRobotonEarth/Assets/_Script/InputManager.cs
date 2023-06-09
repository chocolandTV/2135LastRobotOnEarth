using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get;private set;}
    private PlayerInput _playerInput;
    public static bool IsKeyboardAndMouse => Instance._playerInput.currentControlScheme != "Gamepad";

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += OnShemeChanged;
        SubscribeToInput();
        DontDestroyOnLoad(gameObject);
    }
    public static event Action OnControlShemeChanged;
    public static event Action<CallbackContext> OnLook;
    public static event Action<CallbackContext> OnMove;
    public static event Action<CallbackContext> OnInteract;
    public static event Action<CallbackContext> OnPauseGame;
    public static event Action<CallbackContext> OnThruster;
   
    private void OnShemeChanged(PlayerInput playerInput)
    {
        OnControlShemeChanged?.Invoke();
    }
    private void OnLookInput(CallbackContext context)
    {
        OnLook?.Invoke(context);
    }
    private void OnMoveInput(CallbackContext context)
    {
        OnMove?.Invoke(context);
    }
    private void OnInteractInput(CallbackContext context)
    {
        OnInteract?.Invoke(context);
    }
    private void OnPauseGameInput(CallbackContext context)
    {
        OnPauseGame?.Invoke(context);
    }
    private void OnThrusterInput(CallbackContext context)
    {
        OnThruster?.Invoke(context);
    }

    /// SUBSCRIBE TO INPUT
    private void SubscribeToInput()
    {
        _playerInput.actions["Look"].started += OnLookInput;
        _playerInput.actions["Look"].performed += OnLookInput;
        _playerInput.actions["Look"].canceled += OnLookInput;

        _playerInput.actions["Move"].started += OnMoveInput;
        _playerInput.actions["Move"].performed += OnMoveInput;
        _playerInput.actions["Move"].canceled += OnMoveInput;

        _playerInput.actions["Interact"].started += OnInteractInput;
        _playerInput.actions["Interact"].performed += OnInteractInput;
        _playerInput.actions["Interact"].canceled += OnInteractInput;

        _playerInput.actions["PauseGame"].started += OnPauseGameInput;
        _playerInput.actions["PauseGame"].performed += OnPauseGameInput;
        _playerInput.actions["PauseGame"].canceled += OnPauseGameInput;

        _playerInput.actions["OnThruster"].started += OnThrusterInput;
        _playerInput.actions["OnThruster"].performed += OnThrusterInput;
        _playerInput.actions["OnThruster"].canceled += OnThrusterInput;
    }
    private void UnsubscribeFromInput()
    {
         _playerInput.actions["Look"].started -= OnLookInput;
        _playerInput.actions["Look"].performed -= OnLookInput;
        _playerInput.actions["Look"].canceled -= OnLookInput;

        _playerInput.actions["Move"].started -= OnMoveInput;
        _playerInput.actions["Move"].performed -= OnMoveInput;
        _playerInput.actions["Move"].canceled -= OnMoveInput;

        _playerInput.actions["Interact"].started -= OnInteractInput;
        _playerInput.actions["Interact"].performed -= OnInteractInput;
        _playerInput.actions["Interact"].canceled -= OnInteractInput;

        _playerInput.actions["PauseGame"].started -= OnPauseGameInput;
        _playerInput.actions["PauseGame"].performed -= OnPauseGameInput;
        _playerInput.actions["PauseGame"].canceled -= OnPauseGameInput;

        _playerInput.actions["OnThruster"].started -= OnThrusterInput;
        _playerInput.actions["OnThruster"].performed -= OnThrusterInput;
        _playerInput.actions["OnThruster"].canceled -= OnThrusterInput;
    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
            UnsubscribeFromInput();
        }
    }
    [ContextMenu("Debug print controlsheme")]
    private void DebugPrintControlsheme()
    {
        Debug.Log(" Is keyboard and mouse: " + IsKeyboardAndMouse);
    }
    
}
