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
    public float atkRate; // ���ݷ� ������
    public float upgradeGold; // ��ȭ �� �ʿ��� ���
    public int upgradeCount; // ���׷��̵� ��ġ
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

    // ��ȭ�Ҷ����� 

    // json�� ����Ǿ���� �͵�
    // isHave ������ �ִ���
    // ��ųʸ� �ش� �������� ����
    // �ش�������� ���ݷ� ��ġ �ۼ�Ʈ
    // �ش�������� ��ȭ��ġ
    // �ش�������� ��ȭ�ʿ���

    // �ó����� 1.
    // �����͸Ŵ����� �ش��ϴ� ���������� json�� ������ �ְ� ó���� ���ӸŴ����� �����͸Ŵ����� ������ȭ�Ѵ�.
    // �����͸Ŵ����� ���������� ����Ʈ�� ���� �ش��ϴ� ������ Ŭ������ ����Ʈ ����.
    // �����͸Ŵ����� �� ������ �̸��� ������ key value �� �ϴ� ��ųʸ��� �����ϰ� ���������� json���� ������ �ִ�.
    // ������ �ֽ�ȭ�� �����͸Ŵ����� ���������� ����Ʈ�� �ֽ�ȭ�Ѵ�.
    // ���â�� ��� ������ �����͸Ŵ����� ���������� ����Ʈ�� �����Ѵ�. 

    public void SetItem(ItemInfo setItemInfo)
    {
        itemInfo = setItemInfo; // ������ �����ͷ� ����, ��������� ItemSlot�� itemInfo���� ����Ǹ� DataManager�� itemDataArr�� ���� ����ɰ���, ���� �����ϴ°� ���� �� �״�������ϸ�ɵ�
        if (itemInfo.isHave)
        {
            rockImage.gameObject.SetActive(!itemInfo.isHave);
            countText.text = $"{itemInfo.itemCount} / 5";
            enabled = itemInfo.isHave;
        }            
    }

    //public void SetItem(ItemInfo itemInfo)
    //{
    //    if (itemInfo.isHave) // �̹� �ִٸ�
    //    {
    //        // ��ųʸ��� ������ �ø���
    //    }
    //    else
    //    {
    //        // ��ü�ϳ� ����
    //    }
    //}

}
