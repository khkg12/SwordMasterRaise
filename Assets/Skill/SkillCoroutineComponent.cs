using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoroutineComponent : MonoBehaviour
{
    Collider col;
    [SerializeField] float ENABLED_DELAY;
    [SerializeField] float ENABLED_MAINTAIN_TIME;
    [SerializeField] float CAMERA_DURATION_TIME;
    [SerializeField] Vector3 CAMERA_OFFSET_POS;
    bool isReady;    

    private void Awake()
    {        
        col = GetComponent<Collider>();
    }
    void OnEnable()
    {        
        col.enabled = false;
        if (isReady) // isReady�� true�϶���, �� �ʱ⿣ �����������
        {
            StartCoroutine(EnabledColCo());
            if(CAMERA_OFFSET_POS.magnitude != 0) // ī�޶� ���� �����°��� 0�̶�� ������ϵ���
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
