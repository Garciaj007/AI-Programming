public class ActionFireLaser : Action
{
    DefenseSystem defenseSystem;

    public ActionFireLaser(DefenseSystem ds)
    {
        defenseSystem = ds;
    }

    public override bool Perform()
    {
        if(defenseSystem.GetScannedUnit().type == DefenseSystem.ScannedUnit.ObjectType.INFANTRYMAN)
        {
            defenseSystem.DestroyUnit("by Laser");
            return true;
        }

        return false;
    }
}
