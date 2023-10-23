using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class VerticalSkillSlot
{
    const int SKILL_WIDTH = 5;
    public SkillSlot[] slots = new SkillSlot[SKILL_WIDTH];
}

public class SkillSlotUI : MonoBehaviour
{    
    const int SKILL_HEIGHT = 2;
    const int SKILL_WIDTH = 5;
    public VerticalSkillSlot[] verticalSlots = new VerticalSkillSlot[SKILL_HEIGHT];
    [SerializeField] Image selectSkillImage;
    [SerializeField] TextMeshProUGUI selectSkillText;
    [SerializeField] Button equipSkillBtn;
    [SerializeField] SkillQuickSlot[] skillQuickSlots;
    public Skill skill;

    void Start()
    {
        SetSkillSlot(GameManager.instance.skillList);
        SetQuickSlot();
        equipSkillBtn.onClick.AddListener(()=>EquipSkill());
    }

    public void SetSelectSkill(Skill skill)
    {
        this.skill = skill; 
        selectSkillImage.sprite = skill.sprite;
        selectSkillText.text = skill.skillText; 
    }

    public void EnableEquipBtn(bool isEnabled)
    {
        equipSkillBtn.gameObject.SetActive(isEnabled);        
    }

    public void EquipSkill()
    {
        // 퀵슬롯 목록이 있고 거기에 매칭해서 현재 클릭한 슬롯의 스킬이 이미 있다면 장착이 해제되는 거고
        // 있다면 
        for(int i = 0; i < skillQuickSlots.Length; i++)
        {
            if(skill == skillQuickSlots[i].skill)
            {
                skillQuickSlots[i].SetSkill(null); // 스킬 해제
                return;
            }
        }
        // 없다면
        QuickSlotEnable(true);
    }
    
    // 게임실행할때 한번만 실행 -> 스킬들은 고정되있을테니
    // 활성화될때마다 실행 -> 플레이어의 level에 따른 해금해제 표시
    public void SetSkillSlot(List<Skill> skills)
    {
        int index = 0;
        for(int i = 0; i < SKILL_HEIGHT; ++i)
        {
            for(int j = 0; j < SKILL_WIDTH; ++j)
            {
                if(verticalSlots[i].slots[j].skill == null) // slot의 skill이 존재할 경우에만
                {
                    verticalSlots[i].slots[j].ownerSkillInven = this;
                    verticalSlots[i].slots[j].SetSkill(skills[index]);
                    index++;
                }                
            }
        }
    }

    public void SetQuickSlot()
    {
        for(int i = 0; i < skillQuickSlots.Length; i++)
        {
            skillQuickSlots[i].ownerSkillInven = this;
        }
    }

    public void QuickSlotEnable(bool isEnabled)
    {
        for (int i = 0; i < skillQuickSlots.Length; i++)
        {
            skillQuickSlots[i].enabled = isEnabled;
            skillQuickSlots[i].SelectEnabled(isEnabled);
        }
    }
}
