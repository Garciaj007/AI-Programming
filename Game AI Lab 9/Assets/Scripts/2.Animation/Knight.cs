using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {
    public float speed;

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, y, 0);
        Vector3 velocity = dir.normalized * speed;
                
        animator.SetFloat("Vertical Speed", velocity.y);
        animator.SetFloat("Horizontal Speed", velocity.x);
        transform.Translate(velocity * Time.deltaTime);
        
	}
}
