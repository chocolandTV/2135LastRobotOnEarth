using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeThruster : MonoBehaviour
{
    // UPGRADE 01  Tracks IMPROVE MOVEMENT SPEED
    // SCRIPTABLEOBJECT  COSTS AND MULTIPLIER BALANCER
    [SerializeField] private UpgradeSettings upgradeSettings;
    
    // END SCRIPTABLEOBJECTS
    // Start is called before the first frame update
    private string upgradeName = "Upgrade Thruster";
    private int level = 0;
    private int maxlevel = 9;

    void Start()
    {
        upgradeSettings.activeLevel = level;
    }
    public void OnButtonClickUpgrade()
    {
        Debug.Log(upgradeName);
        if(level < maxlevel)
        {
            if(ResourceManager.Instance.canPurchase(upgradeSettings.costs[level]))
            {
                ResourceManager.Instance.RemoveRocketResource(upgradeSettings.costs[level]);
                
                Upgrade();
            }
            else{
                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color= Color.red;
                UpgradeUIManager.Instance.ErrorMessage_NotEnoughScrap();
                // NOT ENOUGH SCRAP MESSAGE
            }

        }
        else{
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color= Color.red;
            UpgradeUIManager.Instance.ErrorMessage_ReachedMaxLevel();
            // LEVEL MAX MESSAGE
        }
    }
    private void Upgrade()
    {
        level++;
        upgradeSettings.activeLevel = level;
        VariableManager.Instance.SetThrusterPower(upgradeSettings.multiplier[level]);
        if(level ==1){
            PlayerController.Instance.ChangeThrusterUpgrade();
        }
        UpgradeUIManager.Instance.UpdateUI();
        HUDManager.Instance.OnChangeScrapUI();
        // SOUND 
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
        PlayerAnimate.Instance.PlayerStartAnimateRemote(1);
    }
}
