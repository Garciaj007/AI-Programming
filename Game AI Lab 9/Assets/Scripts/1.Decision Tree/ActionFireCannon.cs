
public class ActionFireCannon : Action
{
    DefenseSystem defenseSystem;

    public ActionFireCannon(DefenseSystem ds)
    {
        defenseSystem = ds;
    }

    public override bool Perform()
    {
        if(defenseSystem.GetScannedUnit().type == DefenseSystem.ScannedUnit.ObjectType.VEHICLE)
        {
            defenseSystem.DestroyUnit("by Cannon");
            return true;
        }

        return false;
    }
}
