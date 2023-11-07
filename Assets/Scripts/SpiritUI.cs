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
            spiritData.rate += 0.01f; // �̰� Ŭ�������� ������� �ö󰡴� �� ��ġ��
            spiritData.requireGold *= 2;
            UpdatedText();
        }
        else
            Debug.Log("������");
    }

    public void UpdatedText()
    {
        rateText.text = $"�ʴ�ü��ȸ�� :{spiritData.rate * 100}%";
        goldText.text = $"�ʿ��� :{spiritData.requireGold}";
    }
}
