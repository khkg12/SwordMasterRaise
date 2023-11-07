using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item리스트 
    public ItemInventoryUI itemInven;
    const int REQUIRED_JEWEL = 1000;
    [SerializeField] SummonsUI summonUI;
    public void WeaponSummons()
    {
        if (DataManager.instance.Jewel >= REQUIRED_JEWEL)
        {
            DataManager.instance.Jewel -= REQUIRED_JEWEL;
            summonUI.gameObject.SetActive(true);
        }
        else
            Debug.Log("돈부족");
    }    

}
