using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform target;
    const float OFFSET_DISTANCE_Y = 4;
    const float OFFSET_DISTANCE_Z = 3;
    const float OFFSET_ANGLE_X = 50;
    public bool IsEnabled
    {
        get => enabled;
        set
        {
            enabled = value;
        }
    }       

    void Update()
    {
        transform.position = new Vector3(target.position.x, OFFSET_DISTANCE_Y, target.position.z - OFFSET_DISTANCE_Z);
        transform.eulerAngles = new Vector3(OFFSET_ANGLE_X, 0, 0);
    }
}
