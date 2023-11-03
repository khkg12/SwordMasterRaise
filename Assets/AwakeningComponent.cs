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

    public int AwakeLevel // 레벨 세팅하면서 오브젝트도 세팅
    {
        get => awakeLevel;
        set
        {
            awakeLevel = value;
            if(awakeLevel != 0) // 0이 아닐때만
            {
                enabled = true;
                awakeSkill = awakeSkillList[awakeLevel]; // 각성 오브젝트 세팅                
                PoolManager.instance.InitSkillPool(awakeSkill);
                requiredAttackNum = requiredAttackNums[awakeLevel]; // 필요타수 세팅
            }            
        }
    }
    [SerializeField] private int awakeLevel;

    // 어택 타수 프로퍼티, 플레이어에서 한개씩 더해주고
    // 해당 오브젝트의 충족개수를 만족하면
    // 오브젝트 생성, 다시 0으로 초기화
    // 플레이어에선 더해주기만 하면됨

    private void Awake()
    {
        character = GetComponent<Character>();  
    }   
}
