using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    const int STAT_NUM = 3;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Image levelImage;
    [SerializeField] UpgradeStat[] statSlots = new UpgradeStat[STAT_NUM];
    void Awake()
    {
        levelText.text = $"LV {DataManager.instance.playerData.level}";
        levelImage.fillAmount = DataManager.instance.playerData.exp / DataManager.instance.playerData.maxExp;
        statSlots[0].SetStatus(DataManager.instance.playerData.hp);
        statSlots[1].SetStatus(DataManager.instance.playerData.atk);
        statSlots[2].SetStatus(DataManager.instance.playerData.speed);
    }      
}
