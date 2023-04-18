using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStoreOpener : MonoBehaviour
{
    private void OpenUpgrades() {
        
            HUDManager.Instance.OnUpgradeStoreChange(true);
            HUDManager.Instance.OnCrossHairChange(false);
            PlayerController.Instance.isUpgrading= true;
        
    }
    private void CloseUpgrades() {
        
            HUDManager.Instance.OnUpgradeStoreChange(false);
            HUDManager.Instance.OnCrossHairChange(true);
            PlayerController.Instance.isUpgrading= false;
       
    }
}
