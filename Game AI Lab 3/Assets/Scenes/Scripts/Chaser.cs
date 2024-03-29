﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {
    public GameObject target;
    public float speed;

    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        // To do: Get the direction(dir) towards the target. The direction should be a unit vector.
        Vector3 dir = (target.transform.position - transform.position).normalized;
        

        // Update orientation 
        orientation = dir;
        UpdateOrientation();
                
        Vector3 pos = transform.position;        
        Vector3 velocity = orientation * speed;
        // To do: Update the current position (pos) using S_final = S_initial + v * t
        pos += velocity * Time.deltaTime;

        transform.position = pos;
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
