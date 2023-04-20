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
    [SerializeField] private float movementSpeed = 12.5f; // Upgrade Multiplier 1; 10
    private float UpgradedMovementSpeed => movementSpeed * VariableManager.Instance.Game_movement_multiplier;
    private Vector2 _moveInput;
    private Vector2 _lookInputDelta;
    private Vector2 _lookVector;
    [SerializeField] private float rotateSpeed = 0.25f;
    [SerializeField] private Transform followTransform;

    // LOOK VARS
    [SerializeField] private float rotationPowerX = 3f;
    [SerializeField] private float rotationPowerY = 3f;
    public Vector3 nextPosition;
    private Camera _camera;
    // UPGRADE STORE BOOL
    public bool isUpgrading { get; set; } = false;
    // TRACK ANIMATION
    [SerializeField] private Renderer playerTrackRenderer;
    private bool isJumping = false;
    [SerializeField] private Transform isGroundedCheckObject;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private ParticleSystem ThrusterParticleSystem;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    private void Awake()
    {
        if (Instance != null)
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

        _lookVector = Vector2.zero;
        _camera = Camera.main;

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
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleInput();
        Move();
        
    }

    private void Move()
    {
        float step = (UpgradedMovementSpeed * VariableManager.Instance.Game_movement_multiplier) * Time.fixedDeltaTime;
        // playerTrackRenderer.material.SetTextureOffset ("_MainTex",Vector2.left*0.1f);
        _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, nextPosition, step));
    }
    private void HandleInput()
    {
        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.x * rotationPowerX * Time.fixedDeltaTime, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(_lookInputDelta.y * rotationPowerY * Time.fixedDeltaTime, Vector3.right);
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
        _rigidbody.AddForce(Vector3.up* jumpForce *VariableManager.Instance.Game_thruster_power);
        ThrusterParticleSystem.Emit(emitParams, 100);
        StartCoroutine(WaitUntilGrounded());
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
                ChangeControlUpgrade(true);
                
            }
        }


    }
    private void OnThrusterInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isJumping && isGrounded())
            {
                isJumping = true;
                ThrusterImpulse();
                
            }
        }
        // if(context.canceled)
        // {
        //     if(isJumping)
        //     {
        //         Vector3 rbVelocity =  _rigidbody.velocity;
        //         rbVelocity.y = -1.89f;
        //         _rigidbody.velocity = rbVelocity;
        //     }
        // }


    }
    IEnumerator WaitUntilGrounded()
    {
        while (!isGrounded())
        {
            yield return new WaitForSeconds(1);

        }
        isJumping = false;
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
