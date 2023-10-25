using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SummonsUI : MonoBehaviour, IPointerClickHandler
{
    const int SUMMONS_COUNT = 10;
    bool isSummonsEnd;
    private void OnEnable()
    {
        isSummonsEnd = false;
        StartCoroutine(SummonsCo());     
    }

    IEnumerator SummonsCo()
    {                
        for(int i = 0; i <  SUMMONS_COUNT; i++)
        {
            GetItem(DataManager.instance.itemDataArr[i]);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        isSummonsEnd = true;
    }

    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave가 false라면, 즉 최초획득 아이템이라면
        {
            itemInfo.isHave = true; // true로 바꾸고, 어차피 SetItem은 장비창을 켰을 때 실행되니까
        }
        DataManager.instance.itemDic[itemInfo.itemName]++; // 딕셔너리에 개수추가        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSummonsEnd)
            gameObject.SetActive(false);
    }
}
