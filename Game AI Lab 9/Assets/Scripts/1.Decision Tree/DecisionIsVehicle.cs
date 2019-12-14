public class DecisionIsVehicle : Decision
{
    DefenseSystem defenseSystem;

    public DecisionIsVehicle(DefenseSystem ds)
    {
        defenseSystem = ds;
    }

    public override bool Test()
    {
        return defenseSystem.GetScannedUnit().type == DefenseSystem.ScannedUnit.ObjectType.VEHICLE;
    }
}
