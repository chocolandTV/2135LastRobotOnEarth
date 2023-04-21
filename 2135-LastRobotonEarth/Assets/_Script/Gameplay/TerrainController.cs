using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;
public class TerrainController : MonoBehaviour
{
    [SerializeField] private TerrainData[] alphaMaps;
    private Terrain terrain;
    [SerializeField] private ParticleSystem terrainParticleSystem;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    public static TerrainController Instance;

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData= alphaMaps[0]; // CHANGE ON PLAYERPEFS
    }

    public void ChangeTerrainData(int value)
    {
        terrain.terrainData= alphaMaps[value];
        terrainParticleSystem.Emit(emitParams,100 * (int)VariableManager.Instance.Game_Terraformer_mission);
    }
}
