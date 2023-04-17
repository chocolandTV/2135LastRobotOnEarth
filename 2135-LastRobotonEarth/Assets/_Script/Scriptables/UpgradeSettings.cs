using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UpgradeSetting", menuName ="ScriptableObjects/UpgradeSettingsObject", order = 2)]
public class UpgradeSettings : ScriptableObject
{
    [field:SerializeField] public int[]costs {get; set;}
    [field:SerializeField]public float[]multiplier {get;set;}
    [field:SerializeField]public int activeLevel {get;set;}

}
