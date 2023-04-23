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
     private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    private void Start() {
        Game_collecting_speed = 1;
        Game_movement_multiplier =1;
        Game_tank_capacity_multiplier = 1;
        Game_Terraformer_mission = 1;
        Game_thruster_power=0;
    }
    public void SetMovementMultiplier(float value)
    {
        Game_movement_multiplier = value;
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
    }
    public void SetCollectingSpeed(float value)
    {
        Game_collecting_speed = value;
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
    }
    public void SetThrusterPower(float value)
    {
        Game_thruster_power = value;
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
    }
    public void SetTankCapacity(float value)
    {
        Game_tank_capacity_multiplier = value;
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
    }
    public void SetTerraformerMissionProgress(float value)
    {
        Game_Terraformer_mission = value;
        terraformer.transform.localScale = Vector3.one*value;
        SoundManager.Instance.PlaySound(SoundManager.Sound.upgrade_complete, PlayerController.Instance.gameObject.transform.position);
        // SCALE TERRAFORMER 
        // IF 50% MESSAGE HUD  GOOD JOB KEEP COLLECTING
        // IF 100% MESSAGE HUD FINISHED WIN AND CUTsCENE 

    }
   
}
