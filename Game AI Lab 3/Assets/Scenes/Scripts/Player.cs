using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    Vector3 velocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction.y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction.y = -1;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction.x = -1;
        }

        float dt = Time.deltaTime;
        Vector3 pos = transform.position;
        velocity = direction.normalized * speed;
        pos += velocity * dt;
        transform.position = pos;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
