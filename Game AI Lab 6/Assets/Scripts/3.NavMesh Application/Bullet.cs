using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20;
    public float lifeTime = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        
        transform.Translate(0, 0, speed * dt);

        lifeTime -= dt;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Debug.Log(other.tag);
        if (other.tag == "Alien")
        {

            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
