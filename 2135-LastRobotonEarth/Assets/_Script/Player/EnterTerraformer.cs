using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTerraformer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Terraformer"))
        {
            HUDManager.Instance.OnUpgradeStoreChange(true);
    
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Terraformer"))
        {
            HUDManager.Instance.OnUpgradeStoreChange(false);
    
        }
    }
}
