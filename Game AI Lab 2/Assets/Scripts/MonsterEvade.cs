using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvade : GridMovement
{
    public GameObject player;
    public float reactionRadius = 5;

    float timer;
    float pauseTime = 1;

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        timer -= dt;
        if (timer > 0)
        {
            return;
        }
                
        Vector3 playerPos = player.GetComponent<Player>().GetPositionOnGrid();
        Vector3 pos = GetPositionOnGrid();
        Vector3 movement = Vector3.zero;
        float d = Vector3.Distance(pos, playerPos);
        
        if (d < reactionRadius)
        {
            // To do: complete the evading algorithm by updating the "movement" variable properly
            if (pos.x > playerPos.x)
                movement.x++;
            else if (pos.x < playerPos.x) 
                movement.x--;

            if (pos.y > playerPos.y)
                movement.y++;
            else if (pos.y < playerPos.y)
                movement.y--;

        }
                
        positionOnGrid += movement;
        base.Update();

        if (movement != Vector3.zero)
        {
            timer = pauseTime;
        }
	}
}
