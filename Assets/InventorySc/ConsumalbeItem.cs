using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumalbeItem : Item
{
    public int value;
    public override void Use(PlayerSc player)
    {
        player.hp += 10; // ������Ƽ�� ����, ������ ���������ϸ� �ִ� hp�� �Ѿ�ų� 0���ϰ� �ɰ�� ó���ϴ� �� �Ұ�
        Destroy(gameObject);
    }
}
