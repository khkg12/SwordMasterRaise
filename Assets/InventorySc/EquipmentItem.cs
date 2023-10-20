using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public int additionalAtk;
    bool isEquip = false;
    public override void Use(PlayerSc player)
    {
        if (isEquip == false)
        {
            if(player.weapon != null)
            {
                player.weapon.Use(player); // 사용한번 더해주면 기존의 그 아이템의 Use의 else가 실행되므로 해제됨
            }
            player.weapon = this;
        }
        else
        {
            player.weapon = null;
        }
        isEquip = !isEquip;         
    }
}
