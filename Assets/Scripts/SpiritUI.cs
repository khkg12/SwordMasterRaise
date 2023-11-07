using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiritUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rateText;
    [SerializeField] TextMeshProUGUI goldText;
    SpiritData spiritData;
    private void Awake()
    {
        spiritData = DataManager.instance.spiritData;
        UpdatedText();
    }
    
    public void UpgradeSpirit()
    {
        if (DataManager.instance.Gold >= spiritData.requireGold)
        {
            DataManager.instance.Gold -= spiritData.requireGold;
            spiritData.rate += 0.01f; // 이거 클래스에서 기능으로 올라가는 거 합치기
            spiritData.requireGold *= 2;
            UpdatedText();
        }
        else
            Debug.Log("돈부족");
    }

    public void UpdatedText()
    {
        rateText.text = $"초당체력회복 :{spiritData.rate * 100}%";
        goldText.text = $"필요골드 :{spiritData.requireGold}";
    }
}
