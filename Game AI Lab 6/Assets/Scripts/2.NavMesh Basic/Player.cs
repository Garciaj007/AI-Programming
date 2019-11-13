using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// To do: add namespace(UnityEngine.AI) to access NavMeshAgent
using UnityEngine.AI;

public class Player : MonoBehaviour {
    NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        // To do: Get the NavMeshAgent component which is attached to this game object and assign it to navMeshAgent
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // To do : Set the destination with the clicked position(hit.point) 
                
            }
        }
	}
}
