using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VerticalItemSlot
{    
    public ItemSlot[] slots = new ItemSlot[4];
}

public class ItemInventoryUI : MonoBehaviour
{    
    public VerticalItemSlot[] verticalItemSlots = new VerticalItemSlot[4];

    private void OnEnable()
    {
        ItemSlotInit();
    }

    public void ItemSlotInit()
    {
        int index = 0;
        for(int i= 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {                
                verticalItemSlots[i].slots[j].ownerInven = this;
                verticalItemSlots[i].slots[j].SetItem(DataManager.instance.itemDataArr[index]);
                index++;
            }
        }
    }

    //public void GetItem(ItemInfo itemInfo)
    //{
    //    if (!itemInfo.isHave) // isHave�� false���, �� ����ȹ�� �������̶��
    //    {
    //        itemInfo.isHave = true; // true�� �ٲٰ�, ������ SetItem�� ���â�� ���� �� ����Ǵϱ�
    //    }
    //    DataManager.instance.itemDic[itemInfo.itemName]++; // ��ųʸ��� �����߰�
    //}
}
