using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
	public class AStar2 {
		public Tilemap map;

		List<Node> visited = new List<Node>();
		List<Node> unvisited = new List<Node>();

		Dictionary<Node, Node> predecessorDict = new Dictionary<Node, Node>();
		Dictionary<Node, float> fDistanceDict = new Dictionary<Node, float>();
		Dictionary<Node, float> gDistanceDict = new Dictionary<Node, float>();

		public void Init(Tilemap tileMap)
		{
			map = tileMap;
		}
		
		public List<Node> Search(Node start, Node goal)
		{
			// 1. dist[s] = 0
			// 2. set all other distances to infinity
            List<Node> nodes = map.GetAllNodes();
            foreach (Node node in nodes)
			{
                fDistanceDict[node] = float.MaxValue;
                gDistanceDict[node] = float.MaxValue;
			}
            fDistanceDict[start] = 0;
            gDistanceDict[start] = 0;

			// 3. Initialize S(visited) and Q(unvisited)
			//    S, the set of visited nodes is initially empty
			//    Q, the queue initially conatains all nodes
        	// To do: Initialize visited and unvisited
        
			
			predecessorDict.Clear(); // to generate the result path
			
			while (unvisited.Count > 0)
			{
				// 4. select element of Q with the minimum distance
            	// To do: Get a closest node from the unvisited list
            	// Node u = ?
            	Node u = null;

				// Check if the node u is the goal.            
				if (u == goal) break;
							
				// 5. add u to list of S(visited)            
            	// To do: add u to the visited list
				
				foreach(Node v in map.GetNeighbors(u))
				{
					if (visited.Contains(v))
						continue;

					// 6. If new shortest path found then set new value of shortest path                
	                // To do: update fDistanceDict[v], gDistanceDict[v] and predecessorDict[v]
	                
				}
			}

			List<Node> path = new List<Node>();

			// Generate the shortest path
			path.Add(goal);
			Node p = predecessorDict[goal];
			
			while (p != start)
			{
				path.Add(p);            
				p = predecessorDict[p];        
			}

			path.Reverse();
			return path;
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
	}
}