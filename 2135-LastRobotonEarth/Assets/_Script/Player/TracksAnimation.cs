using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksAnimation : MonoBehaviour
{
    [SerializeField] private Renderer playerTrackRenderer;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void TacksAnim(Vector2 vec)
    {
        
        playerTrackRenderer.material.SetTextureOffset ("_MainTex", new Vector3(vec.x,vec.y,0));
    }
}
