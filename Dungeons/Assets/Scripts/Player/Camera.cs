using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 current_pos;
    public Vector3 target_pos;

    public int cam_speed; 

    // Start is called before the first frame update
    void Start()
    {
        //current_pos = transform.position;
        target_pos = current_pos;
    }

    // Update is called once per frame
    void Update()
    {
        current_pos = transform.position;

        Movement();
    }

    void Movement()
    {
        current_pos = transform.
        transform.position = Vector3.Lerp(current_pos, target_pos, cam_speed * Time.deltaTime);
    }
}
