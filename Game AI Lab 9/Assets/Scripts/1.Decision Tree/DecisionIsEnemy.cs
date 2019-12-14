using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionIsEnemy : Decision {
    DefenseSystem defenseSystem;
    public DecisionIsEnemy(DefenseSystem ds)
    {
        defenseSystem = ds;
    }

    public override bool Test()
    {
        return defenseSystem.GetScannedUnit().isEnemy;        
    }
}
