using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleSkillSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image skillImage;
    [SerializeField] Image shadowImage;
    [SerializeField] Image emptyImage;    
    Skill skill;      
    bool isUse;
    public SkillInventoryUI ownerInven;

    private void Start()
    {
        isUse = true;        
    }   

    IEnumerator CoolTimeCo()
    {
        isUse = false;
        float nowTime = 0;
        while(nowTime < skill.coolTime)
        {
            nowTime += Time.deltaTime;
            skillImage.fillAmount = nowTime / skill.coolTime;
            yield return null;
        }        
        isUse = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        if (isUse)
        {
            ownerInven.owner.ExecuteSkill(skill);            
            StartCoroutine(CoolTimeCo());
        }        
    }

    public void SetSkillSlot(Skill setSkill)
    {
        skill = setSkill;
        if(skill != null)
        {
            skillImage.sprite = skill.sprite;
            shadowImage.sprite = skill.sprite;
        }
        else
            enabled = false;   
        emptyImage.gameObject.SetActive(skill == null);
    }
}
