using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rDepthFirstSearch : MazeGeneration
{
    public int current_room = 0;

    bool player_spawned;

    public override void GenerateDungeon()
    {
        player_spawned = false;
        Generate(Random.Range(1, maze_width -1), Random.Range(1, maze_height -1));
    }

    void Generate(int x, int y)
    {
        if (CountSquareNeighbours(x, y) >= 2)
            return;

        if(!player_spawned)
        {
            player.transform.position = new Vector2(x * x_scale, y * y_scale);
            m_camera.transform.position = new Vector3(x * x_scale, y * y_scale, -20);
            m_camera.GetComponent<Camera>().current_pos = new Vector3(x * x_scale, y * y_scale, -20);
            m_camera.GetComponent<Camera>().target_pos = new Vector3(x * x_scale, y * y_scale, -20);
            player_spawned = true;
        }

        // create room
        map[x, y] = 0;


        // shuffle values
        directions.Shuffle();

        // move to another cell in grid
        Generate(x + directions[0].x, y + directions[0].y);
        Generate(x + directions[1].x, y + directions[1].y);
        Generate(x + directions[2].x, y + directions[2].y);
        Generate(x + directions[3].x, y + directions[3].y);
    }
}
