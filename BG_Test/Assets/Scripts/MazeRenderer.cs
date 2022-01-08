using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
	[Range(1, 50)]
	[SerializeField]private int width = 10;

	[Range(1, 50)]
	[SerializeField]private int height = 10;

	[SerializeField]private Transform wallPrefab = null;
	[SerializeField]private Transform floorPrefab = null;
    

    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);

        Draw(maze);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
    }

    
    void Draw(WallState[,] maze)
    {

    	var floor = Instantiate(floorPrefab, transform);
    	floor.localScale = new Vector3(width / 10, 0.1f, height / 10);
    	floor.localPosition = new Vector3(floor.localPosition.x - 0.5f, floor.localPosition.y, floor.localPosition.z - 0.5f);



        for(int i = 0; i < width; ++i)
    	{
    			for(int j = 0; j < height; ++j)
    		{

    			var cell = maze[i,j];
    			var position = new Vector3(-width/2 + i, 0, -height/2 + j);

    			if(cell.HasFlag(WallState.UP))
    			{
    				var topWall = Instantiate(wallPrefab, transform) as Transform;
    				topWall.position = position + new Vector3(0, 0, 0.5f);
    				topWall.localScale = new Vector3(1, topWall.localScale.y, topWall.localScale.z);
    			}

    			if(cell.HasFlag(WallState.LEFT))
    			{
    				var leftWall = Instantiate(wallPrefab, transform) as Transform;
    				leftWall.position = position + new Vector3(-0.5f, 0, 0);
    				leftWall.localScale = new Vector3(1, leftWall.localScale.y, leftWall.localScale.z);
    				leftWall.eulerAngles = new Vector3(0, 90, 0);
    			}

    			if(i == width - 1)
    			{
    				if(cell.HasFlag(WallState.RIGHT))
    				{
    					var rightWall = Instantiate(wallPrefab, transform) as Transform;
    					rightWall.position = position + new Vector3(0.5f, 0, 0);
    					rightWall.localScale = new Vector3(1, rightWall.localScale.y, rightWall.localScale.z);
    					rightWall.eulerAngles = new Vector3(0, 90, 0);
    				}
    			}

    			if(j == 0)
    			{
    				if(cell.HasFlag(WallState.DOWN))
    				{
    					var bottomWall = Instantiate(wallPrefab, transform) as Transform;
    					bottomWall.position = position + new Vector3(0, 0, -0.5f);
    					bottomWall.localScale = new Vector3(1, bottomWall.localScale.y, bottomWall.localScale.z);
    				}
    			}

    		}
    	}

    }


}
