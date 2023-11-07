using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AwakeStageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageNameText;
    [SerializeField] TextMeshProUGUI explanationText;
    [SerializeField] Button awakeStageBtn;
    
    public int AwakeStageIndex
    {
        get => awakeStageIndex;
        set
        {
            awakeStageIndex = value;
            if (awakeStageIndex == 3) // 최대각성 상태라면 버튼끄
            {
                awakeStageBtn.interactable = false;
                explanationText.text = "최종각성 상태입니다!";
            }
            else
            {                
                DataManager.instance.currentAwakeStageData = DataManager.instance.awakeStageDataArr[awakeStageIndex]; // 레벨에 맞춰서 각성스테이지 정보정해줌
                UpdatedText();
            }            
        }        
    }
    private int awakeStageIndex;

    private void Awake()
    {
        AwakeStageIndex = DataManager.instance.playerData.awakeLevel;
    }  

    public void UpdatedText()
    {
        stageNameText.text = DataManager.instance.currentAwakeStageData.stageName;
        explanationText.text = DataManager.instance.currentAwakeStageData.explanation;
    }
}
