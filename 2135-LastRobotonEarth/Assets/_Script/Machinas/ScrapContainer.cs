using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapContainer : MonoBehaviour
{
    public static ScrapContainer Instance { get;private set;}
    [SerializeField]private GameObject[]ScrapObjects; 
    
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public GameObject GetRandomScrapObject()
    {
        return ScrapObjects[Random.Range(0, ScrapObjects.Length)];
    }
}
