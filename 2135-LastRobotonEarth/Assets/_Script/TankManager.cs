using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TankManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tankIcons;
    [SerializeField] private TextMeshProUGUI tankTexts;
    private GameObject lastTankIcon;
    public static TankManager Instance;
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    ///////////////  UI CHANGING //////////////////
    public void OnChangeValue(int Value)
    {
        if(Value == 0)
        {
            lastTankIcon.SetActive(false);
            tankIcons[0].SetActive(true);
            lastTankIcon = tankIcons[0];

            tankTexts.text = "" + Value + " %";
        }else if ( Value < 20)
        {
            lastTankIcon.SetActive(false);
            tankIcons[1].SetActive(true);
            lastTankIcon = tankIcons[1];

            tankTexts.text = "" + Value + " %";
        }
        else if ( Value < 40)
        {
            lastTankIcon.SetActive(false);
            tankIcons[2].SetActive(true);
            lastTankIcon = tankIcons[2];

            tankTexts.text = "" + Value + " %";
        }
        else if ( Value < 60)
        {
            lastTankIcon.SetActive(false);
            tankIcons[3].SetActive(true);
            lastTankIcon = tankIcons[3];

            tankTexts.text = "" + Value + " %";
        }
        else if ( Value < 80)
        {
            lastTankIcon.SetActive(false);
            tankIcons[4].SetActive(true);
            lastTankIcon = tankIcons[4];

            tankTexts.text = "" + Value + " %";
        }
        else if ( Value >= 100)
        {
            lastTankIcon.SetActive(false);
            tankIcons[5].SetActive(true);
            lastTankIcon = tankIcons[5];

            tankTexts.text = "100 %";
        }
    }
}
