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
    [SerializeField]private float movementSpeed = 5.0f; // Upgrade Multiplier 1; 10
    private Vector2 _moveInput;
    private Vector2 _lookInputDelta;
    private Vector2 _lookVector;
    [SerializeField] private float rotateSpeed = 0.25f;
    public bool isRecycling{get;set;}
    [SerializeField] private Transform followTransform;

    // LOOK VARS
    [SerializeField] private float rotationPowerX = 3f;
    [SerializeField] private float rotationPowerY = 3f;
    public Vector3 nextPosition;
    private Camera _camera;
    // UPGRADE STORE BOOL
    public bool isUpgradable{get;set;} = false;
    // ATTRACTING COLLIDER ENABLE
    [SerializeField] private new Collider collider;

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
    

    }
 
    private void Move()
    {
        
        float step = (movementSpeed* VariableManager.Instance.Game_movement_multiplier)* Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition,step);
        
    }
    private void HandleInput()
    {
        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.x * rotationPowerX* Time.fixedDeltaTime, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.y * rotationPowerY *Time.fixedDeltaTime, Vector3.right);
        _lookInputDelta = Vector2.zero;
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
        // CAMERA ROTATION DONE
        //  IF NO INPUT  -> FREE LOOK
        if (_moveInput.x == 0 && _moveInput.y == 0) 
        {   
            nextPosition = transform.position;
            return; 
        }
        //CHARACTER ROTATION LERP

        float _moveSpeed = movementSpeed * Time.fixedDeltaTime;
        Vector3 position = (transform.forward * _moveInput.y * _moveSpeed) + (transform.right * _moveInput.x * _moveSpeed);
        nextPosition = transform.position + position;        
        Quaternion oldrotation = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), rotateSpeed*Time.fixedDeltaTime);
        //Set the player rotation based on the look transform
        
        //reset the y rotation of the look transform
        
        float xRotChange = oldrotation.eulerAngles.x - transform.rotation.eulerAngles.x;
        float yRotChange = oldrotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        followTransform.transform.localRotation*= Quaternion.AngleAxis(yRotChange,Vector3.up);
    }
    
    private void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookInputDelta += context.ReadValue<Vector2>();
            
        }
        else if (context.canceled)
        {
            _lookInputDelta = Vector2.zero;
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
            if(HUDManager.Instance.isUpgradestoreActive() && isUpgradable)
            {
                UpgradeUIManager.Instance.UpgradeStoreSetActive(false);
                // Disable
                HUDManager.Instance.OnUpgradeStoreChange(false);
                HUDManager.Instance.OnCrossHairChange(true);
                // START UPGRADE SYSTEM
                UpgradeUIManager.Instance.UpgradeStoreCursorActive(true); 
                //  HERE START ROBOT UPGRADE EVENT
            }
        }
        
        
    }
    private void OnRecycleInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRecycling= true;
            collider.enabled=true;
        }
        if (context.canceled)
        {
            isRecycling = false;
            collider.enabled=false;
        }
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();
    }
}
