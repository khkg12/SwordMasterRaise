using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    // 스킬슬롯이 가지고 있어야 할 것
    public Image image;
    public GameObject lockSprite;
    public SkillSlotUI ownerSkillInven;
    public bool IsLock
    {
        get => isLock;
        set
        {
            isLock = value;
            lockSprite.SetActive(isLock);
        }
    }
    public bool isLock;
    public Skill skill = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 정보창에 스킬에 대한 정보 출력
        ownerSkillInven.SetSelectSkill(skill);
        ownerSkillInven.EnableEquipBtn(!IsLock);       
    }

    public void SetSkill(Skill setSkill)
    {
        skill = setSkill;
        if(skill != null)
        {
            image.sprite = skill.sprite;            
        }        
        IsLock = GameManager.instance.Level < skill.requiredLevel;
        Debug.Log(isLock);
    }
}
