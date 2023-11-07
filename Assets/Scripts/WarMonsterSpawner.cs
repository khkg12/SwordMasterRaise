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
            GameObject mon = PoolManager.instance.objectPoolDic[warMonsterPrefab.name].PopMonsterObj(pos, Quaternion.identity); // pop�� �� monster �� �÷�����. Ȱ��ȭ�� �� �ϸ� ó�� Ǯcreate�� ���� �����ö󰡱� ����                            
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
            yield return new WaitUntil(() => GameManager.instance.MonsterCount == 0); // ���ͼ��� 0�̸� ����ǵ�������
            monsterCount = (int)(MONSTER_COUNT_RATE * monsterCount); // ���� �� ��������
            nowWave++;
        }                
    }
}
