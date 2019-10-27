using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacencyMatrix : MonoBehaviour
{
    public GameObject[] nodes;
    int[,] adjacencyMatrix;    

    // Use this for initialization
    void Start () {
		adjacencyMatrix = new int[,] {
               //A, B, C, D, E
        /* A */{ 0, 1, 0, 0, 1 },
        /* B */{ 1, 0, 0, 0, 1 },
        /* C */{ 0, 0, 0, 0, 1 },
        /* D */{ 0, 0, 0, 0, 1 },
        /* E */{ 1, 1, 1, 1, 0 }
        };
    }
	
	// Update is called once per frame
	void Update () {        
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            foreach(int connectedNode in GetConnectedNodes(i))
            {
                Debug.DrawLine(nodes[i].transform.position, nodes[connectedNode].transform.position);
            }
        }
	}

    List<int> GetConnectedNodes(int fromNode)
    {
        List<int> connectedNodes = new List<int>();
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            if (adjacencyMatrix[i, fromNode] == 1)
            {
                connectedNodes.Add(i);
            }
        }
        return connectedNodes;
    }
}
