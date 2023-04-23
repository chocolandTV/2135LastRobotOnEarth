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
    private string objectValueDefault ="Collect Scraps from nearby Objects";
    public static HUDManager Instance{get;set;}
    // UPGRADE STORE 
    [SerializeField] private GameObject hudCrossHair;
    [SerializeField] private GameObject hudUpgradeUI;
    [SerializeField]private GameObject particleWin;
    private bool isWinning = false;
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
        if(!isWinning){
        objectiveNameUI.fontSize = 26;
        objectiveNameUI.text = "SECONDARY:";
        objectiveValueUI.fontSize = 26;
        objectiveValueUI.text = "Deliver the scrap to the Terraformer.";
        SoundManager.Instance.PlaySound(SoundManager.Sound.Robot_StorageFull, PlayerController.Instance.gameObject.transform.position);}
    }
    public void PlayerEnterShuttleMission()
    {
        if(!isWinning){
            objectiveNameUI.fontSize = 26;
            objectiveNameUI.text = "IMPORTANT:";
            objectiveValueUI.fontSize = 26;
            objectiveValueUI.text = "Press E to open the upgradestore";}
    }
    public void PlayerEnterUpgradeStore()
    {
        if(!isWinning){
            objectiveNameUI.fontSize = 26;
            objectiveNameUI.text = "IMPORTANT:";
            objectiveValueUI.fontSize = 24;
            objectiveValueUI.text = "Upgrade your performance and Terraformer to max";}
    }
    public void PlayerExitUpgradeStore()
    {
        if(!isWinning){
            objectiveNameUI.fontSize = 30;
            objectiveNameUI.text = "IMPORTANT:";
            objectiveValueUI.fontSize = 36;
            objectiveValueUI.text = "Collect Scraps from nearby Objects";}
    }
    public void PlayerWinGame()
    {
        objectiveNameUI.fontSize = 32;
        objectiveNameUI.text = "You Won:";
        objectiveValueUI.fontSize = 38;
        objectiveValueUI.text = " Thanks for playing! ";
        particleWin.SetActive(true);
        isWinning=true;

    }
    public void PlayerDefaultMission()
    {
        if(!isWinning){
            objectiveNameUI.fontSize = 36;
            objectiveNameUI.text = objectNameDefault;
            objectiveValueUI.fontSize = 36;
            objectiveValueUI.text = objectValueDefault;}
    }
    public void OnChangeScrapUI()
    {
        scrapValueUI.text  = ResourceManager.Instance.GameRocketScraps.ToString();
    }
}
