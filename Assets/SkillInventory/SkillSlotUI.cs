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
        // ������ ����� �ְ� �ű⿡ ��Ī�ؼ� ���� Ŭ���� ������ ��ų�� �̹� �ִٸ� ������ �����Ǵ� �Ű�
        // �ִٸ� 
        for(int i = 0; i < skillQuickSlots.Length; i++)
        {
            if(skill == skillQuickSlots[i].skill)
            {
                skillQuickSlots[i].SetSkill(null); // ��ų ����
                return;
            }
        }
        // ���ٸ�
        QuickSlotEnable(true);
    }
    
    // ���ӽ����Ҷ� �ѹ��� ���� -> ��ų���� �����������״�
    // Ȱ��ȭ�ɶ����� ���� -> �÷��̾��� level�� ���� �ر����� ǥ��
    public void SetSkillSlot(List<Skill> skills)
    {
        int index = 0;
        for(int i = 0; i < SKILL_HEIGHT; ++i)
        {
            for(int j = 0; j < SKILL_WIDTH; ++j)
            {
                if(verticalSlots[i].slots[j].skill == null) // slot�� skill�� ������ ��쿡��
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
