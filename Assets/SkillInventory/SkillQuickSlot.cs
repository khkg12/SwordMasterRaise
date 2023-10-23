using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillQuickSlot : MonoBehaviour, IPointerClickHandler
{
    public Skill skill = null;
    [SerializeField] Image image;
    [SerializeField] Image arrow;
    public SkillSlotUI ownerSkillInven;
    public int index;

    public void SetSkill(Skill setSkill)
    {
        skill = setSkill;
        if (skill != null)
        {
            image.sprite = skill.sprite;            
        }
        else
        {
            image.sprite = null;    
        }
        GameManager.instance.playerSkillList[index] = skill;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSkill(ownerSkillInven.skill);        
        ownerSkillInven.QuickSlotEnable(false);
    }

    public void SelectEnabled(bool isEnabled)
    {
        arrow.gameObject.SetActive(isEnabled);
    }
}
