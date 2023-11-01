using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrikeSwordController : InPlaceObj
{
    Collider col;    
    const float ENABLED_DELAY = 0.5f;    
    const float DURATION_TIME = 0.5f;
    Vector3 offsetPos = new Vector3(0, 3, -5);
    bool isReady;

    private void Awake()
    {        
        col = GetComponent<Collider>();
    }
    new void OnEnable()
    {        
        base.OnEnable();                
        col.enabled = false;        
        if(isReady) // isReady가 true일때만, 즉 초기엔 실행되지않음
        {            
            StartCoroutine(EnabledColCo()); 
            StartCoroutine(CameraMoveCo());
        }
        isReady = true;
    }        

    IEnumerator EnabledColCo()
    {                
        yield return new WaitForSeconds(ENABLED_DELAY);        
        col.enabled = true;
    }

    IEnumerator CameraMoveCo()
    {
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = false; 
        Camera.main.transform.position += offsetPos;
        yield return new WaitForSeconds(DURATION_TIME);
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = true;
    }
}

