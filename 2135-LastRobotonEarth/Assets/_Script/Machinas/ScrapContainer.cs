using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapContainer : MonoBehaviour
{
    [SerializeField]private GameObject[]ScrapObjects; 
    // Start is called before the first frame update
    public GameObject GetRandomScrapObject()
    {
        return ScrapObjects[Random.Range(0, ScrapObjects.Length)];
    }
}
