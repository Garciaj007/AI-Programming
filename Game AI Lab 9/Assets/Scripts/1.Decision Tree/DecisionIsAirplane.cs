using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionIsAirplane : Decision {
    DefenseSystem defenseSystem;

    public DecisionIsAirplane(DefenseSystem ds)
    {
        defenseSystem = ds;
    }
    public override bool Test()
    {
        return defenseSystem.GetScannedUnit().type == DefenseSystem.ScannedUnit.ObjectType.AIRPLANE;
    }
}
