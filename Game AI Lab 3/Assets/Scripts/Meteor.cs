using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    
    Vector3 orientation = Vector3.up;
    public Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
                
        orientation = velocity.normalized;
        UpdateOrientation();

        Vector3 pos = transform.position;        
        pos += velocity * dt;

        transform.position = pos;
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
