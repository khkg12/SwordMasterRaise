using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item����Ʈ 
    public ItemInventoryUI itemInven;
    [SerializeField] SummonsUI summonUI;
    public void WeaponSummons()
    {
        if (DataManager.instance.Gold >= 1000)
        {
            DataManager.instance.Gold -= 1000;
            summonUI.gameObject.SetActive(true);
        }
        else
            Debug.Log("������");
    }    

}
