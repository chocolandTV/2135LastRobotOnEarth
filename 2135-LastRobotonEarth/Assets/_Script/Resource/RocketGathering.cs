using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGathering : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Recycler"))
        {
            int holdScraps = ResourceManager.Instance.GamePlayerScraps;
            ResourceManager.Instance.RemovePlayerResource();
            ResourceManager.Instance.AddResourceRocket(holdScraps);
            system.Emit(emitParams, holdScraps);
            HUDManager.Instance.OnChangeScrapUI();
        }
    }
    
}
