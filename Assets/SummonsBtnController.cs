using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item리스트 
    public ItemInventoryUI itemInven;
    [SerializeField] Image summonUI;
    public void Summons()
    {
        // 확률 계산후 어떤 아이템이 나올지 선정
        // item에 담기
        // 나중에 코루틴으로 바꾸고 개수추가하기
        summonUI.gameObject.SetActive(true);
        // GetItem(DataManager.instance.itemDataArr[0]); // 임시설정
    }    
}
