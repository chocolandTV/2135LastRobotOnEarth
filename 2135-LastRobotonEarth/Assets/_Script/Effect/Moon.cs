using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    
    Vector3 rotation = new Vector3(0,3,0);
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position);
    }
    // ROTATE 
    private void Rotate()
    {
        transform.Rotate(rotation*Time.fixedDeltaTime);
    }
    // DISTANCE TO PLAYER 
    private void KeepDistance()
    {
        transform.position = (transform.position - PlayerController.Instance.gameObject.transform.position).normalized * distance + PlayerController.Instance.gameObject.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        KeepDistance();
    }
}
