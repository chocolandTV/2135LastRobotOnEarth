using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksAnimation : MonoBehaviour
{
    [SerializeField] private Renderer playerTrackRenderer;
    private new Rigidbody rigidbody;
    [SerializeField] private AudioSource Tracksound;
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
       
        
        TracksAnim(-velocity);
        // TracksSound(velocity);
        
    }
    private void TracksSound(float volume)
    {
        
        Tracksound.volume  = Mathf.Clamp(volume/20, 0, 0.1f);
    }
   private void TracksAnim(float value)
    {
        offset += value;
        if(offset > 10000.0f || offset < -10000.0f)
            offset = 0;
        playerTrackRenderer.materials[0].SetTextureOffset ("_BaseMap", new Vector2(offset,offset));
    }
}
