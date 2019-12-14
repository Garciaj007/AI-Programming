using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFireMissile : Action {
    DefenseSystem defenseSystem;
    public ActionFireMissile(DefenseSystem ds)
    {
        defenseSystem = ds;
    }

    public override bool Perform()
    {
        //Debug.Log("Fire Cannon");
        if (defenseSystem.GetScannedUnit().type == DefenseSystem.ScannedUnit.ObjectType.AIRPLANE)
        {
            defenseSystem.DestroyUnit("by Missile");
            return true;
        }

        return false;
    }
}
