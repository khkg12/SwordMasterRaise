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

    public void OnPointerClick(PointerEventData eventData) // 그 자리에서(동일오브젝트에서만) 눌렀다가 뗐을 때
    {
        Debug.Log("클릭");
        if (item != null)
        {
            item.Use(ownerInven.owner);
            if(item is ConsumalbeItem)
            {
                SetItem(null);
            }
        }
    }

    // 클릭했을 때 항호작용 해야된다고 해서 버튼으로 구현하지말고 아래 인터페이스들을 사용할것
    public void OnPointerDown(PointerEventData eventData) // 근데 이것도 레이를쏴서 하는것이라 가려지면 slot이 클릭이 안됨, 따라서 가려질수있는 UI가 RAYCAST TARGET을 해제해줘야함
    {
        Debug.Log(gameObject.name + "다운");
    }

    public void OnPointerUp(PointerEventData eventData) // eventData는 eventSystem의 정보
    {
        Debug.Log(gameObject.name + "업");
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
