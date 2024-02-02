using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{        
    public List<Skill> skillList; // ��罺ų ����ִ� ��ų����Ʈ
    public List<GameObject> soldierPrefabList; // ������ ������ ����ִ� ����Ʈ
    public GameObject soldierObj; // ���� �� ��ȯ�� ���� ������Ʈ
    public Skill[] playerSkillList; // �÷��̾ ��Ʋ �� ����� ��ų�迭
    public ItemInfo equipItemInfo; // �÷��̾ ������ ������, ������Ƽ�� ó���ؾ��Ҽ���, �����������Ϸ��� DataManager�� �������־���ϳ�?    

    public int MonsterCount
    {
        get => monsterCount;
        set
        {
            monsterCount = value;
            UIManager.instance.UpdateMonsterCount(monsterCount);
        }
    }
    private int monsterCount;    

    public int SelectSoldierId
    {
        get => selectSoldierId;
        set
        {
            selectSoldierId = value;
            soldierObj = soldierPrefabList[selectSoldierId];
            // equipSoldierIdList.Add(selectSoldierId);
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

    public int GetWarReward()
    {
        int badge = 10;
        for(int i = 0; i < WarMonsterSpawner.nowWave; i++)
        {
            badge = (int)(badge * 1.5f);
        }
        WarMonsterSpawner.nowWave = 0;
        return badge;
    }

    public void GameLose()
    {
        // IsGameStop = true;
        PauseManager.instance.IsPaused = true;
        if (SceneManager.GetActiveScene().name == "War")
        {
            DataManager.instance.Badge += GetWarReward();            
        }            
        UIManager.instance.ShowDefeatUI();        
    }   
}
