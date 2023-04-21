using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Scrap"))
        {
            Destroy(gameObject);
        }
        if(other.CompareTag("Player"))
        {
            other.transform.position = PlayerController.Instance.startposition;
        }
    }
}
