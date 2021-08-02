using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemies : MonoBehaviour
{
    public GameObject mob;
    public GameObject[] spawn_points;

    public int num_mobs = 3;
    public int current_mobs = 0;

    bool spawn;

    private void Start()
    {

    }

    private void Update()
    {
        if(spawn)
        {
            while (current_mobs < num_mobs)
            {
                int spawn_num = Random.Range(0, spawn_points.Length);
                GameObject new_mob = Instantiate(mob, spawn_points[spawn_num].transform.position, Quaternion.identity);
                Debug.Log("Generate mob :" + current_mobs);
                current_mobs++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spawn = true;
        }
    }
}
