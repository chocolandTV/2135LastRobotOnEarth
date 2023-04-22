using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance{get;set;}
    // ROCKET HOLDER
    public int GameRocketScraps { get; private set; }
     // PLAYER HOLDER
    public int GamePlayerScraps { get; private set; }

    // LIMITS 
    private int MaxScraps;

    public int SuperMaxScraps  =>  MaxScraps * (int)VariableManager.Instance.Game_tank_capacity_multiplier;
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    
    void Start()
    {
        GamePlayerScraps = 0;
        GameRocketScraps = 0;
        MaxScraps = 20 ;// LATER SCRIPTABLE OBJECT DEFINE * MULTIPLIER
    }
    ///////////////////// CHECKS /////////////////////////
    public bool canPurchase(int value)
    {
        return (GameRocketScraps - value >= 0);
    }
    public bool isSpaceInStorage()
    {
        if(GamePlayerScraps < SuperMaxScraps)
        {
            Overload();
        }
        return (GamePlayerScraps < SuperMaxScraps);
    }
    ///////////////////////////////// SUBTRACT ///////////////////////
    public void RemoveRocketResource(int value)
    {
        GameRocketScraps -= value;
        Debug.Log("RM:Payed " + value+ " for Upgrade");
        TankManager.Instance.OnChangeValue( (int) Mathf.Round((float)GamePlayerScraps/ (float)SuperMaxScraps *100f));
    }
    public void RemovePlayerResource()
    {
        // Debug.Log("RM:Player spend " + GamePlayerScraps+ " to the Rocket");
        GamePlayerScraps = 0;
        TankManager.Instance.OnChangeValue( (int) Mathf.Round((float)GamePlayerScraps/ (float)SuperMaxScraps *100f));
    }
    ////////////////////////////////// ADD ///////////////////////////
    public void AddResourcePlayer(int value)
    {
        GamePlayerScraps+=value;
        
        // Debug.Log("RM: Stored " +  value + " in PlayerStorage");
        TankManager.Instance.OnChangeValue((int) Mathf.Round((float)GamePlayerScraps/ (float)SuperMaxScraps *100f));
    }
    public void AddResourceRocket(int value)
    {
        GameRocketScraps+=value;
        Debug.Log("RM: Stored " +  value + " in RocketStorage");
    }
    
    // EFFECTS
    private void Overload()
    {
        // ANIMATION
        // TEXTURE 
        // UI UPDATE
    }
}
