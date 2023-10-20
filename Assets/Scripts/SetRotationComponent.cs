using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;

public class SetRotationComponent : MonoBehaviour
{
    Character character;
    [SerializeField] Image rangeImg;
    
    void Start()
    {
        character = GetComponent<Character>();        
    }
    
    void Update()
    {
        GetRotation();
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
            character.ChangeStateTag = StateTag.Skill; // SKILL 상태로 변경
            enabled = false;
            rangeImg.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        rangeImg.gameObject.SetActive(true);
    }

}
