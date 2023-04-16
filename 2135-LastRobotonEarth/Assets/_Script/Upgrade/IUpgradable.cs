using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable 
{
   void SetValue(float value);
   void SubtractCosts(int costs);
   void SetLevel(int level);
}
