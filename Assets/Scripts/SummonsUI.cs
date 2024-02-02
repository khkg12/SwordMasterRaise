using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEditorInternal.Profiling.Memory.Experimental;
using static UnityEditor.Progress;

public class SummonsUI : MonoBehaviour, IPointerClickHandler
{
    readonly static int SUMMONS_COUNT = 10;
    readonly static int TOTAL_ITEM_WEIGHT = 100;
    [SerializeField] Image[] itemSprites;
    ItemInfo[] items;
    ItemInfo[] getItems = new ItemInfo[SUMMONS_COUNT];
    WaitForSeconds waitForSecond;
    bool isSummonsEnd;

    private void Awake()
    {
        waitForSecond = new WaitForSeconds(0.5f);
        items = DataManager.instance.itemDataArr.OrderByDescending(i => i.itemWeight).ToArray();        
        // ������ ����ġ�� �������� ���� 20, 20, 20 ... 0.2
    }

    private void OnEnable()
    {
        isSummonsEnd = false;
        SetItems(DataManager.instance.itemDataArr);
        StartCoroutine(SummonsCo());     
    }

    IEnumerator SummonsCo()
    {                
        for(int i = 0; i < SUMMONS_COUNT; i++)
        {
            GetItem(getItems[i]);            
            itemSprites[i].sprite = DataManager.instance.itemSpriteDic[getItems[i].itemName];
            itemSprites[i].gameObject.SetActive(true);             
            yield return waitForSecond;
        }
        yield return waitForSecond;
        isSummonsEnd = true;
    }

    public ItemInfo RandomPeek()
    {                             
        float pivot = Random.Range(0, TOTAL_ITEM_WEIGHT); // ����ġ ������ 100
        float nowPivot = 0;
        foreach (ItemInfo item in items)
        {
            nowPivot += item.itemWeight; // �������� ������� ���� ���ϰ�
            if (nowPivot >= pivot) // ������ ���� pivot���� ũ�ų� ���ٸ�
                return item;
        }
        return null; // �������ʾ��� ��� null
    }

    public void SetItems(ItemInfo[] itemArr) // ��ȯ���� ������ �� ������ ����
    {        
        for (int i = 0; i < SUMMONS_COUNT; i++)
        {
            getItems[i] = RandomPeek();            
        }
    }      

    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave�� false���, �� ����ȹ�� �������̶��
            itemInfo.isHave = true; // true�� �ٲٰ�, ������ SetItem�� ���â�� ���� �� ����Ǵϱ�        
        itemInfo.itemCount++;
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
        }            
    }        
}
