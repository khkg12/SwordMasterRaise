using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class ItemData // 
{
    public Sprite sprite;
    public ItemInfo itemInfo;
}

[System.Serializable]
public class ItemInfo
{
    public Sprite sprite;
    public string itemName;
    public bool isHave;
    public float atkRate; // 공격력 증가율
    public float upgradeGold; // 강화 시 필요한 골드
    public int upgradeCount; // 업그레이드 수치
    public int itemCount;
}

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image rockImage;
    [SerializeField] TextMeshProUGUI countText;
    public ItemInfo itemInfo;
    public ItemInventoryUI ownerInven;   
    public bool IsHave
    {
        get => itemInfo.isHave;
        set
        {
            itemInfo.isHave = value;
            rockImage.gameObject.SetActive(!itemInfo.isHave);
            countText.text = $"{itemInfo.itemCount} / 5";
            enabled = itemInfo.isHave;
        }
    }    

    // itemCount = itemDic[itemName];

    // 강화할때마다 

    // json에 저장되어야할 것들
    // isHave 가지고 있는지
    // 딕셔너리 해당 아이템의 개수
    // 해당아이템의 공격력 수치 퍼센트
    // 해당아이템의 강화수치
    // 해당아이템의 강화필요골드

    // 시나리오 1.
    // 데이터매니저에 해당하는 아이템정보 json을 가지고 있고 처음에 게임매니저나 데이터매니저에 역직렬화한다.
    // 데이터매니저의 아이템정보 리스트는 위에 해당하는 정보의 클래스로 리스트 구성.
    // 데이터매니저엔 또 무기의 이름과 개수를 key value 로 하는 딕셔너리가 존재하고 마찬가지로 json으로 가지고 있다.
    // 정보의 최신화는 데이터매니저의 아이템정보 리스트를 최신화한다.
    // 장비창을 띄울 때마다 데이터매니저의 아이템정보 리스트를 참조한다. 

    public void SetItem(ItemInfo setItemInfo)
    {
        itemInfo = setItemInfo; // 아이템 데이터로 세팅, 얕은복사로 ItemSlot의 itemInfo값이 변경되면 DataManager의 itemDataArr의 값도 변경될것임, 따라서 저장하는건 꺼질 때 그대로저장하면될듯
        if (itemInfo.isHave)
        {
            rockImage.gameObject.SetActive(!itemInfo.isHave);
            countText.text = $"{itemInfo.itemCount} / 5";
            enabled = itemInfo.isHave;
        }            
    }

    //public void SetItem(ItemInfo itemInfo)
    //{
    //    if (itemInfo.isHave) // 이미 있다면
    //    {
    //        // 딕셔너리에 개수만 올리고
    //    }
    //    else
    //    {
    //        // 객체하나 생성
    //    }
    //}

}
