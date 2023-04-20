using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;
public class TerrainController : MonoBehaviour
{
    [SerializeField] private TerrainData alphaTest01;
    private Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData= alphaTest01;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
