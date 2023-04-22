using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksAnimation : MonoBehaviour
{
    [SerializeField] private Renderer playerTrackRenderer;
    private new Rigidbody rigidbody;
    private AudioSource Tracksound;
    private float velocity;
    private float offset= 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // playerTrackRenderer = GetComponent<Renderer>();
    }

    private void FixedUpdate() {
        HandlePlayerMovement();
    }
    private void HandlePlayerMovement()
    {
        
        float velocity = rigidbody.velocity.magnitude;
       
        if(velocity > 1)
        {
            TracksAnim(-velocity* Time.fixedDeltaTime);
            TracksSound(velocity);
        }
    }
    private void TracksSound(float volume)
    {
        Debug.Log(volume);
        Tracksound.volume  = Mathf.Clamp(volume, 0, 0.1f);
    }
   private void TracksAnim(float value)
    {
        offset += value;
        playerTrackRenderer.materials[0].SetTextureOffset ("_BaseMap", new Vector2(offset,offset));
    }
}
