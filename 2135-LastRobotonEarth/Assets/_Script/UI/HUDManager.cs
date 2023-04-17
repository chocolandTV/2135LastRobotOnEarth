using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scrapValueUI;
    [SerializeField] private TextMeshProUGUI objectiveNameUI;
    [SerializeField] private TextMeshProUGUI objectiveValueUI;
    private string objectNameDefault = "PRIMARY:";
    private string objectValueDefault ="Repair the Shuttle";
    public static HUDManager Instance{get;set;}
    // UPGRADE STORE 
    [SerializeField] private GameObject hudCrossHair;
    [SerializeField] private GameObject hudUpgradeUI;
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    public bool isUpgradestoreActive()
    {
        return (hudUpgradeUI.activeSelf);
    }
    public void OnUpgradeStoreChange(bool value)
    {
        hudUpgradeUI.SetActive(value);
    }
    public void OnCrossHairChange(bool value)
    {
        hudCrossHair.SetActive(value);
    }
    public void PlayerTankFullMission()
    {
        objectiveNameUI.fontSize = 26;
        objectiveNameUI.text = "SECONDARY:";
        objectiveValueUI.fontSize = 26;
        objectiveValueUI.text = "Deliver the scrap to the shuttle.";
    }
    public void PlayerEnterShuttleMission()
    {
        objectiveNameUI.fontSize = 26;
        objectiveNameUI.text = "IMPORTANT:";
        objectiveValueUI.fontSize = 26;
        objectiveValueUI.text = "Press E to open the Upgradestore";
    }
    public void PlayerDefaultMission()
    {
        objectiveNameUI.fontSize = 36;
        objectiveNameUI.text = objectNameDefault;
        objectiveValueUI.fontSize = 36;
        objectiveValueUI.text = objectValueDefault;
    }
    public void OnChangeScrapUI()
    {
        scrapValueUI.text  = ResourceManager.Instance.GameRocketScraps.ToString();
    }
}