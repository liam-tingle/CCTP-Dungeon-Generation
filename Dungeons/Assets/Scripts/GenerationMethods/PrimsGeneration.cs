using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsGeneration : MazeGeneration
{
    public override void GenerateDungeon()
    {
        // Generate random values for X and Y
        // within the grids borders:
        int x = Random.Range(1, maze_height - 1); 
        int y = Random.Range(1, maze_height - 1); 

        // Carve out the starting cell from the grid:
        map[x, y] = 0;

        // spawn the player and position the camera
        player.transform.position = new Vector2(x * x_scale, y * y_scale);
        m_camera.transform.position = new Vector3(x * x_scale, y * y_scale, -20);
        m_camera.GetComponent<Camera>().current_pos = new Vector3(x * x_scale, y * y_scale, -20);
        m_camera.GetComponent<Camera>().target_pos = new Vector3(x * x_scale, y * y_scale, -20);

        List<MapLocation> walls = new List<MapLocation>();
        walls.Add(new MapLocation(x + 1, y));
        walls.Add(new MapLocation(x - 1, y));
        walls.Add(new MapLocation(x, y + 1));
        walls.Add(new MapLocation(x, y - 1));

        int iterations = 0;

        while(walls.Count > 0 && iterations < 5000)
        {
            int rand_wall = Random.Range(0, walls.Count);
            x = walls[rand_wall].x;
            y = walls[rand_wall].y;
            walls.RemoveAt(rand_wall);

            if(CountSquareNeighbours(x, y) == 1)
            {
                map[x, y] = 0;

                walls.Add(new MapLocation(x + 1, y));
                walls.Add(new MapLocation(x - 1, y));
                walls.Add(new MapLocation(x, y + 1));
                walls.Add(new MapLocation(x, y - 1));
            }

            iterations++;
        }
    }
}
