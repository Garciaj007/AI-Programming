using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercepter : MonoBehaviour {
    public GameObject target;
    public float speed;
    public float maxAngularSpeed;
    Vector3 velocity = Vector3.zero;
    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Start () {
                
    }
	
	// Update is called once per frame
	void Update () {
        // To do: Complete Update() to intercept the target
        // * recommended steps: 
        // 1. implement the chasing that you did in Lab3.1
        // 2. (Optional) implement the rotation limit that you did in Lab3.3
        // 3. calculate the predicted moving position and chase the predicted position instead of the actual position of the target.
        
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
