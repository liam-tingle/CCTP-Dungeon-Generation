using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MazeGeneration
{
    public override void GenerateDungeon()
    {
        bool completed = false;

        //int x = Random.Range(1, dungeon_width -1);
        //int y = Random.Range(1, dungeon_height - 1);

        int x = maze_width / 2;
        int y = maze_height / 2;

        while (!completed)
        {
            map[x, y] = 0;


            if (Random.Range(0, 100) < 50)
            {
                x += Random.Range(-1, 2);
            }
            else
            {
                y += Random.Range(-1, 2);
            }

            completed |= x < 1 || x >= maze_width - 1 || y < 1 || y >= maze_height - 1;
        }
    }
}
