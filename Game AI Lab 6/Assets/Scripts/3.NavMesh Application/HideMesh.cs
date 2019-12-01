using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMesh : MonoBehaviour {

    void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
