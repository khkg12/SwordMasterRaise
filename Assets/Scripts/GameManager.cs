using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{        
    public List<Skill> skillList; // ��罺ų ����ִ� ��ų����Ʈ
    public Skill[] playerSkillList; // �÷��̾ ��Ʋ �� ����� ��ų�迭
    public ItemInfo equipItemInfo; // �÷��̾ ������ ������, ������Ƽ�� ó���ؾ��Ҽ���, �����������Ϸ��� DataManager�� �������־���ϳ�?
    public int monsterCount;

    public int BossCount
    {
        get => BossCount;
        set
        {
            bossCount = value;
            if(bossCount == 0) // ������ ����Ҵٸ�
            {
                UpgradeAwaken(); // ���������ø���                      
            }
        }
    }
    private int bossCount;

    public void UpgradeAwaken()
    {
        DataManager.instance.playerData.awakeLevel++;
    }

    new void Awake()
    {
        base.Awake();        
    }                
}
