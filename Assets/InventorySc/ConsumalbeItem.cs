using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumalbeItem : Item
{
    public int value;
    public override void Use(PlayerSc player)
    {
        player.hp += 10; // 프로퍼티로 제약, 변수에 직접접근하면 최대 hp를 넘어가거나 0이하가 될경우 처리하는 게 불가
        Destroy(gameObject);
    }
}
