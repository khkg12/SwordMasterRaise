using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeBoss : ShortRangeMonster
{       
    public override void DIe()
    {
        BossMonsterSpawner.AwakeMonsterCount--;
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject);
    }
}
