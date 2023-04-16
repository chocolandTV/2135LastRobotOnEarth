using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    // ROCKET HOLDER
    public int GameRocketScraps { get; private set; }
     // PLAYER HOLDER
    public int GamePlayerScraps { get; private set; }

    // LIMITS 
    public int MaxScraps { get; private set; }

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
        if(GamePlayerScraps < MaxScraps)
        {
            Overload();
        }
        return (GamePlayerScraps < MaxScraps);
    }
    ///////////////////////////////// SUBTRACT ///////////////////////
    public void PayResource(int value)
    {
        GameRocketScraps -= value;
        Debug.Log("RM:Payed " + value+ " for Upgrade");
    }
    ////////////////////////////////// ADD ///////////////////////////
    public void AddResourcePlayer(int value)
    {
        GamePlayerScraps+=value;
        Debug.Log("RM: Stored " +  value + " in PlayerStorage");
    }
    public void AddResourceRocket(int value)
    {
        GameRocketScraps+=value;
        Debug.Log("RM: Stored " +  value + " in RocketStorage");
    }
    public void AddStorageCapacity(float percent)
    {
        MaxScraps += (int)(MaxScraps * ( percent/100.0f));
        Debug.Log("RM: new StorageCapacity: " + MaxScraps);
    }
    // EFFECTS
    private void Overload()
    {
        // ANIMATION
        // TEXTURE 
        // UI UPDATE
    }
}
