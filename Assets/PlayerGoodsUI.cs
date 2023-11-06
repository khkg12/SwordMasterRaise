using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoodsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI badgeText;
    [SerializeField] TextMeshProUGUI jewelText;
    // ������Ƽ�� ���߿� �ٲٱ� 

    void Update()
    {
        goldText.text = $"{DataManager.instance.Gold}";
        badgeText.text = $"{DataManager.instance.Badge}";
        jewelText.text = $"{DataManager.instance.Jewel}";
    }
}

