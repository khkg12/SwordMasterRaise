using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;

public class SetRotationComponent : MonoBehaviour
{
    Player player;
    [SerializeField] Image rangeImg;
    const float MAX_SCALE_TIME = 0.5f;
    Vector3 originImgScale;
    Vector3 offsetVec = new Vector3(0.01f, 0.01f, 0);
    float nowTime;

    void Awake()
    {
        originImgScale = rangeImg.rectTransform.localScale;
        player = GetComponent<Player>();        
    }
    
    void Update()
    {
        GetRotation();
        RangeImageActive();
    }

    void GetRotation()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 클릭하면 그쪽을 바라보게
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dir = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
                transform.forward = dir;
            }                        
            player.ChangeStateTag = StateTag.Skill; // SKILL 상태로 변경
            enabled = false;
            rangeImg.gameObject.SetActive(false);
            player.skillInven.EnableSkillSlot(true);
        }
    }

    void RangeImageActive()
    {
        nowTime += Time.deltaTime;
        rangeImg.rectTransform.localScale += offsetVec;
        if (nowTime >= MAX_SCALE_TIME)
        {
            rangeImg.rectTransform.localScale = originImgScale;
            nowTime = 0;
        }
    }

    private void OnEnable()
    {
        rangeImg.rectTransform.localScale = originImgScale;
        rangeImg.gameObject.SetActive(true);
        player.skillInven.EnableSkillSlot(false); // 방향정할 때 스킬슬롯 클릭안되게 끄기
    }

}
