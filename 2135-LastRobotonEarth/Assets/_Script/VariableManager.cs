using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    
    public static VariableManager Instance;
    [field: SerializeField] public float Game_movement_multiplier { get; private set; }
    [field: SerializeField] public float Game_collecting_speed { get; private set; }
    [field: SerializeField] public float Game_thruster_power { get; private set; }
    [field: SerializeField] public float Game_tank_capacity_multiplier { get; private set; }
    [field: SerializeField] public float Game_robot_hull_multiplier { get; private set; }
    
    public void AddMovementMultiplier(float value)
    {
        Game_movement_multiplier += value;
    }
    public void AddCollectingSpeed(float value)
    {
        Game_collecting_speed += value;
    }
    public void AddThrusterPower(float value)
    {
        Game_thruster_power += value;
    }
    public void AddTankCapacity(float value)
    {
        Game_tank_capacity_multiplier += value;
    }
    public void AddRobotHull(float value)
    {
        Game_robot_hull_multiplier += value;
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
