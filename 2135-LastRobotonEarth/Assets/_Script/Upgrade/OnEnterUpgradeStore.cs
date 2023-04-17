using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterUpgradeStore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            HUDManager.Instance.OnUpgradeStoreChange(true);
            HUDManager.Instance.OnCrossHairChange(false);
            PlayerController.Instance.isUpgradable= true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            HUDManager.Instance.OnUpgradeStoreChange(false);
            HUDManager.Instance.OnCrossHairChange(true);
            PlayerController.Instance.isUpgradable= false;
        }
    }
}