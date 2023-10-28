using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventoryUI : MonoBehaviour
{
    [SerializeField] BattleSkillSlot[] skillSlots;
    public Player owner;
    private void Start()
    {
        SlotInit();
    }

    public void SlotInit()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].SetSkillSlot(GameManager.instance.playerSkillList[i]);
            skillSlots[i].ownerInven = this;
        }
    }

    public void EnableSkillSlot(bool isEnabled)
    {
        for(int i = 0; i < skillSlots.Length; i++)
        {
            if(skillSlots[i].Skill != null) // null�� �ƴѰ�쿡��, �� ��ų�� ���Կ� ���� ����
                skillSlots[i].enabled = isEnabled;
        }
    }
}
