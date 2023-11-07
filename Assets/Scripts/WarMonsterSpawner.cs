using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarMonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject warMonsterPrefab;
    const float MONSTER_UPGRADE_HP_RATE = 1.5f;
    const float MONSTER_UPGRADE_ATK_RATE = 1.2f;
    const float MONSTER_COUNT_RATE = 1.2f;
    public static int nowWave = 0;
    int monsterCount = 10;

    private void Start()
    {
        GameManager.instance.MonsterCount = 0;
        PoolManager.instance.InitMonsterPool(warMonsterPrefab, monsterCount);
        StartCoroutine(SpawnCo());
    }

    public void MonsterSpawn()
    {
        for (int i = 0; i < monsterCount; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            GameObject mon = PoolManager.instance.objectPoolDic[warMonsterPrefab.name].PopMonsterObj(pos, Quaternion.identity); // pop할 때 monster 수 올려야함. 활성화할 때 하면 처음 풀create할 때도 수가올라가기 때문                            
            MonsterUpgrade(mon.GetComponent<Monster>());
        }
    }

    public void MonsterUpgrade(Monster monster)
    {
        monster.Hp = (int)(MONSTER_UPGRADE_HP_RATE * monster.Hp);
        monster.MaxHp = monster.Hp;
        monster.Atk = (int)(MONSTER_UPGRADE_ATK_RATE * monster.Atk);
    }

    IEnumerator SpawnCo()
    {
        while (true)
        {
            UIManager.instance.ShowWaveUI(nowWave);
            yield return new WaitForSeconds(3f);
            MonsterSpawn();            
            yield return new WaitUntil(() => GameManager.instance.MonsterCount == 0); // 몬스터수가 0이면 제어권돌려받음
            monsterCount = (int)(MONSTER_COUNT_RATE * monsterCount); // 몬스터 수 점점증가
            nowWave++;
        }                
    }
}
