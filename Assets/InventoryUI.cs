using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class VerticalSlot
{
    public Slot[] slots = new Slot[5];
}

public class InventoryUI : MonoBehaviour
{
    public PlayerSc owner;
    // public Slot[,] slots = new Slot[3, 5]; // 2���� �迭 �ʱ�ȭ, ��Ÿ�𿡼��� ����ҹ��ϴ�, ������ 2�����迭�� �ν�����â���� �巡�׾ص�� �ȵǹǷ� �ణ�� Ʈ���� ����ؾߵ�
    public VerticalSlot[] verticalSlots = new VerticalSlot[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                verticalSlots[i].slots[j].ownerInven = this; // slot�� ����, owner�� ����
            }
        }
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (verticalSlots[i].slots[j].item == null) // �κ��丮�� ����� ã�Ƽ�
                {
                    verticalSlots[i].slots[j].SetItem(item); // �ֱ�
                    // ���� ������ �׿����ϴ� ��쿡 ��ųʸ��� ����ؼ� �־��
                    return;
                }
            }
        }
    }
}
