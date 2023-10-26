using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData // 
{
    public Sprite sprite;
    public ItemInfo itemInfo;
}

[System.Serializable]
public class ItemInfo
{    
    public string itemName;
    public bool isHave;
    public float atkRate; // 공격력 증가율
    public float upgradeGold; // 강화 시 필요한 골드
    public int upgradeCount; // 업그레이드 수치
    public int itemCount;
    public float itemWeight; // 뽑기확률을 위한 아이템 가중치
}

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemImage;
    [SerializeField] Image rockImage;    
    [SerializeField] TextMeshProUGUI countText;
    private ItemInfo itemInfo;
    public ItemInventoryUI ownerInven;
    public SelectItemUI selectItemUI;
    public ItemSlot nextItemSlot;
    //public bool IsHave
    //{
    //    get => itemInfo.isHave;
    //    set
    //    {
    //        itemInfo.isHave = value;
    //        if (itemInfo.isHave)
    //        {
    //            countText.text = $"{itemInfo.itemCount} / 5";
    //            rockImage.gameObject.SetActive(!itemInfo.isHave);                
    //            enabled = itemInfo.isHave;
    //        }
    //    }
    //}
    public int ItemCount
    {
        get => itemInfo.itemCount;
        set
        {
            itemInfo.itemCount = value;
            if (!itemInfo.isHave) // 조합을 통한 카운트의 개수가 증가할것이니 이 때 가지고 있지 못한 Item이였다면 isHave를 true로 변경
            {
                itemInfo.isHave = true;
                itemImage.sprite = DataManager.instance.itemSpriteDic[itemInfo.itemName];
                rockImage.gameObject.SetActive(!itemInfo.isHave);
                enabled = itemInfo.isHave;
            }
            countText.text = $"{itemInfo.itemCount} / 5";
        }
    }
    
    public void SetItem(ItemInfo setItemInfo)
    {
        itemInfo = setItemInfo; // 아이템 데이터로 세팅, 얕은복사로 ItemSlot의 itemInfo값이 변경되면 DataManager의 itemDataArr의 값도 변경될것임, 따라서 저장하는건 꺼질 때 그대로저장하면될듯
        if (itemInfo.isHave)
        {
            itemImage.sprite = DataManager.instance.itemSpriteDic[itemInfo.itemName];
            rockImage.gameObject.SetActive(!itemInfo.isHave);
            countText.text = $"{itemInfo.itemCount} / 5";
            enabled = itemInfo.isHave;
        }            
    }    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(itemInfo.isHave)
        {
            selectItemUI.gameObject.SetActive(true); // 창띄우고
            selectItemUI.SetItem(itemInfo);
            selectItemUI.SetItemSlot(this);
        }        
    }
}
