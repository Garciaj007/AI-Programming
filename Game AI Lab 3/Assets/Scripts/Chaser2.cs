using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser2 : MonoBehaviour {
    public GameObject target;
    public float speed;
    public float maxAngularSpeed;

    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        // To do: Complete Update() to chase the targe with the angular speed limit (maxAngularSpeed)
        // * recommended steps: 
        // 1. implement the chasing that you did in Lab3.1
        // 2. limit the rotation (orientation). The rotation angle cannot be over the max angle based on maxAngularSpeed.
        orientation = (target.transform.position - transform.position).normalized;
        transform.position += orientation * speed * Time.deltaTime;

        var maxAngle = maxAngularSpeed * Time.deltaTime; 
        var desiredAngle = -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg;
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
