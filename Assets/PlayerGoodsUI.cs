using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoodsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    // ������Ƽ�� ���߿� �ٲٱ� 

    void Update()
    {
        goldText.text = $"{DataManager.instance.playerData.gold}";   
    }
}
