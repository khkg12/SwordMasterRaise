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
    const int WAVE_COUNT = 3;
    int nowWave = 0;

    private void Start()
    {
        stageData = DataManager.instance.currentStageData;
        StartCoroutine(SpawnCo());        
    }

    public void MonsterSpawn()
    {
        for (int i = 0; i < stageData.idArr.Length; ++i)
        {
            int index = stageData.idArr[i];
            for (int j = 0; j < stageData.countArr[i]; ++j)
            {
                Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                Instantiate(monsterList[index], pos, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnCo()
    {
        while (nowWave <WAVE_COUNT)
        {
            yield return new WaitForSeconds(3);
            nowWave++;
            MonsterSpawn();
        }        
    }
}


