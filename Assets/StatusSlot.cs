using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fieldName;
    [SerializeField] TextMeshProUGUI fieldValue;
    
    public void SetSlot(string name, object value)
    {
        fieldName.text = name;
        fieldValue.text = value.ToString(); 
    }
    
}
