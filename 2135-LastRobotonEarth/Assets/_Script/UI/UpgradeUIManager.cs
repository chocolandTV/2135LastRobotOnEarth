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
    [SerializeField] private RawImage[] levelProgressIcons;
    [SerializeField] private Texture2D recycleBG;
    [SerializeField] private Texture2D recycleActive;
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
    {   int counter = 0;
        foreach (UpgradeSettings x in upgradesSettings)
        {
            CostTexts[counter].text = x.costs[x.activeLevel].ToString();
            levelTexts[counter].text = x.activeLevel.ToString();
            for (int i = 0; i < 10; i++)
            {
                if(x.activeLevel >= i+1)
                    levelProgressIcons[i*counter].texture = recycleActive;
            }
            counter ++;
        }
        
        
        
        // foreach Level levelProgressIcons  color 00ffff and recycleBG / recycleActive

    }
    public void UpgradeStoreSetActive(bool value)
    {
        UpgradeStoreUI.SetActive(value);
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // ENABLE MOUSE
            // DISABLE MOVEMENT & LOOK

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // ENABLE MOVEMENT & LOOK
        }
    }
    public void ExitButton()
    {
        UpgradeStoreSetActive(false);
    }
    public void ErrorMessage_Hide()
    {
        error_NotEnoughScrap.SetActive(false);
        Error_MaxLevel.SetActive(false);
    }
    public void ErrorMessage_NotEnoughScrap()
    {
        error_NotEnoughScrap.SetActive(true);
        // SOUND PLAY
        // ROBO ANIMATION
    }
    public void ErrorMessage_ReachedMaxLevel()
    {
        Error_MaxLevel.SetActive(true);
        // SOUND PLAY
        // ROBO ANIMATION
    }
}
