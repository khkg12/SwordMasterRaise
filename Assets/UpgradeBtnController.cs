using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum StatusTag
{
    HP,
    ATK,
    SPEED,
    CRITICAL,
}

public class UpgradeBtnController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] TextMeshProUGUI goldText;
    int increaseAmount;    
    [SerializeField] StatusTag statusTag;
    Button upgradeBtn;

    private void Start()
    {        
       upgradeBtn = GetComponent<Button>();
       StatusInfoInit(statusTag);        
    }

    public void UpgradeStatus(ref int upgradeStatus, ref int upgradeGold)
    {
        if (GameManager.instance.gold >= upgradeGold)
        {
            upgradeStatus += increaseAmount;         
            upgradeGold = (int)(1.1f * upgradeGold);
            statText.text = $"{upgradeStatus} -> {upgradeStatus + increaseAmount}";
            goldText.text = $"{upgradeGold}";
        }
        else
        {
            Debug.Log("돈이 부족합니다");
        }
    }    

    void StatusInfoInit(StatusTag statusTag)
    {
        switch (statusTag)
        {
            case StatusTag.HP:
                increaseAmount = 30;
                statText.text = $"{GameManager.instance.hp} -> {GameManager.instance.hp + increaseAmount}";
                goldText.text = $"{GameManager.instance.upgradeHpGold}";                
                upgradeBtn.onClick.AddListener(() => UpgradeStatus(ref GameManager.instance.hp, ref GameManager.instance.upgradeHpGold));
                break;
            case StatusTag.ATK:
                increaseAmount = 5;
                statText.text = $"{GameManager.instance.atk} -> {GameManager.instance.atk + increaseAmount}";
                goldText.text = $"{GameManager.instance.upgradeAtkGold}";
                upgradeBtn.onClick.AddListener(() => UpgradeStatus(ref GameManager.instance.atk, ref GameManager.instance.upgradeAtkGold));
                break;
            case StatusTag.SPEED:
                //increaseAmount = 1;
                //statText.text = $"{GameManager.instance. } -> {GameManager.instance.hp + increaseAmount}";
                break;
        }
    }    
}
