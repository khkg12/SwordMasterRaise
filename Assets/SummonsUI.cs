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
        if (!itemInfo.isHave) // isHave�� false���, �� ����ȹ�� �������̶��
        {
            itemInfo.isHave = true; // true�� �ٲٰ�, ������ SetItem�� ���â�� ���� �� ����Ǵϱ�
        }
        DataManager.instance.itemDic[itemInfo.itemName]++; // ��ųʸ��� �����߰�        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSummonsEnd)
            gameObject.SetActive(false);
    }
}
