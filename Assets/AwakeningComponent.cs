using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeningComponent : MonoBehaviour
{
    [SerializeField] private List<Skill> awakeSkillList;    
    [SerializeField] private int[] requiredAttackNums;
    int requiredAttackNum;
    Skill awakeSkill;
    Character character;
    public int AttackNum
    {
        get => attackNum;
        set
        {
            attackNum = value;
            if(attackNum == requiredAttackNum)
            {
                attackNum = 0;
                awakeSkill.Use(character);
            }
        }
    }
    private int attackNum;

    public int AwakeLevel // ���� �����ϸ鼭 ������Ʈ�� ����
    {
        get => awakeLevel;
        set
        {
            awakeLevel = value;
            if(awakeLevel != 0) // 0�� �ƴҶ���
            {
                enabled = true;
                awakeSkill = awakeSkillList[awakeLevel]; // ���� ������Ʈ ����                
                PoolManager.instance.InitSkillPool(awakeSkill);
                requiredAttackNum = requiredAttackNums[awakeLevel]; // �ʿ�Ÿ�� ����
            }            
        }
    }
    [SerializeField] private int awakeLevel;

    // ���� Ÿ�� ������Ƽ, �÷��̾�� �Ѱ��� �����ְ�
    // �ش� ������Ʈ�� ���������� �����ϸ�
    // ������Ʈ ����, �ٽ� 0���� �ʱ�ȭ
    // �÷��̾�� �����ֱ⸸ �ϸ��

    private void Awake()
    {
        character = GetComponent<Character>();  
    }   
}
