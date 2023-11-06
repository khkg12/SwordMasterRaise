using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class SkillCoStrategy
//{
//    public virtual IEnumerator CustomCo()
//    {
//        yield return null;
//    }
//}

//public class SkillEnableCo : SkillCoStrategy
//{

//}


//public class SkillCameraCo : SkillCoStrategy
//{

//}


//[Flags]
//public enum COROUTINE_TYPE
//{
//    ENABLE = 1<<0,
//    CAMERA = 1<<1,
//    ETC  = 1<<2
//}

public class SkillCoroutineComponent : MonoBehaviour
{
    Collider col;
    [SerializeField] float ENABLED_DELAY;
    [SerializeField] float ENABLED_MAINTAIN_TIME;
    [SerializeField] float CAMERA_DURATION_TIME;
    [SerializeField] Vector3 CAMERA_OFFSET_POS;
    bool isReady;
    //public COROUTINE_TYPE type;
    //public List<SkillCoStrategy> coList = new List<SkillCoStrategy>();


    private void Awake()
    {        
        col = GetComponent<Collider>();    
    }
    void OnEnable()
    {        
        //foreach(var co in coList)
        //{
        //    StartCoroutine(co.CustomCo());
        //}

        col.enabled = false;
        if (isReady) // isReady가 true일때만, 즉 초기엔 실행되지않음
        {
            StartCoroutine(EnabledColCo());
            if(CAMERA_OFFSET_POS.magnitude != 0) // 카메라 무브 오프셋값이 0이라면 실행안하도록
                StartCoroutine(CameraMoveCo());
        }
        isReady = true;
    }

    IEnumerator EnabledColCo()
    {
        yield return new WaitForSeconds(ENABLED_DELAY);
        col.enabled = true;
        yield return new WaitForSeconds(ENABLED_MAINTAIN_TIME);
        col.enabled = false;
    }

    IEnumerator CameraMoveCo()
    {
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = false;
        Camera.main.transform.position += CAMERA_OFFSET_POS;
        yield return new WaitForSeconds(CAMERA_DURATION_TIME);
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = true;
    }
}
