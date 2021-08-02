using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilsonsAlgorithm : MazeGeneration
{
    List<MapLocation> available = new List<MapLocation>();

    int quit_count = 0;

    void Wilsons()
    {
        int x = Random.Range(2, maze_width - 1);
        int y = Random.Range(2, maze_height - 1);

        map[x, y] = 2;

        while (GetAvailableCells() > 1 && quit_count < 20)
        {
            wRandomWalk();
        }

        for(int i = 0; i < maze_width; i++)
            for (int j = 0; j < maze_width; j++)
            {
                if(map[i,j] == 2)
                {
                    map[i,j] = 0;
                }
            }
    }

    int CSNeighbours(int x, int y)
    {

        int count = 0;

        for (int d = 0; d < directions.Count; d++)
        {
            int next_xpos = x + directions[d].x;
            int next_ypos = y + directions[d].y;

            if (map[next_xpos, next_ypos] == 2) 
            {
                count++;
            }
        }

        return count;
    }

    int GetAvailableCells()
    {
        available.Clear();

        for (int y = 1; y < maze_height - 1; y++)
        {
            for (int x = 1; x < maze_width - 1; x++)
            {
                if (CSNeighbours(x, y) == 0)
                {
                    available.Add(new MapLocation(x, y));
                }
            }
        }

        return available.Count;
    }

    void wRandomWalk()
    {
        // keep track of visited locations
        List<MapLocation> visited = new List<MapLocation>();

        int current_xpos;
        int current_ypos;
        int random_start = Random.Range(0, available.Count);

        current_xpos = available[random_start].x;
        current_ypos = available[random_start].y;

        visited.Add(new MapLocation(current_xpos, current_ypos));

        int loop = 0;

        bool valid_path = false;

        while ((current_xpos > 0 && current_xpos < maze_width - 1 && current_ypos > 0)
            && current_ypos < maze_height - 1 && loop < 5000 && !valid_path)
        {

            map[current_xpos, current_ypos] = 0;

            if (CSNeighbours(current_xpos, current_ypos) > 1)
                break;

            int rand_direction = Random.Range(0, directions.Count);
            int next_xpos = current_xpos + directions[rand_direction].x;
            int next_ypos = current_ypos + directions[rand_direction].y;

            if (CountSquareNeighbours(next_xpos, next_ypos) < 2) 
            {
                current_xpos = next_xpos;
                current_ypos = next_ypos;
                visited.Add(new MapLocation(current_xpos, current_ypos));
            }
            valid_path = CSNeighbours(current_xpos, current_ypos) == 1;

            loop++;
        }

        // add cells in path to maze
        if (valid_path)
        {
            map[current_xpos, current_ypos] = 0;
            Debug.Log("PathFound");

            foreach (MapLocation m in visited)
            {
                map[m.x, m.y] = 2;
            }

            visited.Clear();
            quit_count = 0;
        }
        // restart the loop
        else
        {
            Debug.Log("Attempt Failed");

            foreach (MapLocation m in visited)
            {
                map[m.x, m.y] = 1;
            }

            visited.Clear();
            quit_count++;
        }
    }
}
