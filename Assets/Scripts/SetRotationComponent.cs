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
    
    void Awake()
    {
        player = GetComponent<Player>();        
    }
    
    void Update()
    {
        GetRotation();
    }

    void GetRotation()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ��ư�� Ŭ���ϸ� ������ �ٶ󺸰�
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dir = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
                transform.forward = dir;
            }                        
            player.ChangeStateTag = StateTag.Skill; // SKILL ���·� ����
            enabled = false;
            rangeImg.gameObject.SetActive(false);
            player.skillInven.EnableSkillSlot(true);
        }
    }

    private void OnEnable()
    {
        if(player == null)
        {
            Debug.Log("üũ");          
        }

        if (player.skillInven == null)
        {
            Debug.Log("üũ2");
        }

        rangeImg.gameObject.SetActive(true);
        player.skillInven.EnableSkillSlot(false); // �������� �� ��ų���� Ŭ���ȵǰ� ����
    }

}
