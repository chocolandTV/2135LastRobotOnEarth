using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ResourceSource : MonoBehaviour
{
    public enum ResourceType
    {
        Scrap,
        Figures,
        Tungsten,
    }
    public ResourceType type;
    public int quantity{get; set;}
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Recycler"))
        {
            // ADD RESOURCE
            for (int i = 100; i > 1; i--)
            {
                gameObject.transform.localScale  = (Vector3.one * i/100);
            }
            Destroy(gameObject);
        }
    }

}
