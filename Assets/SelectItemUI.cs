using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemUI : MonoBehaviour
{
    ItemInfo itemInfo;
    ItemSlot itemSlot;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI atkRateText;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    [SerializeField] TextMeshProUGUI itemCountText;
    [SerializeField] Button equipBtn;
    const float WEAPON_UPGRADE_ATK_RATE = 0.3f;
    const float WEAPON_UPGRADE_GOLD_RATE = 0.5f;
    public void SetItem(ItemInfo setIteminfo)
    {
        itemInfo = setIteminfo;
        itemImage.sprite = DataManager.instance.itemSpriteDic[itemInfo.itemName]; // 이미지설정
        UpdatedText();
        if (GameManager.instance.equipItemInfo != itemInfo) // 다르다면
            equipBtn.interactable = true; // 장착버튼 활성화        
        else        
            equipBtn.interactable = false; // 같다면 비활성화       
    }

    public void SetItemSlot(ItemSlot setItemSlot)
    {
        itemSlot = setItemSlot;        
    }

    public void UpgradeItem() // 강화버튼 클릭 시 실행될 기능
    {
        if(GameManager.instance.gold >= itemInfo.upgradeGold) // 돈이더많다면, GameManager에 bool 함수로 만들기 시간되면 -> GameManager.instance.CanUpgrade(itemInfo.upgradeGold) bool 함수 
        {
            itemInfo.upgradeGold = (int)(WEAPON_UPGRADE_GOLD_RATE * itemInfo.upgradeGold);
            itemInfo.atkRate = (int)(WEAPON_UPGRADE_ATK_RATE * itemInfo.atkRate);
            UpdatedText();
        }
        else
        {
            Debug.Log("돈부족!");
        }
    }

    public void EquipItem() // 장착버튼 클릭 시 실행될 기능
    {
        GameManager.instance.equipItemInfo = itemInfo;
        equipBtn.interactable = false;
    }

    public void CombinationItem() // 조합버튼 클릭 시 실행될 기능
    {
        if(itemInfo.itemCount >= 5)
        {
            int nextItemCnt = 0;
            int leftItemCnt = 0;
            nextItemCnt = itemSlot.ItemCount / 5; // 5로 나눈 개수
            leftItemCnt = itemSlot.ItemCount % 5; // 5로 나눈 나머지                         
            itemSlot.ItemCount = leftItemCnt; // 현재 아이템의 개수는 leftItemCnt
            itemSlot.nextItemSlot.ItemCount += nextItemCnt;
            UpdatedText();
        }
        else
        {
            Debug.Log("조합을 위한 아이템의 수가 부족!");
        }
    }

    private void UpdatedText()
    {
        atkRateText.text = $"공격력 증가 : {itemInfo.atkRate} -> {itemInfo.atkRate * (1 + WEAPON_UPGRADE_ATK_RATE)}";
        upgradeCostText.text = $"강화 {itemInfo.upgradeGold}";
        itemCountText.text = $"{itemInfo.itemCount} / 5";
    }
}
