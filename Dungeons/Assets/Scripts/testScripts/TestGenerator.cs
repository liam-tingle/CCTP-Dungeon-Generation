using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    public int dungeon_width, dungeon_height;

    public GameObject sprite;

    byte[,] map;

    int num_rooms = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        //RandomWalk();
        //Prims();
        //Wilsons();
        RDFS();
        CarveMap();
    }

    void Initialise()
    {
        map = new byte[dungeon_width, dungeon_height];

        for(int y = 0; y < dungeon_height; y++)
            for (int x = 0; x < dungeon_height; x++)
            {
                map[x, y] = 1;
            }
    }

    void CarveMap()
    {
        for (int y = 0; y < dungeon_height; y++)
            for (int x = 0; x < dungeon_height; x++)
            {
                if(map[x, y] == 1)
                {
                    Vector3 index_pos = new Vector3(x, y, 0);
                    Instantiate(sprite, index_pos, Quaternion.identity);
                }
            }
    }

    void RandomWalk()
    {
        bool completed = false;

        //int x = Random.Range(1, dungeon_width -1);
        //int y = Random.Range(1, dungeon_height - 1);

        int x = dungeon_width / 2;
        int y = dungeon_height / 2;

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

            Debug.Log(x);
            Debug.Log(y);

            completed |= x < 0 || x >= dungeon_width -1 || y < 0 || y >= dungeon_height -1;
        }
    }

    void Prims()
    {
        // Generate random values for X and Y
        // within the grids borders:
        int x = Random.Range(1, dungeon_width - 1);
        int y = Random.Range(1, dungeon_height - 1);

        // Carve out the starting cell from the grid:
        map[x, y] = 0;

        List<MapLocation> walls = new List<MapLocation>();
        walls.Add(new MapLocation(x + 1, y));
        walls.Add(new MapLocation(x - 1, y));
        walls.Add(new MapLocation(x, y + 1));
        walls.Add(new MapLocation(x, y - 1));

        int iterations = 0;

        while (walls.Count > 0 && iterations < 5000)
        {
            int rand_wall = Random.Range(0, walls.Count);
            x = walls[rand_wall].x;
            y = walls[rand_wall].y;
            walls.RemoveAt(rand_wall);

            if (CountSquareNeighbours(x, y) == 1)
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



    List<MapLocation> directions = new List<MapLocation>()
    {
        new MapLocation(1, 0),  // right
        new MapLocation(0, 1),  // up
        new MapLocation(-1, 0), // left
        new MapLocation(0, -1), // down
    };

    List<MapLocation> available = new List<MapLocation>();

    int quit_count = 0;

    void Wilsons()
    {
        int x = Random.Range(2, dungeon_width - 1);
        int y = Random.Range(2, dungeon_height - 1);

        map[x, y] = 2;//2;

        while (GetAvailableCells() > 1 && quit_count < 20)
        {
            wRandomWalk();
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

        for (int y = 1; y < dungeon_height - 1; y++)
        {
            for (int x = 1; x < dungeon_width - 1; x++)
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

        while ((current_xpos > 0 && current_xpos < dungeon_width - 1 && current_ypos > 0)
            && current_ypos < dungeon_height - 1 && loop < 5000 && !valid_path)
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
                map[m.x, m.y] = 2;//2;
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

    void RDFS()
    {
        Generate(dungeon_width / 2, dungeon_height / 2);//Random.Range(1, dungeon_width - 1), Random.Range(1, dungeon_height - 1));

        for(int x = 0; x < dungeon_width; x++)
            for(int y = 0; y < dungeon_height; y++)
            {
                if(map[x,y] == 0)
                {
                    num_rooms++;
                }
            }
        Debug.Log(num_rooms);
    }

    void Generate(int x, int y)
    {
        // return to previous instance
        if (CountSquareNeighbours(x, y) >= 2)
            return;

        // create room
        map[x, y] = 0;


        // shuffle directional values
        directions.Shuffle();

        Debug.Log(directions[0].x);
        Debug.Log(directions[0].y);

        // move to another cell in grid
        Generate(x + directions[0].x, y + directions[0].y);
        Generate(x + directions[1].x, y + directions[1].y);
        Generate(x + directions[2].x, y + directions[2].y);
        Generate(x + directions[3].x, y + directions[3].y);
    }



    public int CountSquareNeighbours(int x, int y)
    {
        int count = 0;

        if (x <= 0 || x >= dungeon_width - 1 || y <= 0 || y >= dungeon_height - 1) return 5;

        // check left neighbour
        if (map[x - 1, y] == 0) count++;
        // check above neighbour
        if (map[x, y + 1] == 0) count++;
        // check right neighbour
        if (map[x + 1, y] == 0) count++;
        // check below neighbour
        if (map[x, y - 1] == 0) count++;

        // return neighbouring [x, y] value
        return count;
    }

    public int CountDiagonalNeighbours(int x, int y)
    {
        int count = 0;

        if (x <= 0 || x >= dungeon_width - 1 || y <= 0 || y >= dungeon_height - 1) return 5;

        // check above-left neighbour
        if (map[x - 1, y + 1] == 0) count++;
        // check above-right neighbour
        if (map[x + 1, y + 1] == 0) count++;
        // check below-right neighbour
        if (map[x + 1, y - 1] == 0) count++;
        // check below-left neighbour
        if (map[x - 1, y - 1] == 0) count++;

        // return neighbouring [x, y] value
        return count;
    }

    public int CountAllNeighbours(int x, int y)
    {
        // return neighbourhood [x, y] values
        return CountSquareNeighbours(x, y) + CountDiagonalNeighbours(x, y);
    }
}
