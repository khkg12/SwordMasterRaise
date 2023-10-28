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

// ���ȿ� ���� ������ ����ü�� ���� -> ex) hp, �ʿ���, ������
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
                // ��ų����
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

    public List<Skill> skillList; // ��罺ų ����ִ� ��ų����Ʈ
    public Skill[] playerSkillList; // �÷��̾ ��Ʋ �� ����� ��ų�迭
    public ItemInfo equipItemInfo; // �÷��̾ ������ ������, ������Ƽ�� ó���ؾ��Ҽ���
    public int monsterCount;

    new void Awake()
    {
        base.Awake();        
    }
    private void Start()
    {
        DataInit(); // ������ �ʱ�ȭ
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
