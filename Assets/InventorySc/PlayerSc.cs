using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSc : MonoBehaviour
{
    // ����Ϳ��� ��� ��ĵ� ���
    public int hp;
    public int atk;

    public InventoryUI inventoryUI;
    public GameObject InvenObj;    
    public EquipmentItem weapon = null;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item)) 
        {            
            item.gameObject.SetActive(false); 
            item.transform.SetParent(InvenObj.transform);
            inventoryUI.AddItem(item);
        }
    }    
}
