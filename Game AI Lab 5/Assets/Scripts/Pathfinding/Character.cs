using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
	public class Character : MonoBehaviour {
		public Tilemap map;
		AStar2 aStarPathfinder = new AStar2();
		public enum State { MOVE, IDLE };
		State state;

		// Use this for initialization
		void Start () {
			state = State.IDLE;
			aStarPathfinder.Init(map);	    	
		}
		
		// Update is called once per frame
		void Update () {
			// handle mouse input
			if (state == State.IDLE && Input.GetMouseButtonDown(0))
			{
				state = State.MOVE;
				Vector3 pos = Input.mousePosition;
				pos = Camera.main.ScreenToWorldPoint(pos);
				
				Node targetNode = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

				if (targetNode.tileType == Node.TileType.GRASS)
				{
					StartCoroutine(SearchPathAndMove(targetNode));
				}
				else
				{
					state = State.IDLE;
					Debug.Log("WALL - can't move to here");
				}
				
			}
		}

		IEnumerator SearchPathAndMove(Node target)
		{
			// To do: Find out a start node. The start node is the node where the character stands
            Node start = map.GetNode(Mathf.FloorToInt(transform.position.x + 0.5f), Mathf.FloorToInt(transform.position.y + 0.5f));

            // To do: Get the shortest path between start and target using astar algorithm
            List<Node> path = aStarPathfinder.Search(start, target);

			// To do: move the character using the position info in the shortest path
			// you need to use "yield return new WaitForSeconds(0.5f);" to make a delay between each movement
			// Refer to https://docs.unity3d.com/Manual/Coroutines.html
            foreach (var pos in path)
            {
                transform.position = new Vector3(pos.x, pos.y);
                yield return new WaitForSeconds(0.5f);
            }

            // set state to IDLE in order to enable next movement
			state = State.IDLE;
        }
	}
}