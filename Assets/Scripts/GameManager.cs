using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{        
    public List<Skill> skillList; // ��罺ų ����ִ� ��ų����Ʈ
    public List<GameObject> soldierPrefabList; // ������ ������ ����ִ� ����Ʈ
    public GameObject soldierObj; // ���� �� ��ȯ�� ���� ������Ʈ
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

    public int SelectSoldierId
    {
        get => selectSoldierId;
        set
        {
            selectSoldierId = value;
            soldierObj = soldierPrefabList[selectSoldierId];
        }
    }
    private int selectSoldierId;

    public bool IsGameStop
    {
        get => isGameStop;
        set
        {
            isGameStop = value;
            if (isGameStop)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;            
        }
    }
    private bool isGameStop;

    public void UpgradeAwaken()
    {
        DataManager.instance.playerData.awakeLevel++;
    }  

    new void Awake()
    {
        base.Awake();        
    }

    private void OnApplicationQuit()
    {
        Debug.Log("sada");
        DataManager.instance.SaveItemData();
        DataManager.instance.SavePlayerData();
        DataManager.instance.SaveSoldierData();
    }
}
