using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeBoss : ShortRangeMonster
{    
    new void Update()
    {
        base.Update();
        // 보스패턴추가, fsm을 주고 추가해도될듯?, 보스에 fsm그냥 추가할것
    }

    public override void DIe()
    {
        BossMonsterSpawner.AwakeMonsterCount--;
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject);
    }
}
