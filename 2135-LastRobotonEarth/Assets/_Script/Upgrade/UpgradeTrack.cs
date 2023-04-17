using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTrack : MonoBehaviour
{

    // UPGRADE 01  Tracks IMPROVE MOVEMENT SPEED
    // SCRIPTABLEOBJECT  COSTS AND MULTIPLIER BALANCER
    private int cost;
    private int[] multiplier  = new int[10];
    // END SCRIPTABLEOBJECTS
    // Start is called before the first frame update
    private string upgradeName = "Upgrade Tracks";
    public int level = 0;
    private int maxlevel = 10;

    void Start()
    {
        
    }
    public void OnButtonClickUpgrade()
    {
        Debug.Log(upgradeName);
        if(level <= maxlevel)
        {
            if(ResourceManager.Instance.canPurchase(cost))
            {
                ResourceManager.Instance.RemoveRocketResource(cost);
                level++;
                Upgrade();
            }
            else{
                // NOT ENOUGH SCRAP MESSAGE
            }

        }
        else{
            // LEVEL MAX MESSAGE
        }
    }
    private void Upgrade()
    {
        VariableManager.Instance.SetMovementMultiplier(multiplier[level]);
        // SOUND 
        // ROBOT ANIMATION HAPPY 
    }
    

}
