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
    [SerializeField] SelectItemUI selectItemUI;

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
                verticalItemSlots[i].slots[j].selectItemUI = selectItemUI;                
                if(i != 3 && j == 3) // 0,3 1,3 2,3 .. 3,3 �� �ȵ�
                {
                    verticalItemSlots[i].slots[j].nextItemSlot = verticalItemSlots[i + 1].slots[0];                    
                }
                else if(j != 3)// 0,0 0,1 0,2 1,0 1,1 1,2 2,0 2,1 2,2 3,0 3,1 3,2
                {
                    verticalItemSlots[i].slots[j].nextItemSlot = verticalItemSlots[i].slots[j + 1];
                }
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
