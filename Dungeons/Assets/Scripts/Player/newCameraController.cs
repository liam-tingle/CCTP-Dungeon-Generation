using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCameraController : MonoBehaviour
{
    public GameObject m_cam;

    public float cam_speed = 1;

    Vector3 current_pos;
    Vector3 new_pos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        current_pos = m_cam.transform.position;

        if (collision.tag == "North")
        {
            // move camera up by 1 room
            m_cam.GetComponent<Camera>().target_pos = m_cam.GetComponent<Camera>().current_pos + new Vector3(0, 1.74f, 0);

            //Debug.Log("Move Camera up");
        }
        else if (collision.tag == "South")
        {
            // move camera down by 1 room
            m_cam.GetComponent<Camera>().target_pos = m_cam.GetComponent<Camera>().current_pos - new Vector3(0, 1.74f, 0);

            //Debug.Log("Move Camera down");
        }
        else if (collision.tag == "West")
        {
            // move camera left by 1 room
            m_cam.GetComponent<Camera>().target_pos = m_cam.GetComponent<Camera>().current_pos - new Vector3(2.56f, 0, 0);

            //Debug.Log("Move Camera left");
        }
        else if (collision.tag == "East")
        {
            // move camera right by 1 room
            m_cam.GetComponent<Camera>().target_pos = m_cam.GetComponent<Camera>().current_pos + new Vector3(2.56f, 0, 0);

            //Debug.Log("Move Camera right");
        }
    }
}
