using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int y;

    public MapLocation(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

public class MazeGeneration : MonoBehaviour
{
    public List<MapLocation> directions = new List<MapLocation>()
    {
        new MapLocation(1, 0),  // East
        new MapLocation(0, 1),  // North
        new MapLocation(-1, 0), // West
        new MapLocation(0, -1), // South
    };

    public List<MapLocation> locations = new List<MapLocation>();

    public MapLocation StartOfDungeon;
    public MapLocation EndOfDungeon;
    
    // straights
    public GameObject ns_room;
    public GameObject we_room;
    // corners
    public GameObject es_room;
    public GameObject ws_room;
    public GameObject ne_room;
    public GameObject nw_room;
    // ends
    public GameObject n_room;
    public GameObject s_room;
    public GameObject e_room;
    public GameObject w_room;
    // junctions
    public GameObject nswe_room;
    public GameObject nwe_room;
    public GameObject swe_room;
    public GameObject nsw_room;
    public GameObject nse_room;

    // Locked door
    //public GameObject locked_door;

    public enum RoomType
    {
        Wall,
        Room,
        Boss,
        Shop,
    }

    public struct Rooms
    {
        public RoomType room_type;
        public GameObject room;

        public Rooms(RoomType rt, GameObject r)
        {
            room_type = rt;
            room = r;
        }
    }

    public Rooms[,] room_places;

    // player
    public GameObject player;
    public GameObject m_camera;

    public MazeGeneration dungeon;

    public int maze_width;
    public int maze_height;
    public float x_scale = 2.56f;
    public float y_scale = 1.74f;

    public byte[,] map;

    // Start is called before the first frame update
    void Start()
    {
        maze_height = GameObject.FindObjectOfType<GameManager>().maze_size;
        maze_width = GameObject.FindObjectOfType<GameManager>().maze_size;

        RunDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        // dungeon failsafe
        if (GameObject.FindGameObjectWithTag("Room") == null)
        {
            Debug.Log("Error");
            ClearDungeon();
            RunDungeon();
        }
    }

    public void RunDungeon()
    {
        InitialiseDungeon();
        GenerateDungeon();
        DrawDungeon();

        // get all positions in grid that are NOT equal to 1
        // and put them in a list
        for(int y = 1; y < maze_height - 1; y++) // 1
        {
            for (int x = 1; x < maze_width - 1; x++) // 1
            {
                if(map[x,y] != 1)
                {
                    locations.Add(new MapLocation(x, y));
                }
            }
        }

        // spawn the dungeon exit
        EndLevel stairs = GetComponent<EndLevel>();
        stairs.Add(dungeon);
            
    }

    // Clear the dungeon grid of placed rooms
    public void ClearDungeon()
    {
        // find all rooms in the grid and destroy them
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        
        for(var i = 0; i < rooms.Length; i++)
        {
            Destroy(rooms[i]);
        }

        // find all enimies in the scene and destroy them for the new genertion
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
    }

    // initialise dungeon values
    void InitialiseDungeon()
    {
        map = new byte[maze_width, maze_height];
        room_places = new Rooms[maze_width, maze_height];

        for (int y = 0; y < maze_width; y++)
        {
            for (int x = 0; x < maze_height; x++)
            {
                map[x, y] = 1;
            }
        }
    }
    
    public virtual void GenerateDungeon()
    {

    }

    // place rooms depending on neighbours
    void DrawDungeon()
    {
        for (int y = 0; y < maze_width; y++)
        {
            for (int x = 0; x < maze_height; x++)
            {
                if (map[x, y] == 1)
                {
                    room_places[x, y].room_type = RoomType.Wall;
                    room_places[x, y].room = null;
                }
                // NS room
                else if(Search(x, y, new int[] {5, 0, 5, 1, 0, 1, 5, 0, 5})) //||
                        //Search(x, y, new int[] { 5, 0, 5, 1, 3, 1, 5, 0, 5 }))
                {
                    
                    GameObject g_obj = Instantiate(ns_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // WE room
                else if (Search(x, y, new int[] { 5, 1, 5, 0, 0, 0, 5, 1, 5})) //||
                         //Search(x, y, new int[] { 5, 1, 5, 0, 3, 0, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(we_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // corners 
                // WS room
                else if (Search(x, y, new int[] { 5, 1, 5, 0, 0, 1, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 0, 3, 1, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(ws_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // ES room
                else if (Search(x, y, new int[] { 5, 1, 5, 1, 0, 0, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 1, 3, 0, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(es_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // NE room
                else if (Search(x, y, new int[] { 5, 0, 5, 1, 0, 0, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 1, 3, 0, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(ne_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // NW room
                else if (Search(x, y, new int[] { 5, 0, 5, 0, 0, 1, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 0, 3, 1, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(nw_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // End rooms
                // n room
                else if (Search(x, y, new int[] { 5, 0, 5, 1, 0, 1, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 1, 3, 1, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(n_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // s room
                else if (Search(x, y, new int[] { 5, 1, 5, 1, 0, 1, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 1, 3, 1, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(s_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // e room
                else if (Search(x, y, new int[] { 5, 1, 5, 1, 0, 0, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 1, 3, 0, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(e_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // w room
                else if (Search(x, y, new int[] { 5, 1, 5, 0, 0, 1, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 0, 3, 1, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(w_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // junction rooms
                // nswe room
                else if (Search(x, y, new int[] { 5, 0, 5, 0, 0, 0, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 0, 3, 0, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(nswe_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // nwe room
                else if (Search(x, y, new int[] { 5, 0, 5, 0, 0, 0, 5, 1, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 0, 3, 0, 5, 1, 5 }))
                {
                    GameObject g_obj = Instantiate(nwe_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // swe room
                else if (Search(x, y, new int[] { 5, 1, 5, 0, 0, 0, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 1, 5, 0, 3, 0, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(swe_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // nsw room
                else if (Search(x, y, new int[] { 5, 0, 5, 0, 0, 1, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 0, 3, 1, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(nsw_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
                // nse room
                else if (Search(x, y, new int[] { 5, 0, 5, 1, 0, 0, 5, 0, 5 })) //||
                         //Search(x, y, new int[] { 5, 0, 5, 1, 3, 0, 5, 0, 5 }))
                {
                    GameObject g_obj = Instantiate(nse_room);
                    g_obj.transform.position = new Vector3(x * x_scale, y * y_scale, 0);

                    room_places[x, y].room_type = RoomType.Room;
                    room_places[x, y].room = g_obj;
                }
            }
        }
    }

    bool Search(int column, int row, int [] pattern)
    {
        int count = 0;
        int position = 0;

        for(int y = 1; y > -2; y--)
        {
            for(int x = -1; x < 2; x++)
            {
                if (pattern[position] == map[column + x, row + y] || pattern[position] == 5)
                    count++;
                position++;
            }
        }

        return (count == 9);
    }

    public int CountSquareNeighbours(int x, int y)
    {
        int count = 0;

        if (x <= 0 || x >= maze_width - 1 || y <= 0 || y >= maze_height - 1) return 5;

        // check left neighbour
        if (map[x -1, y] == 0 || map[x - 1, y] == 3) count++;
        // check above neighbour
        if (map[x, y +1] == 0 || map[x, y + 1] == 3) count++;
        // check right neighbour
        if (map[x +1, y] == 0 || map[x + 1, y] == 3) count++;
        // check below neighbour
        if (map[x, y -1] == 0 || map[x, y - 1] == 3) count++;

        // return neighbouring [x, y] value
        return count;
    }

    public int CountDiagonalNeighbours(int x, int y)
    {
        int count = 0;

        if (x <= 0 || x >= maze_width - 1 || y <= 0 || y >= maze_height - 1) return 5;

        // check above-left neighbour
        if (map[x - 1, y +1] == 0 || map[x - 1, y + 1] == 3) count++;
        // check above-right neighbour
        if (map[x +1, y + 1] == 0 || map[x + 1, y + 1] == 3) count++;
        // check below-right neighbour
        if (map[x + 1, y -1] == 0 || map[x + 1, y - 1] == 3) count++;
        // check below-left neighbour
        if (map[x -1, y - 1] == 0 || map[x - 1, y - 1] == 3) count++;

        // return neighbouring [x, y] value
        return count;
    }

    public int CountAllNeighbours(int x, int y)
    {
        // return neighbourhood [x, y] values
        return CountSquareNeighbours(x, y) + CountDiagonalNeighbours(x, y);
    }
}
