using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : GridMovement
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
            if (pos.x > playerPos.x)
            {
                // To do: update movement.x properly
                movement.x--;
            }
            else if (pos.x < playerPos.x)
            {
                // To do: update movement.x properly     
                movement.x++;
            }

            if (pos.y > playerPos.y)
            {
                // To do: update movement.y properly
                movement.y--;
            }
            else if (pos.y < playerPos.y)
            {
                // To do: update movement.y properly
                movement.y++;
            }
        }

        
        positionOnGrid += movement;
        base.Update();

        if (movement != Vector3.zero)
        {
            timer = pauseTime;
        }
	}
}
