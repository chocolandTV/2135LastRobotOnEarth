using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUILookAtCamera : MonoBehaviour
{
    private new Camera camera;

    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    private void OnEnable() {
        if(camera != null){
        transform.eulerAngles = camera.transform.eulerAngles;
        }
    }
}
