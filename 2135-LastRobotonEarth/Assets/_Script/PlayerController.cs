using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    // Player Vars
    private Rigidbody _rigidbody;
    public Vector3 startposition;
    [SerializeField] private float movementSpeed = 12.5f; // Upgrade Multiplier 1; 10
    [SerializeField] private float smoothInputSpeed = 0.2f;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
   
    public float UpgradedMovementSpeed => movementSpeed * VariableManager.Instance.Game_movement_multiplier;
    private Vector2 _moveInput;
    private Vector2 _lookInputDelta;
    private Vector2 _lookVector;
    [SerializeField] private float rotateSpeed = 0.25f;
    [SerializeField] private Transform followTransform;

    // LOOK VARS
    [SerializeField] public float rotationPowerX = 3f;// MAIN MENU CHANGABLE
    [SerializeField] public float rotationPowerY = 3f;// MAIN MENU CHANGABLE
    public Vector3 nextPosition;
    private Camera _camera;
    // UPGRADE STORE BOOL
    public bool isUpgrading { get; set; } = false;
    // TRACK ANIMATION
    [SerializeField] private Renderer playerTrackRenderer;
    private bool isJumping = false;
    private int counter=0;
    [SerializeField] private Transform isGroundedCheckObject;
    [SerializeField] private float jumpForce = 25.0f;
    [SerializeField] private ParticleSystem ThrusterParticleSystem;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    // VERTICAL MOUSEMOVEMENT
    public Vector3 mouseVerticalVector = -Vector3.right; // MAIN MENU CHANGABLE
    // Thruster mats
    [SerializeField]private Material thrusterMat, thrusterReadyMat;
    [SerializeField] private Renderer ThrusterRenderer;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        startposition = transform.position;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        _lookVector = Vector2.zero;
        _camera = Camera.main;
        
        mouseVerticalVector = -Vector3.right;

    }
    public void StartGame()
    {
        SubscribeToInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void SubscribeToInput()
    {
        InputManager.OnLook += OnLookInput;
        InputManager.OnMove += OnMoveInput;
        InputManager.OnInteract += OnInteractInput;
        

    }
    private void UnsubscribeFromInput()
    {
        InputManager.OnLook -= OnLookInput;
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnInteract -= OnInteractInput;
        InputManager.OnThruster -= OnThrusterInput;
        
    }
    public void ChangeThrusterUpgrade()
    {
        InputManager.OnThruster += OnThrusterInput;
    }
    public void ChangeControlUpgrade(bool value)
    {
        if (value) // UPGRADE STORE ON
        {
            InputManager.OnLook -= OnLookInput;
            InputManager.OnMove -= OnMoveInput;
            InputManager.OnInteract -= OnInteractInput;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UpgradeUIManager.Instance.UpgradeStoreSetActive(true);
            HUDManager.Instance.OnCrossHairChange(false);
            // ADD DESCRIPTION IN UPGRADE STORE AND DELETE ERROR MESSAGES 
            HUDManager.Instance.PlayerEnterUpgradeStore();
            UpgradeUIManager.Instance.ErrorMessage_Hide();
            _moveInput = Vector2.zero;
        }
        else if (!value) // UPGRADE STORE OFF
        {
            InputManager.OnLook += OnLookInput;
            InputManager.OnMove += OnMoveInput;
            InputManager.OnInteract += OnInteractInput;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UpgradeUIManager.Instance.UpgradeStoreSetActive(false);
            PlayerController.Instance.isUpgrading = false;
            HUDManager.Instance.OnCrossHairChange(true);
            HUDManager.Instance.PlayerExitUpgradeStore();
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleInput();
        
        Move();
        
    }
 // float step = (UpgradedMovementSpeed * VariableManager.Instance.Game_movement_multiplier) * Time.fixedDeltaTime;
 // _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, nextPosition, smoothInputVelocity));
    private void Move()
    {
            currentInputVector= Vector2.SmoothDamp(currentInputVector,_moveInput, ref smoothInputVelocity, smoothInputSpeed,UpgradedMovementSpeed);
            Vector3 move = new Vector3(currentInputVector.x,0, currentInputVector.y);
            SpeedControl();
            _rigidbody.AddForce((nextPosition - transform.position) * UpgradedMovementSpeed * 10f, ForceMode.Force);
    }
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        if(flatVel.magnitude > UpgradedMovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * UpgradedMovementSpeed;
            _rigidbody.velocity = new Vector3 (limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }
    private void HandleInput()
    {
        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.x * rotationPowerX * Time.fixedDeltaTime, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.y * rotationPowerY * Time.fixedDeltaTime, mouseVerticalVector);
        _lookInputDelta = Vector2.zero;
        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
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

        float _moveSpeed = UpgradedMovementSpeed * Time.fixedDeltaTime;
        Vector3 position = (transform.forward * _moveInput.y * _moveSpeed) + (transform.right * _moveInput.x * _moveSpeed);
        nextPosition = transform.position + position;
        Quaternion oldrotation = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), rotateSpeed * Time.fixedDeltaTime);
        float xRotChange = oldrotation.eulerAngles.x - transform.rotation.eulerAngles.x;
        float yRotChange = oldrotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        followTransform.transform.localRotation *= Quaternion.AngleAxis(yRotChange, Vector3.up);
    }
    private void ThrusterImpulse()
    {
        // MAT CHANGE
        ThrusterRenderer.material = thrusterMat;
        _rigidbody.velocity = Vector3.zero;
        Debug.Log(jumpForce *VariableManager.Instance.Game_thruster_power);
        _rigidbody.AddForce(Vector3.up* jumpForce *VariableManager.Instance.Game_thruster_power,ForceMode.Impulse);
        ThrusterParticleSystem.Emit(emitParams, (int)(jumpForce *VariableManager.Instance.Game_thruster_power));
        counter = 0;
        StartCoroutine(WaitUntilCooldown());
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
            if (!isUpgrading)
            {
                isUpgrading = true;
                HUDManager.Instance.OnUpgradeStoreChange(false);
                ChangeControlUpgrade(true);

                
            }
        }


    }
    
    private void OnThrusterInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isJumping)
            {
                isJumping = true;
                ThrusterImpulse();
                
            }
        }
        


    }
    IEnumerator WaitUntilCooldown()
    {
        
        while (counter<= 3)
        {
            counter ++;
            yield return new WaitForSeconds(1);

        }
        // MAT CHANGE
        ThrusterRenderer.material = thrusterReadyMat;
        
        isJumping = false;
        counter =0;
    }
    
    private bool isGrounded()
    {
        return Physics.CheckSphere(isGroundedCheckObject.position,.1f, LayerMask.GetMask("Ground"));
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();
    }
}
