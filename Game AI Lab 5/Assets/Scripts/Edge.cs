using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge {
    public float weight;
    public Node node;// connected node 
	

    public Edge(float weight)
    {
        this.weight = weight;
    }
}
