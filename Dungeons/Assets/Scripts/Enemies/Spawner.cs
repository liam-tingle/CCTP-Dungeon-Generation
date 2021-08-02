using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mob;

    float random_x;
    float random_y;
    //float next_spawn = 0.0f;

    public int num_mobs = 3;

    Vector2 spawn_area;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num_mobs; i++)
        {
            random_x = Random.Range(-0.75f, 0.75f);
            random_y = Random.Range(-0.45f, 0.35f);
            spawn_area = new Vector2(random_x, random_y);
            GameObject new_mob =  Instantiate(mob, spawn_area, Quaternion.identity);
            new_mob.name = "mob" + i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
