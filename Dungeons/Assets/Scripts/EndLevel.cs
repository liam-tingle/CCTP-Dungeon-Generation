using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject Stairs;

    public void Add(MazeGeneration end)
    {
        int end_index = Random.Range(0, end.locations.Count);
        Vector3 stairs_pos = end.room_places[end.locations[end_index].x, end.locations[end_index].y].room.transform.position + new Vector3(0.32f, 0.075f, 0);
        Stairs.transform.position = stairs_pos;
    }
}
