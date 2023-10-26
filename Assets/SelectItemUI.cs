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
        itemImage.sprite = DataManager.instance.itemSpriteDic[itemInfo.itemName]; // �̹�������
        UpdatedText();
        if (GameManager.instance.equipItemInfo != itemInfo) // �ٸ��ٸ�
            equipBtn.interactable = true; // ������ư Ȱ��ȭ        
        else        
            equipBtn.interactable = false; // ���ٸ� ��Ȱ��ȭ       
    }

    public void SetItemSlot(ItemSlot setItemSlot)
    {
        itemSlot = setItemSlot;        
    }

    public void UpgradeItem() // ��ȭ��ư Ŭ�� �� ����� ���
    {
        if(GameManager.instance.gold >= itemInfo.upgradeGold) // ���̴����ٸ�, GameManager�� bool �Լ��� ����� �ð��Ǹ� -> GameManager.instance.CanUpgrade(itemInfo.upgradeGold) bool �Լ� 
        {
            itemInfo.upgradeGold = (int)(WEAPON_UPGRADE_GOLD_RATE * itemInfo.upgradeGold);
            itemInfo.atkRate = (int)(WEAPON_UPGRADE_ATK_RATE * itemInfo.atkRate);
            UpdatedText();
        }
        else
        {
            Debug.Log("������!");
        }
    }

    public void EquipItem() // ������ư Ŭ�� �� ����� ���
    {
        GameManager.instance.equipItemInfo = itemInfo;
        equipBtn.interactable = false;
    }

    public void CombinationItem() // ���չ�ư Ŭ�� �� ����� ���
    {
        if(itemInfo.itemCount >= 5)
        {
            int nextItemCnt = 0;
            int leftItemCnt = 0;
            nextItemCnt = itemSlot.ItemCount / 5; // 5�� ���� ����
            leftItemCnt = itemSlot.ItemCount % 5; // 5�� ���� ������                         
            itemSlot.ItemCount = leftItemCnt; // ���� �������� ������ leftItemCnt
            itemSlot.nextItemSlot.ItemCount += nextItemCnt;
            UpdatedText();
        }
        else
        {
            Debug.Log("������ ���� �������� ���� ����!");
        }
    }

    private void UpdatedText()
    {
        atkRateText.text = $"���ݷ� ���� : {itemInfo.atkRate} -> {itemInfo.atkRate * (1 + WEAPON_UPGRADE_ATK_RATE)}";
        upgradeCostText.text = $"��ȭ {itemInfo.upgradeGold}";
        itemCountText.text = $"{itemInfo.itemCount} / 5";
    }
}
