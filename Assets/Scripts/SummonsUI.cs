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
        // 아이템 가중치로 내림차순 정렬 20, 20, 20 ... 0.2
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
        float pivot = Random.Range(0, TOTAL_ITEM_WEIGHT); // 가중치 총합이 100
        float nowPivot = 0;
        foreach (ItemInfo item in items)
        {
            nowPivot += item.itemWeight; // 내림차순 순서대로 값을 더하고
            if (nowPivot >= pivot) // 더해진 값이 pivot보다 크거나 같다면
                return item;
        }
        return null; // 뽑히지않았을 경우 null
    }

    public void SetItems(ItemInfo[] itemArr) // 소환으로 나오게 될 아이템 세팅
    {        
        for (int i = 0; i < SUMMONS_COUNT; i++)
        {
            getItems[i] = RandomPeek();            
        }
    }      

    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave가 false라면, 즉 최초획득 아이템이라면
            itemInfo.isHave = true; // true로 바꾸고, 어차피 SetItem은 장비창을 켰을 때 실행되니까        
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
