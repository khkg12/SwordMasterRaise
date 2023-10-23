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
    // public Slot[,] slots = new Slot[3, 5]; // 2차원 배열 초기화, 판타디에서도 사용할법하다, 하지만 2차원배열은 인스펙터창에서 드래그앤드롭 안되므로 약간의 트릭을 사용해야됨
    public VerticalSlot[] verticalSlots = new VerticalSlot[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                verticalSlots[i].slots[j].ownerInven = this; // slot의 주인, owner를 세팅
            }
        }
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (verticalSlots[i].slots[j].item == null) // 인벤토리의 빈공간 찾아서
                {
                    verticalSlots[i].slots[j].SetItem(item); // 넣기
                    // 만약 개수가 쌓여야하는 경우에 딕셔너리를 사용해서 넣어보기
                    return;
                }
            }
        }
    }
}
