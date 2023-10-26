using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;

public class SummonsUI : MonoBehaviour, IPointerClickHandler
{
    const int SUMMONS_COUNT = 10;
    [SerializeField] Image[] itemSprites;
    ItemInfo[] items = new ItemInfo[SUMMONS_COUNT];
    bool isSummonsEnd;
    private void OnEnable()
    {
        isSummonsEnd = false;
        StartCoroutine(SummonsCo());     
    }

    IEnumerator SummonsCo()
    {                
        for(int i = 0; i < SUMMONS_COUNT; i++)
        {
            GetItem(items[i]);
            itemSprites[i].sprite = DataManager.instance.itemSpriteDic[items[i].itemName];
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        isSummonsEnd = true;
    }

    public void SetItems() 
    {
        for(int i = 0; i < SUMMONS_COUNT; i++)
        {
            items[i] = DataManager.instance.itemDataArr[i];
        }
    }

    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave�� false���, �� ����ȹ�� �������̶��
        {
            itemInfo.isHave = true; // true�� �ٲٰ�, ������ SetItem�� ���â�� ���� �� ����Ǵϱ�
        }
        DataManager.instance.itemDic[itemInfo.itemName]++; // ��ųʸ��� �����߰�        
    }

    public void DisableItemImage(bool isEnabled)
    {
        foreach (Image item in itemSprites)
            item.gameObject.SetActive(isEnabled);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSummonsEnd)
        {
            gameObject.SetActive(false);
            DisableItemImage(false);
            items = null;
        }            
    }        
}
