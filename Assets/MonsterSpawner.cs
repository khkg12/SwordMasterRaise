using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int id; // 스테이지 아이디    
    public int[] idArr;
    public int[] countArr;    
}

public class MonsterSpawner : MonoBehaviour
{
    StageData stageData;
    public List<GameObject> monsterList;    

    private void Start()
    {
        stageData = DataManager.instance.currentStageData;
        MonsterSpawn();
    }

    public void MonsterSpawn()
    {
        for (int i = 0; i < stageData.idArr.Length; ++i)
        {
            int index = stageData.idArr[i];
            for (int j = 0; j < stageData.countArr[i]; ++j)
            {
                Instantiate(monsterList[index], transform.position, Quaternion.identity);
            }
        }
    }
}


