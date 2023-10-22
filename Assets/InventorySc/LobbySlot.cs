using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);    
    }   
}
