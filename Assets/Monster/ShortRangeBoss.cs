using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeBoss : ShortRangeMonster
{    
    new void Update()
    {
        base.Update();
        // ���������߰�, fsm�� �ְ� �߰��ص��ɵ�?, ������ fsm�׳� �߰��Ұ�
    }

    public override void DIe()
    {
        BossMonsterSpawner.AwakeMonsterCount--;
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject);
    }
}
