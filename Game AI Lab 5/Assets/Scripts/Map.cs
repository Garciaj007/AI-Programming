using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject block;
    public float gap;

    int width;
    int height;

    int[,] map;
    Node[,] refNodeMap;
    GameObject[,] refImageMap;

    List<Node> nodes = new List<Node>();


	// Use this for initialization
	void Start () {
        
        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        
        width = map.GetLength(0);
        height = map.GetLength(1);
        refImageMap = new GameObject[height, width];
        DrawInitMap();
        


        refNodeMap = new Node[height, width];
        SetupNodesAndEdges();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void SetupNodesAndEdges()
    {
        nodes.Clear();
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[y, x] != 1)
                {
                    Node n = new Node(x, y);
                    nodes.Add(n);
                    refNodeMap[y, x] = n;
                }
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[y, x] != 1)
                {
                    Node curNode = GetNode(x, y);
                    if (x+1 < width && map[y, x+1] != 1)
                    {
                        Edge e = new Edge(1);
                        e.node = refNodeMap[y, x + 1];
                        
                        curNode.AddEdge(e);
                    }
                    if (x-1 >= 0 && map[y, x-1] != 1)
                    {
                        Edge e = new Edge(1);
                        e.node = refNodeMap[y, x-1];
                        curNode.AddEdge(e);
                    }
                    if (y+1 < height && map[y+1, x] != 1)
                    {
                        Edge e = new Edge(1);
                        e.node = refNodeMap[y+1, x];
                        curNode.AddEdge(e);
                    }
                    if (y-1 >= 0 && map[y-1, x] != 1)
                    {
                        Edge e = new Edge(1);
                        e.node = refNodeMap[y-1, x];
                        curNode.AddEdge(e);
                    }
                }
            }
        }
    }

    void DrawInitMap()
    {
        Transform t = transform;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject go = Instantiate(block, t);
                go.transform.position = t.position + new Vector3(x * gap, - y * gap, 0);
                refImageMap[y, x] = go;
                if (map[y, x] == 1)
                {
                    go.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }

    public List<Node> GetNodeList()
    {
        return nodes;
    }

    public Node GetNode(int x, int y)
    {
        return nodes.Find(delegate (Node n) { return (n.x == x) && (n.y == y); });
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        foreach(Edge e in node.GetEdges())
        {
            neighbors.Add(e.node);
        }
        return neighbors;
    }

    public float GetDistance(Node n1, Node n2)
    {
        float distance = 0;
        foreach (Edge e in n1.GetEdges())
        {
            if (e.node == n2)
            {
                distance = e.weight;
            }
        }
        return distance;
    }

    public void ChangeColor(Node n, Color c)
    {
        refImageMap[n.y, n.x].GetComponent<SpriteRenderer>().color = c;
    }
}
