using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject maze_gen;

    private void Start()
    {

    }

    public void GenerateDungeon()
    {
        maze_gen.GetComponent<MazeGeneration>().ClearDungeon();
        maze_gen.GetComponent<MazeGeneration>().RunDungeon();
    }
    public void ResetMaze()
    {
        maze_gen.GetComponent<MazeGeneration>().ClearDungeon();
    }
}
