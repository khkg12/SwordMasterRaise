using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item리스트 
    public ItemInventoryUI itemInven;
    [SerializeField] SummonsUI summonUI;
    public void WeaponSummons()
    {
        // 확률 계산후 어떤 아이템이 나올지 선정
        // item에 담기
        // 나중에 코루틴으로 바꾸고 개수추가하기
        // 뽑힌 아이템 리스트 여기서 정해지고
        
        summonUI.gameObject.SetActive(true);
        // GetItem(DataManager.instance.itemDataArr[0]); // 임시설정
    }    

}
