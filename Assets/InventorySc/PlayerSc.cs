using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSc : MonoBehaviour
{
    // 사냥터에서 얻는 방식도 고려
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
