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
    const int SUMMONS_COUNT = 10;
    [SerializeField] Image[] itemSprites;
    ItemInfo[] items = new ItemInfo[SUMMONS_COUNT];
    ItemInfo[] getItems = new ItemInfo[SUMMONS_COUNT];   
    bool isSummonsEnd;

    private void Awake()
    {
        items = DataManager.instance.itemDataArr.OrderByDescending(i => i.itemWeight).ToArray(); // 아이템 가중치로 내림차순 정렬 20, 20, 20 ... 0.2
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
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        isSummonsEnd = true;
    }

    public void SetItems(ItemInfo[] itemArr) 
    {        
        for (int i = 0; i < SUMMONS_COUNT; i++)
        {
            getItems[i] = RandomPeek();            
        }
    }
   
    public ItemInfo RandomPeek()
    {
        // 가중치들의 합                        
        float pivot = Random.Range(0f, 100f); // 가중치 총합이 100
        float nowPivot = 0;        
        foreach (ItemInfo item in items)
        {
            nowPivot += item.itemWeight; // 내림차순 순서대로 값을 더하고
            if(nowPivot >= pivot) // 더해진 값이 pivot보다 크거나 같다면
            {
                Debug.Log("1"+item.itemName);
                return item;                
            }
        }
        return null; // 뽑히지않았을 경우 null
    }



    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave가 false라면, 즉 최초획득 아이템이라면
        {
            itemInfo.isHave = true; // true로 바꾸고, 어차피 SetItem은 장비창을 켰을 때 실행되니까
        }        
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
