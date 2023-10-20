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
    public GameObject emptySprite;
    public Skill skill = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(skill != null)
        {

        }
    }

    public void SetSkill(Skill setSkill)
    {
        skill = setSkill;
        if(skill != null )
        {
            image.sprite = skill.sprite;            
        }
        emptySprite.SetActive(skill == null);
    }
}
