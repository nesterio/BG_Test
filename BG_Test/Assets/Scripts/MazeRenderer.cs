using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
	[Header("Assigning the size of the maze")]
	[Range(1, 50)]
	[SerializeField]private int width = 10;

	[Range(1, 50)]
	[SerializeField]private int height = 10;

	[Header("Assigning prefabs")]
	[SerializeField]private Transform wallPrefab = null;
	[SerializeField]private Transform floorObj = null;
	[SerializeField]private Transform playerPrefab = null;
	[SerializeField]private Transform exitPrefab = null;

	private Transform player;
	private Transform exit;
    

    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);

        Draw(maze);

        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
    }

    
    void Draw(WallState[,] maze)
    {

    	floorObj.localScale = new Vector3(width / 10, 0.1f, height / 10);



        for(int i = 0; i < width; ++i)
    	{
    			for(int j = 0; j < height; ++j)
    		{

    			var cell = maze[i,j];
    			var position = new Vector3(-width/2 + i, 0, -height/2 + j);

    			if(cell.HasFlag(WallState.UP))
    			{
    				var topWall = Instantiate(wallPrefab, transform) as Transform;
    				topWall.position = position + new Vector3(0, 0 + topWall.localScale.y/2, 0.5f);
    				topWall.localScale = new Vector3(1, topWall.localScale.y, topWall.localScale.z);
    			}

    			if(cell.HasFlag(WallState.LEFT))
    			{
    				var leftWall = Instantiate(wallPrefab, transform) as Transform;
    				leftWall.position = position + new Vector3(-0.5f, 0 + leftWall.localScale.y/2, 0);
    				leftWall.localScale = new Vector3(1, leftWall.localScale.y, leftWall.localScale.z);
    				leftWall.eulerAngles = new Vector3(0, 90, 0);
    			}

    			if(i == width - 1 || cell.HasFlag(WallState.RIGHT))
    			{
    					var rightWall = Instantiate(wallPrefab, transform) as Transform;
    					rightWall.position = position + new Vector3(0.5f, 0 + rightWall.localScale.y/2, 0);
    					rightWall.localScale = new Vector3(1, rightWall.localScale.y, rightWall.localScale.z);
    					rightWall.eulerAngles = new Vector3(0, 90, 0);
    			}

    			if(j == 0 || cell.HasFlag(WallState.DOWN))
    			{
    					var bottomWall = Instantiate(wallPrefab, transform) as Transform;
    					bottomWall.position = position + new Vector3(0, 0 + bottomWall.localScale.y/2, -0.5f);
    					bottomWall.localScale = new Vector3(1, bottomWall.localScale.y, bottomWall.localScale.z);
    			}

    			if(i == width - 1 && j == 0)
    			{
    				player = Instantiate(playerPrefab, transform);
    				player.position = position + new Vector3(0, player.localScale.y/2, 0);
    			}

    			if(i == 0 && j == height - 1)
    			{
    				exit = Instantiate(exitPrefab, transform);
    				exit.position = position + new Vector3(0, exit.localScale.y/2, 0);
    			}

    		}
    	}

    	player.GetComponent<PlayerScript>().SetTarget(exit);

    }


}
