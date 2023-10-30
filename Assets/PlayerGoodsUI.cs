using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoodsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    // 프로퍼티로 나중에 바꾸기 

    void Update()
    {
        goldText.text = $"{DataManager.instance.playerData.gold}";   
    }
}
