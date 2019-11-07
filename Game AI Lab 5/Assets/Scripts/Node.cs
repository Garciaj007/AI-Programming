using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    //string name;
    //Vector3 location;
    public int x;
    public int y;

    List<Edge> edges = new List<Edge>();

	public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public List<Edge> GetEdges()
    {
        return edges;
    }

    public void AddEdge(Edge e)
    {
        edges.Add(e);
    }

    public string GetStringInfo()
    {
        return "Node[" + x.ToString() + y.ToString() + "]";
    }

    public bool IsEqual(Node n)
    {
        return x == n.x && y == n.y;
    }
}
