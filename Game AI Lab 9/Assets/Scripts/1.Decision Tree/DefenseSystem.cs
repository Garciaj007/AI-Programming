using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSystem : MonoBehaviour
{
    public class ScannedUnit
    {
        public enum ObjectType { AIRPLANE, VEHICLE, INFANTRYMAN }
        public ObjectType type;
        public bool isEnemy;
        public float distance;
        public float speed;

        public ScannedUnit()
        {
            type = GetRandomEnum<ScannedUnit.ObjectType>();
            isEnemy = Random.Range(0f, 1f) > 0.3f ? true : false;
            distance = Random.Range(100f, 200f);
            speed = Random.Range(30f, 50f);
        }

        public void Update()
        {
            distance -= speed * Time.deltaTime;
        }

        public string GetStringInfo()
        {
            string info = "Unknown";
            switch(type)
            {
                case ObjectType.AIRPLANE:
                    info = "Airplane";
                    break;
                case ObjectType.VEHICLE:
                    info = "Vehicle";
                    break;
                case ObjectType.INFANTRYMAN:
                    info = "Infantryman";
                    break;
            }
            info += isEnemy ? " (Enemy)" : " (Ally)";
            info += "\nDistance = " + distance;
            info += "\nSpeed = " + speed; 
            return info;
        }
    }

    string actionDisplay = "";
    ScannedUnit scannedUnit = null;
    
    public void SetActionDisplay(string action)
    {
        actionDisplay = action;
    }
    public ScannedUnit GetScannedUnit()
    {
        return scannedUnit;
    }
    public void DestroyUnit(string weapon)
    {
        actionDisplay = "The unit has been destroyed " + weapon;
    }

    IEnumerator Start()
    {
        // Set up the decision tree
        Decision isEnemy = new DecisionIsEnemy(this);
        Decision isAirplane = new DecisionIsAirplane(this);        
        Decision isDistanceLessThan = new DecisionIsDistanceLessThan(this, 30);
        // To do : Create an isVehicle object 
        DecisionIsVehicle isVehicle = new DecisionIsVehicle(this);
        Action fireMissile = new ActionFireMissile(this);
        // To do : Create a laser-firing-action object.
        Action fireLaser = new ActionFireLaser(this);
        // To do : Create a cannon-firing-action object.
        Action fireCannon = new ActionFireCannon(this);

        Decision decisionTree = isEnemy;
        isEnemy.trueNode = isDistanceLessThan;
        isEnemy.falseNode = null;

        isDistanceLessThan.trueNode = isAirplane;
        isDistanceLessThan.falseNode = null;

        isAirplane.trueNode = fireMissile;

        // To do : Connect the falseNode of isAirplane to isVehicle
        isAirplane.falseNode = isVehicle;

        // To do : Set the child nodes with proper actions
        isVehicle.trueNode = fireCannon;
        isVehicle.falseNode = fireLaser;

        yield return null;

        while (true) // main loop
        {
            DoRadarScan(); // Scan a new unit or update the scanned unit

            // Get an action from the decision tree
            Action action = decisionTree.MakeDecision() as Action;
            if (action != null)
            {
                if (action.Perform())// action performed
                {
                    yield return new WaitForSeconds(3); // wait for 3 sec
                    scannedUnit = null;
                }
            }
            
            if (scannedUnit != null && scannedUnit.distance < 0)
            {
                
                actionDisplay = "No action";
                yield return new WaitForSeconds(3); // wait for 3 sec
                scannedUnit = null;
            }
                        
            yield return null;
        }
    }

    void DoRadarScan()
    {
        if (scannedUnit == null)
        {
            actionDisplay = "";
            scannedUnit = new ScannedUnit();
        }
        
        scannedUnit.Update(); // Update its distance        
    }

    void OnGUI()
    {
        // Radar Display
        string radarDisplay = "No object found in radar";
        if (scannedUnit != null)
        {
            radarDisplay = scannedUnit.GetStringInfo();
            GUI.color = scannedUnit.isEnemy ? Color.red : Color.green;
        }
        GUI.Box(new Rect(10, 10, 400, 200), "Radar");
        GUI.TextField(new Rect(50, 50, 300, 100), radarDisplay);
        
        // Defense System Display        
        GUI.Box(new Rect(10, 220, 400, 100), "Defense System");
        GUI.TextField(new Rect(50, 250, 300, 30), actionDisplay);
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}