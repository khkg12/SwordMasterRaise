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
        items = DataManager.instance.itemDataArr.OrderByDescending(i => i.itemWeight).ToArray(); // ������ ����ġ�� �������� ���� 20, 20, 20 ... 0.2
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
        // ����ġ���� ��                        
        float pivot = Random.Range(0f, 100f); // ����ġ ������ 100
        float nowPivot = 0;        
        foreach (ItemInfo item in items)
        {
            nowPivot += item.itemWeight; // �������� ������� ���� ���ϰ�
            if(nowPivot >= pivot) // ������ ���� pivot���� ũ�ų� ���ٸ�
            {
                Debug.Log("1"+item.itemName);
                return item;                
            }
        }
        return null; // �������ʾ��� ��� null
    }



    public void GetItem(ItemInfo itemInfo)
    {
        if (!itemInfo.isHave) // isHave�� false���, �� ����ȹ�� �������̶��
        {
            itemInfo.isHave = true; // true�� �ٲٰ�, ������ SetItem�� ���â�� ���� �� ����Ǵϱ�
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
