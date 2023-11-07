using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class BossMonsterSpawner : MonoBehaviour
{
    public static int AwakeMonsterCount
    {
        get => awakenMonsterCount;
        set
        {
            awakenMonsterCount = value;
            if (awakenMonsterCount == 0) // 보스 다잡았다면
            {
                GameManager.instance.UpgradeAwaken(); // 각성레벨올리고
                UIManager.instance.ShowVictoryUI();
            }
        }
    }
    private static int awakenMonsterCount;
    AwakeStageData awakestageData;
    public List<GameObject> monsterList;   
    Vector3 SpawnPos = new Vector3(0, 0, 5);

    private void Start()
    {
        awakenMonsterCount = 0;
        awakestageData = DataManager.instance.currentAwakeStageData;
        PoolManager.instance.InitAwakeMonsterPool(awakestageData, monsterList);
        MonsterSpawn();
    }

    public void MonsterSpawn()
    {
        for (int i = 0; i < awakestageData.count; ++i)
        {                        
            PoolManager.instance.objectPoolDic[monsterList[awakestageData.bossId].name].PopMonsterObj(SpawnPos, Quaternion.identity);
            AwakeMonsterCount++;
        }
    }
}
