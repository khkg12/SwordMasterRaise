using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{    
    StageData stageData;
    public List<GameObject> monsterList;    
    const int WAVE_COUNT = 3;
    int nowWave = 0;    

    private void Start()
    {
        GameManager.instance.monsterCount = 0; // 캐릭터가 죽었을 때 초기화하던
        stageData = DataManager.instance.currentStageData;
        PoolManager.instance.InitMonsterPool(stageData, monsterList); // 해당스테이지에 나오는 몬스터의 풀 세팅
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
                PoolManager.instance.objectPoolDic[monsterList[index].name].PopMonsterObj(pos, Quaternion.identity); // pop할 때 monster 수 올려야함. 활성화할 때 하면 처음 풀create할 때도 수가올라가기 때문                
            }
        }
    }

    IEnumerator SpawnCo()
    {
        while (nowWave < WAVE_COUNT)
        {
            UIManager.instance.ShowWaveUI(nowWave);
            yield return new WaitForSeconds(3f);                 
            MonsterSpawn();            
            yield return new WaitUntil(()=>GameManager.instance.monsterCount == 0); // 몬스터수가 0이면 제어권돌려받음
            nowWave++;
        }        
        yield return new WaitForSeconds(1f); // 모든 웨이브 반복이 종료되면
        // 승리 UI 출력, 버튼 클릭 시 메인으로 
        UIManager.instance.ShowVictoryUI(stageData.rewardGold, stageData.rewardExp);
        DataManager.instance.Exp += stageData.rewardExp;
        DataManager.instance.Gold += stageData.rewardGold;  
    }
}


