using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {
    public Map map;

    List<Node> visited = new List<Node>();
    List<Node> unvisited = new List<Node>();

    Dictionary<Node, Node> predecessorDict = new Dictionary<Node, Node>();
    Dictionary<Node, float> fDistanceDict = new Dictionary<Node, float>();
    Dictionary<Node, float> gDistanceDict = new Dictionary<Node, float>();

    // Use this for initialization
    void Start()
	{
        Node startNode = map.GetNode(0, 9);
        Node goalNode = map.GetNode(9, 0);
                
        StartCoroutine(Search(startNode, goalNode));
	}
    
    IEnumerator Search(Node start, Node goal)
    {
        // 1. dist[s] = 0
        // 2. set all other distances to infinity
        List<Node> nodes = map.GetNodeList();
        foreach (Node node in nodes)
        {
            fDistanceDict[node] = float.MaxValue;
            gDistanceDict[node] = float.MaxValue;
        }
        fDistanceDict[start] = 0;
        gDistanceDict[start] = 0;

        // 3. Initialize S(visited) and Q(unvisited)
        //    S, the set of visited nodes is initially empty
        //    Q, the queue initially contains all nodes
        // To do: Initialize visited and unvisited
        visited = new List<Node>();
        unvisited = nodes;

        predecessorDict.Clear(); // to generate the result path
		
		while (unvisited.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);
			
            // 4. select element of Q with the minimum distance
            // To do: Get a closest node from the unvisited list
            // Node u = ?
            Node u = GetClosestFromUnvisited();

            // Check if the node u is the goal.
            if (u.IsEqual(goal)) break;
            map.ChangeColor(u, Color.yellow);
            
            // 5. add u to list of S(visited)
            // To do: add u to the visited list
            visited.Add(u);
            
            foreach(Node v in map.GetNeighbors(u))
            {
                if (visited.Contains(v))
                    continue;

                // 6. If new shortest path found then set new value of shortest path
                // To do: update fDistanceDict[v] and gDistanceDict[v]
                // if f_dist[v] > g_dist[u] + w(u,v) + h(v,G) then 
                //      f_dist[v] = g_dist[u] + w(u, v) + h(v, G)
				//		g_dist[v] = g_dist[u] + w(u, v)
				//		update predecessorDict to build the result path
                var pathDistance = gDistanceDict[u] + map.GetDistance(u, v);
                var estimatedDistance = GetEstimatedDistance(v, goal);
                if (fDistanceDict[v] > pathDistance + estimatedDistance)
                {
                    fDistanceDict[v] = pathDistance + estimatedDistance;
                    gDistanceDict[v] = pathDistance;
                }

                predecessorDict[v] = u;
            }
        }

        
        // Display the result path
        Node p = predecessorDict[goal];
        map.ChangeColor(goal, Color.red);
        int count = 200;
        while (p != start)
        {
            map.ChangeColor(p, Color.red);
            p = predecessorDict[p];
            if (count < 0) break;

            count--;
        }
    }
    
    Node GetClosestFromUnvisited()
    {
        float shortest = float.MaxValue;
        Node shortestNode = null;
        foreach (var node in unvisited)
        {
            if (shortest > fDistanceDict[node])
            {
                shortest = fDistanceDict[node];
                shortestNode = node;
            }
        }
        
        unvisited.Remove(shortestNode);
        return shortestNode;
    }

    float GetEstimatedDistance(Node node1, Node node2)
    {
        // To do : Calculate the estimated distance
        // The direct distance can be used as an estimated value
        float dist = Vector2.Distance(new Vector2(node1.x, node1.y), new Vector2(node2.x, node2.y));
        return dist;
    }
}
