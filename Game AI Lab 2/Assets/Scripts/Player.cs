using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridMovement
{
    public bool enableControl = true;
    float frameTime = 0.2f;

    float frameTimeCounter;
	// Use this for initialization
	void Start () {
        frameTimeCounter = 0;
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        frameTimeCounter -= dt;
        if (frameTimeCounter > 0 || !enableControl)
        {
            return;
        }
        
        Vector3 movement = Vector3.zero;        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y++;            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y--;            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x++;
        }

        if (movement != Vector3.zero)
        {
            frameTimeCounter = frameTime;
        }
        
        positionOnGrid += movement;
        base.Update();        
    }
}
