using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get;private set;}

    // Player Vars
    private Rigidbody _rigidbody;
    [SerializeField]private float movementSpeed = 5.0f;
    private Vector2 PlayerDirection;
    private Vector2 _moveInput;
    private Vector2 _lookInputDelta;
    private Vector2 _lookVector;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    public Vector2 velocity = Vector2.zero;
    public float magnitude = 0.25f;
    private bool isRecycling;
   
    [SerializeField] private Transform followTransform;

    // LOOK VARS
    [SerializeField] private float rotationPowerX = 3f;
    [SerializeField] private float rotationPowerY = 3f;
    // [SerializeField] private float rotationLerp = 0.5f;
    private Quaternion nextRotation;
    public Vector3 nextPosition;
    private Camera _camera;
    

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        SubscribeToInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        isRecycling = false;
        _lookVector = Vector2.zero;
        _camera = Camera.main;
        
    }

    private void SubscribeToInput()
    {
        InputManager.OnLook += OnLookInput;
        InputManager.OnMove += OnMoveInput;
        InputManager.OnInteract += OnInteractInput;
        InputManager.OnRecycle += OnRecycleInput;
    }
     private void UnsubscribeFromInput()
    {
        InputManager.OnLook -= OnLookInput;
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnInteract -= OnInteractInput;
        InputManager.OnRecycle -= OnRecycleInput;
    }
    // Update is called once per frame
    private void FixedUpdate() {
       HandleInput();
       Move();
        // Recycle();

    }
    private void Move()
    {
         Vector3 worldDeltaPosition = nextPosition - transform.position;

        //Map to local space
        float dX = Vector3.Dot(transform.right, worldDeltaPosition);
        float dY = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dX, dY);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }

        
        transform.position = nextPosition;
    }
    private void HandleInput()
    {
      

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.x * rotationPowerX, Vector3.up);

        
        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.y * rotationPowerY, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
        
        float _moveSpeed = movementSpeed / 100f;
        Vector3 position = (transform.forward * _moveInput.y * _moveSpeed) + (transform.right * _moveInput.x * _moveSpeed);
        nextPosition = transform.position + position;        
        

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
    
    private void Recycle()
    {
        if(isRecycling)
        {

        }
    }
    private void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookInputDelta = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _lookInputDelta = Vector2.zero;
        }
    }
    public Vector2 GetDelta()
    {
        
        if (_lookInputDelta != null)
        {
            return _lookInputDelta;
        }
        else
        {
            return Vector2.zero;
        }
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // INTERACT Set Object Parent RobotHand
        }
        if (context.canceled)
        {
            // INTERACT unSet Object Parent RobotHand
        }
    }
    private void OnRecycleInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRecycling= true;
        }
        if (context.canceled)
        {
            isRecycling = false;
        }
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();
    }
}
