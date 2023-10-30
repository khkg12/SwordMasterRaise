using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    const int changeZ = -70;    
    Vector3 backPos = new Vector3 (0, 0, 100);
    
    void Update()
    {
        if(transform.position.z <= changeZ)
        {
            transform.position = backPos;
        }
        transform.Translate(-Vector3.forward * 0.01f);    
    }
}
