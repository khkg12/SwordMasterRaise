using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMonsterSpawner : MonoBehaviour
{
    AwakeStageData awakestageData;
    public List<GameObject> monsterList;   
    Vector3 SpawnPos = new Vector3(0, 0, 5);

    private void Start()
    {
        GameManager.instance.monsterCount = 0;
        awakestageData = DataManager.instance.currentAwakeStageData;
        MonsterSpawn();
    }

    public void MonsterSpawn()
    {
        for (int i = 0; i < awakestageData.count; ++i)
        {            
            Instantiate(monsterList[awakestageData.bossId], SpawnPos, Quaternion.identity);
            GameManager.instance.monsterCount++;
        }
    }
}
