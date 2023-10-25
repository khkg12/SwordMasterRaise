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
    //    if (!itemInfo.isHave) // isHave가 false라면, 즉 최초획득 아이템이라면
    //    {
    //        itemInfo.isHave = true; // true로 바꾸고, 어차피 SetItem은 장비창을 켰을 때 실행되니까
    //    }
    //    DataManager.instance.itemDic[itemInfo.itemName]++; // 딕셔너리에 개수추가
    //}
}
