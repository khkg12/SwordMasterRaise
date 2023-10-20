using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public InventoryUI ownerInven;
    public Image image;
    public GameObject emptySprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Item item = null;

    public void OnPointerClick(PointerEventData eventData) // �� �ڸ�����(���Ͽ�����Ʈ������) �����ٰ� ���� ��
    {
        Debug.Log("Ŭ��");
        if (item != null)
        {
            item.Use(ownerInven.owner);
            if(item is ConsumalbeItem)
            {
                SetItem(null);
            }
        }
    }

    // Ŭ������ �� ��ȣ�ۿ� �ؾߵȴٰ� �ؼ� ��ư���� ������������ �Ʒ� �������̽����� ����Ұ�
    public void OnPointerDown(PointerEventData eventData) // �ٵ� �̰͵� ���̸����� �ϴ°��̶� �������� slot�� Ŭ���� �ȵ�, ���� ���������ִ� UI�� RAYCAST TARGET�� �����������
    {
        Debug.Log(gameObject.name + "�ٿ�");
    }

    public void OnPointerUp(PointerEventData eventData) // eventData�� eventSystem�� ����
    {
        Debug.Log(gameObject.name + "��");
        if(eventData.pointerEnter.gameObject.TryGetComponent(out Slot targetSlot))
        {
            Item tempItem = targetSlot.item;
            targetSlot.SetItem(item);
            SetItem(tempItem);
        }
        Debug.Log(eventData.pointerEnter.gameObject.name); 
    }

    public void SetItem(Item setItem)
    {
        item = setItem;
        if(item != null)
        {
            image.sprite = item.sprite;
            nameText.text = item.name;
            priceText.text = "" + item.price;
        }
        emptySprite.SetActive(item == null);
    }
}
