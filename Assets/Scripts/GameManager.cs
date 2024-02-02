using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{        
    public List<Skill> skillList; // 모든스킬 담고있는 스킬리스트
    public List<GameObject> soldierPrefabList; // 모든솔져 프리팹 담고있는 리스트
    public GameObject soldierObj; // 전투 시 소환할 솔져 오브젝트
    public Skill[] playerSkillList; // 플레이어가 배틀 시 사용할 스킬배열
    public ItemInfo equipItemInfo; // 플레이어가 장착할 아이템, 프로퍼티로 처리해야할수도, 데이터저장하려면 DataManager가 가지고있어야하나?    

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
