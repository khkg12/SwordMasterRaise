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
        GameManager.instance.monsterCount = 0; // ĳ���Ͱ� �׾��� �� �ʱ�ȭ�ϴ�
        stageData = DataManager.instance.currentStageData;
        PoolManager.instance.InitMonsterPool(stageData, monsterList); // �ش罺�������� ������ ������ Ǯ ����
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
                PoolManager.instance.objectPoolDic[monsterList[index].name].PopMonsterObj(pos, Quaternion.identity); // pop�� �� monster �� �÷�����. Ȱ��ȭ�� �� �ϸ� ó�� Ǯcreate�� ���� �����ö󰡱� ����                
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
            yield return new WaitUntil(()=>GameManager.instance.monsterCount == 0); // ���ͼ��� 0�̸� ����ǵ�������
            nowWave++;
        }        
        yield return new WaitForSeconds(1f); // ��� ���̺� �ݺ��� ����Ǹ�
        // �¸� UI ���, ��ư Ŭ�� �� �������� 
        UIManager.instance.ShowVictoryUI(stageData.rewardGold, stageData.rewardExp);
        DataManager.instance.Exp += stageData.rewardExp;
        DataManager.instance.Gold += stageData.rewardGold;  
    }
}


