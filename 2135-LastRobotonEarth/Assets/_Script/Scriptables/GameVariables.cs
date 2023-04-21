using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameVariable", menuName = "ScriptableObjects/GameVariablesObject", order = 1)]
public class GameVariables : ScriptableObject
{
   
   [field:SerializeField] public float SoundVolume {get; private set;}
   [field:SerializeField] public float MusicVolume {get; private set;}
   [field:SerializeField] public float MouseSensitivity {get; private set;}
   // HEADER SPAWNING 
   
   [field:SerializeField] public Vector2Int SpawningQuantity { get; private set;}
   [field:SerializeField] public int SpawningMaxObjects { get; private set;}
}
