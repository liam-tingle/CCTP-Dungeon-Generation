using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public static CameraController instance;

    //public RoomScript current_room;

    //public float camera_move_speed;

    //void Awake()
    //{
    //    instance = this;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    UpdateCamPos();
    //}

    //void UpdateCamPos()
    //{
    //    if(current_room == null)
    //    {
    //        return;
    //    }

    //    Vector3 target_pos = GetCamTargetPos();

    //    transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * camera_move_speed);
    //}

    //Vector3 GetCamTargetPos()
    //{
    //    if (current_room == null)
    //    {
    //        return Vector3.zero;
    //    }

    //    Vector3 target_pos = current_room.CentreRoom();
    //    target_pos.z = transform.position.z;

    //    return target_pos;
    //}

    //public bool isSwitching()
    //{
    //    return transform.position.Equals(GetCamTargetPos()) == false;
    //}
}
