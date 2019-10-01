using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BresenhamMovement : GridMovement
{
    public GameObject player;
    public float reactionRadius = 5;

    float timer;
    float pauseTime = 1;

    Vector3[] path = null;
    int stepCounter = 0;

    // Use this for initialization
    new void Start()
    {
        base.Start();
                
        // To do: enable the following line to get the Bresenham path
        path = GetBresenhamPath(GetPositionOnGrid(), player.GetComponent<Player>().GetPositionOnGrid());        
    }

    // Update is called once per frame
    new void Update()
    {
        float dt = Time.deltaTime;
        timer -= dt;
        if (timer > 0)
        {
            return;
        }
                
        if (path != null && stepCounter >= path.Length)
        {
            return;
        }

        // To do: Update the "positionOnGrid" variable with the position information in the "path" variable in order
        positionOnGrid = path[stepCounter++];

        // To do: Disable the following code block
        //---------------------------------------------------------------------
        //Vector3 playerPos = player.GetComponent<Player>().GetPositionOnGrid();
        //Vector3 pos = GetPositionOnGrid();
        //Vector3 movement = Vector3.zero;
        //float d = Vector3.Distance(pos, playerPos);
        
        //if (d < reactionRadius)
        //{
        //    if (pos.x > playerPos.x)
        //    {
        //        movement.x--;
        //    }
        //    else if (pos.x < playerPos.x)
        //    {
        //        movement.x++;
        //    }

        //    if (pos.y > playerPos.y)
        //    {
        //        movement.y--;
        //    }
        //    else if (pos.y < playerPos.y)
        //    {
        //        movement.y++;
        //    }
        //}
        //positionOnGrid += movement;
        //base.Update();        

        //if (movement != Vector3.zero)
        //{
        //    timer = pauseTime;
        //}        //Vector3 playerPos = player.GetComponent<Player>().GetPositionOnGrid();
        //Vector3 pos = GetPositionOnGrid();
        //Vector3 movement = Vector3.zero;
        //float d = Vector3.Distance(pos, playerPos);
        
        //if (d < reactionRadius)
        //{
        //    if (pos.x > playerPos.x)
        //    {
        //        movement.x--;
        //    }
        //    else if (pos.x < playerPos.x)
        //    {
        //        movement.x++;
        //    }

        //    if (pos.y > playerPos.y)
        //    {
        //        movement.y--;
        //    }
        //    else if (pos.y < playerPos.y)
        //    {
        //        movement.y++;
        //    }
        //}
        //positionOnGrid += movement;
        //base.Update();        

        //if (movement != Vector3.zero)
        //{
        //    timer = pauseTime;
        //}
        //-----------------------------------------------------------------------


        // To do: Enable the following two lines
        base.Update();
        timer = pauseTime;
    }

    Vector3[] GetBresenhamPath(Vector3 start, Vector3 end)
    {   
        int row = (int)start.y;
        int col = (int)start.x;
        int nextRow = (int)start.y;
        int nextCol = (int)start.x;
        int endRow = (int)end.y;
        int endCol = (int)end.x;
        

        int deltaRow = endRow - row;
        int deltaCol = endCol - col;
        int stepCol, stepRow;
        int currentStep = 0;
        int fraction;

        int pathLength = deltaRow > deltaCol ? deltaRow : deltaCol;
        Vector3[] path = new Vector3[pathLength+1];

        if (deltaRow < 0) stepRow = -1; else stepRow = 1;
        if (deltaCol < 0) stepCol = -1; else stepCol = 1;
        deltaRow = Mathf.Abs(deltaRow * 2);
        deltaCol = Mathf.Abs(deltaCol * 2);
                
        path[currentStep] = new Vector3(nextCol, nextRow, 0);
        currentStep++;

        if (deltaCol > deltaRow)
        {
            fraction = deltaRow * 2 - deltaCol;
            while (nextCol != endCol)
            {
                if (fraction >= 0)
                {
                    nextRow = nextRow + stepRow;
                    fraction = fraction - deltaCol;
                }
                nextCol = nextCol + stepCol;
                fraction = fraction + deltaRow;
                path[currentStep] = new Vector3(nextCol, nextRow, 0);
                currentStep++;
            }
        }
        else
        {
            fraction = deltaCol * 2 - deltaRow;
            while (nextRow != endRow)
            {
                if (fraction >= 0)
                {
                    nextCol = nextCol + stepCol;
                    fraction = fraction - deltaRow;
                }
                nextRow = nextRow + stepRow;
                fraction = fraction + deltaCol;

                path[currentStep] = new Vector3(nextCol, nextRow, 0);                
                currentStep++;
            }
        }

        return path;
    }
}
