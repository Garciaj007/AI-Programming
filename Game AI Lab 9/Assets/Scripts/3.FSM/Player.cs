using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {    
    public int speed = 5;

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y);
        Vector3 velocity = dir.normalized * speed;
        transform.Translate(velocity * Time.deltaTime);
    }
}
