using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLocomotionController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _movement;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    public Vector2 velocity = Vector2.zero;
    public float magnitude = 0.25f;

    private void OnEnable()
    {
        _movement = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
    public bool shouldMove;
    public bool shouldTurn;
    public float turn;



    public void Update()
    {
        Vector3 worldDeltaPosition = _movement.nextPosition - transform.position;

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

        shouldMove = velocity.magnitude > magnitude;
        transform.position = _movement.nextPosition;
        

        
        
        // _animator.SetBool("IsMoving", shouldMove);
        // _animator.SetFloat("VelocityX", velocity.x);
        // _animator.SetFloat("VelocityY", Mathf.Abs(velocity.y));

    }





}
