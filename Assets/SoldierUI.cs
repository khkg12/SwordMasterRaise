using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUI : MonoBehaviour
{
    [SerializeField] SoldierSlot[] soldierSlots;    
    void Start()
    {
        SlotInit();
    }

    public void SlotInit()
    {
        for (int i = 0; i < soldierSlots.Length; i++)
        {
            soldierSlots[i].SetSoldier(DataManager.instance.soldierDataArr[i]);
            soldierSlots[i].owner = this;
            if (soldierSlots[i].IsHave && soldierSlots[i].id == GameManager.instance.SelectSoldierId)
                soldierSlots[i].IsEquip = true;
        }
    }

    public void DisableEquip()
    {
        foreach(SoldierSlot slot in soldierSlots)
        {
            slot.IsEquip = false;
        }        
    }
}
