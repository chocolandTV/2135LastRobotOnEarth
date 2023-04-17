using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    
    public static VariableManager Instance;
    [SerializeField] private GameObject terraformer;
    [field: SerializeField] public float Game_movement_multiplier { get; private set; }
    [field: SerializeField] public float Game_collecting_speed { get; private set; }
    [field: SerializeField] public float Game_thruster_power { get; private set; }
    [field: SerializeField] public float Game_tank_capacity_multiplier { get; private set; }
    [field: SerializeField] public float Game_Terraformer_mission { get; private set; }
    
    public void SetMovementMultiplier(float value)
    {
        Game_movement_multiplier = value;
    }
    public void SetCollectingSpeed(float value)
    {
        Game_collecting_speed = value;
    }
    public void SetThrusterPower(float value)
    {
        Game_thruster_power = value;
    }
    public void SetTankCapacity(float value)
    {
        Game_tank_capacity_multiplier = value;
    }
    public void SetTerraformerMissionProgress(float value)
    {
        Game_Terraformer_mission = value;
        terraformer.transform.localScale *= value;
        // SCALE TERRAFORMER 
        // IF 50% MESSAGE HUD  GOOD JOB KEEP COLLECTING
        // IF 100% MESSAGE HUD FINISHED WIN AND CUTsCENE 

    }
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
}
