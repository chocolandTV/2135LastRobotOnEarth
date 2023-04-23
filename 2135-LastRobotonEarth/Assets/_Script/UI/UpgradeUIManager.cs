using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeUIManager : MonoBehaviour
{
    // ERRORMESSAGES
    // SOUNDS UPGRADED & MISS
    [SerializeField]private UpgradeSettings[] upgradesSettings;
    [SerializeField] private GameObject UpgradeStoreUI;
    [SerializeField] private GameObject error_NotEnoughScrap;
    [SerializeField] private GameObject Error_MaxLevel;
    [SerializeField] private TextMeshProUGUI[] CostTexts;
    [SerializeField] private TextMeshProUGUI[] levelTexts;
     [SerializeField] private TextMeshProUGUI[] ModifierTexts;
    [SerializeField] private RawImage[] levelProgressIcons;
    [SerializeField] private Texture2D recycleBG;
    [SerializeField] private Texture2D recycleActive;
    /// BUTTON RESET COLOR 
    [SerializeField] private Image[] upgradeButtonColorReset;
    public static UpgradeUIManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    // Start is called before the first frame update
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {   
        int counter = 0;
        foreach (UpgradeSettings x in upgradesSettings)
        {
            
            CostTexts[counter].text = x.costs[x.activeLevel].ToString();
            if(x.activeLevel < 9){
                int levelUI = x.activeLevel +1;
                levelTexts[counter].text = levelUI.ToString();}
            else{
                levelTexts[counter].text = "MAX";
            }
           
            for (int i = 0; i < 10; i++)
            {
                if(x.activeLevel >= i)
                    levelProgressIcons[i+counter*10].texture = recycleActive;
                    levelProgressIcons[i+counter*10].color = Color.cyan;
            }
            counter ++;
        }
         ModifierTexts[0].text = PlayerController.Instance.UpgradedMovementSpeed.ToString("F2") + " m/s";
         ModifierTexts[1].text = VariableManager.Instance.Game_collecting_speed.ToString("F2") + " Scrap/s";
         ModifierTexts[2].text = (20 * VariableManager.Instance.Game_tank_capacity_multiplier).ToString("F2") + " m³";
         ModifierTexts[3].text = (10 * VariableManager.Instance.Game_thruster_power).ToString("F2") + "  au";
         ModifierTexts[4].text = (10 * VariableManager.Instance.Game_Terraformer_mission).ToString("F2") + " %";
        
        

    }
    public void UpgradeStoreSetActive(bool value)
    {
        UpgradeStoreUI.SetActive(value);
    }
    
    public void ExitButton()
    {
        PlayerController.Instance.ChangeControlUpgrade(false);
        SoundManager.Instance.PlaySound(SoundManager.Sound.MenuClick, PlayerController.Instance.gameObject.transform.position);
    }
    public void ErrorMessage_Hide()
    {
        
        error_NotEnoughScrap.SetActive(false);
        Error_MaxLevel.SetActive(false);
        foreach (Image x in upgradeButtonColorReset)
        {
            x.color= Color.white;
        }
        SoundManager.Instance.PlaySound(SoundManager.Sound.MenuClick, PlayerController.Instance.gameObject.transform.position);
    }
    public void ErrorMessage_NotEnoughScrap()
    {
        error_NotEnoughScrap.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_failed, PlayerController.Instance.gameObject.transform.position);
        // ROBO ANIMATION
    }
    public void ErrorMessage_ReachedMaxLevel()
    {
        Error_MaxLevel.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_failed, PlayerController.Instance.gameObject.transform.position);
        // ROBO ANIMATION
    }
}
