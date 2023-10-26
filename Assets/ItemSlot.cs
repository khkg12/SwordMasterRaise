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
    public float atkRate; // ���ݷ� ������
    public float upgradeGold; // ��ȭ �� �ʿ��� ���
    public int upgradeCount; // ���׷��̵� ��ġ
    public int itemCount;
    public float itemWeight; // �̱�Ȯ���� ���� ������ ����ġ
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
            if (!itemInfo.isHave) // ������ ���� ī��Ʈ�� ������ �����Ұ��̴� �� �� ������ ���� ���� Item�̿��ٸ� isHave�� true�� ����
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
        itemInfo = setItemInfo; // ������ �����ͷ� ����, ��������� ItemSlot�� itemInfo���� ����Ǹ� DataManager�� itemDataArr�� ���� ����ɰ���, ���� �����ϴ°� ���� �� �״�������ϸ�ɵ�
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
            selectItemUI.gameObject.SetActive(true); // â����
            selectItemUI.SetItem(itemInfo);
            selectItemUI.SetItemSlot(this);
        }        
    }
}
