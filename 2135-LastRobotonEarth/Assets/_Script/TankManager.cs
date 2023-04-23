using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TankManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tankIcons;
    [SerializeField] private TextMeshProUGUI tankTexts;
    [SerializeField] private Animator animator;
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
    private void Start() {
        tankIcons[0].SetActive(true);
        lastTankIcon = tankIcons[0];
        tankTexts.text = "0 %";
    }
    private IEnumerator AnimateFullTank(float sec)
    {
        animator.enabled= true;
        yield return new WaitForSeconds (sec);
        animator.enabled= false;
        
    }
    ///////////////  UI CHANGING //////////////////
    public void OnChangeValue(int Value)
    { // PERCENT  0- 100 % 
        tankTexts.text = "" + Value + " %";
        if(Value == 0)
        {
            lastTankIcon.SetActive(false);
            tankIcons[0].SetActive(true);
            lastTankIcon = tankIcons[0];
            

            
        }else if ( Value > 0 && Value <= 20)
        {
            lastTankIcon.SetActive(false);
            tankIcons[1].SetActive(true);
            lastTankIcon = tankIcons[1];
            HUDManager.Instance.PlayerDefaultMission();
            
        }
        else if ( Value > 20 && Value <= 40)
        {
            lastTankIcon.SetActive(false);
            tankIcons[2].SetActive(true);
            lastTankIcon = tankIcons[2];

            
        }
        else if ( Value > 40 && Value <= 60)
        {
            lastTankIcon.SetActive(false);
            tankIcons[3].SetActive(true);
            lastTankIcon = tankIcons[3];

            
        }
        else if ( Value > 60 && Value <= 80)
        {
            lastTankIcon.SetActive(false);
            tankIcons[4].SetActive(true);
            lastTankIcon = tankIcons[4];

            
        }
        else if ( Value >= 100)
        {
            lastTankIcon.SetActive(false);
            tankIcons[5].SetActive(true);
            lastTankIcon = tankIcons[5];
            HUDManager.Instance.PlayerTankFullMission();
            StartCoroutine( AnimateFullTank(3) );
            
        }
    }
}
