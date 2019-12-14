using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionIsDistanceLessThan : Decision {
    DefenseSystem defenseSystem;
    float radius;

    public DecisionIsDistanceLessThan(DefenseSystem ds, float r)
    {
        defenseSystem = ds;
        radius = r;
    }

    public override bool Test()
    {
        return defenseSystem.GetScannedUnit().distance < radius;
    }
}
