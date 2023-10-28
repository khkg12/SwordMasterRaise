using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int atk;
    public int hp;
    public int level;
    public int gold;    
}

// 스탯에 대한 정보를 구조체로 정리 -> ex) hp, 필요골드, 증가량
public struct StatInfo
{
    int stat;
    int requireGold;
    int increaseAmount;
}

public class GameManager : Singleton<GameManager>
{    
    public int atk;
    public int hp;
    public int Level
    {
        get => level;
        set
        {
            level = value;
            if(level >= 10)
            {
                // 스킬개방
            }
            if (level >= 20)
            {

            }
            if (level >= 30)
            {

            }
            if (level >= 40)
            {

            }
        }
    }
    private int level;
    public int gold;
    public int upgradeHpGold;
    public int upgradeAtkGold;
    public int upgradeCritical;

    public List<Skill> skillList; // 모든스킬 담고있는 스킬리스트
    public Skill[] playerSkillList; // 플레이어가 배틀 시 사용할 스킬배열
    public ItemInfo equipItemInfo; // 플레이어가 장착할 아이템, 프로퍼티로 처리해야할수도
    public int monsterCount;

    new void Awake()
    {
        base.Awake();        
    }
    private void Start()
    {
        DataInit(); // 데이터 초기화
        Level = 3;
    }

    public void DataInit()
    {
        atk = DataManager.instance.playerData.atk;
        hp = DataManager.instance.playerData.hp;
        Level = DataManager.instance.playerData.level;
        gold = DataManager.instance.playerData.gold;
    }
}
