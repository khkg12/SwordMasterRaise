using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoodsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI badgeText;
    // ������Ƽ�� ���߿� �ٲٱ� 

    void Update()
    {
        goldText.text = $"{DataManager.instance.Gold}";
        badgeText.text = $"{DataManager.instance.Badge}";
    }
}
