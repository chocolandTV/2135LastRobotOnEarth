using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksAnimation : MonoBehaviour
{
    [SerializeField] private Renderer playerTrackRenderer;
    private new Rigidbody rigidbody;
    private float velocity;
    private float offset= 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        HandlePlayerMovement();
    }
    private void HandlePlayerMovement()
    {
        velocity = rigidbody.velocity.x + rigidbody.velocity.y + rigidbody.velocity.z;
        Debug.Log(velocity);
        if(velocity > 1)
        {
            TracksAnim(velocity);
            TracksSound((float)velocity /10);
        }
    }
    private void TracksSound(float value)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Robot_Moving, PlayerController.Instance.gameObject.transform.position, value);
    }
   private void TracksAnim(float value)
    {
        offset += value;
        playerTrackRenderer.material.SetTextureOffset ("_MainTex", new Vector3(offset,0,0));
    }
}
