using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStat : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI statText;    
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Button upgradeBtn;    
    StatInfo statInfo;

    public void SetStatus(StatInfo setStatInfo)
    {
        statInfo = setStatInfo;
        UpdateText();
    }

    public void UpgradeStatus() // 업그레이드버튼 클릭시 실행할 기능
    {
        if(DataManager.instance.Gold >= statInfo.requireGold)
        {
            DataManager.instance.Gold -= statInfo.requireGold;
            statInfo.stat += statInfo.increaseAmount;
            statInfo.requireGold = (int)(1.1f * statInfo.requireGold);
            UpdateText();
        }
        else
        {
            Debug.Log("돈부족");
        }
    }

    public void UpdateText()
    {
        statText.text = $"{statInfo.name} : {statInfo.stat} -> {statInfo.stat + statInfo.increaseAmount}";
        goldText.text = $"{statInfo.requireGold}";
    }
    
}

