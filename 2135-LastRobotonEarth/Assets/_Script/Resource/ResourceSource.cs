using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSource : MonoBehaviour
{
    public int quantity;
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Recycler"))
        {
            if(ResourceManager.Instance.isSpaceInStorage())
            // ADD RESOURCE
            {    for (int i = 100; i > 1; i--)
                {
                    gameObject.transform.localScale  = (Vector3.one * i/100);
                }
                ResourceManager.Instance.AddResourcePlayer(quantity);
                Destroy(gameObject);
            }
        }
    }

}
