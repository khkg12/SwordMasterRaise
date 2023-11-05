using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoroutineComponent : MonoBehaviour
{
    Collider col;
    [SerializeField] float ENABLED_DELAY;
    [SerializeField] float ENABLED_MAINTAIN_TIME;
    [SerializeField] float CAMERA_DURATION_TIME;
    Vector3 offsetPos = new Vector3(0, 3, -5);
    bool isReady;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    void OnEnable()
    {        
        col.enabled = false;
        if (isReady) // isReady가 true일때만, 즉 초기엔 실행되지않음
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
        yield return new WaitForSeconds(ENABLED_MAINTAIN_TIME);
        col.enabled = false;
    }

    IEnumerator CameraMoveCo()
    {
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = false;
        Camera.main.transform.position += offsetPos;
        yield return new WaitForSeconds(CAMERA_DURATION_TIME);
        Camera.main.transform.GetComponent<FollowCam>().IsEnabled = true;
    }
}
