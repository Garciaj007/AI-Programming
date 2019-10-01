using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {
    public float gap = 0.5f;
    Vector3 origin = Vector3.zero;

    protected Vector3 positionOnGrid;
    
	// Use this for initialization
	protected void Start () {
        
        positionOnGrid = new Vector3(((int)transform.position.x) / gap, ((int)transform.position.y) / gap, 0);
	}
	
	// Update is called once per frame
	protected void Update () {
        Vector3 pos = origin + positionOnGrid * gap;

        transform.position = pos;	
	}

    public Vector3 GetPositionOnGrid()
    {
        return positionOnGrid;
    }
}
