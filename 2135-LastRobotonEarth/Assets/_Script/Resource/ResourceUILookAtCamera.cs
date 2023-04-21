using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceUILookAtCamera : MonoBehaviour
{
    private new Camera camera;
    // [SerializeField] public TextMeshProUGUI text;
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
