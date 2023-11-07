using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] TextMeshProUGUI rewardText;
    int maxIndex;

    private void Awake()
    {
        maxIndex = DataManager.instance.stageDataArr.Length - 1;
        StageIndex = 0;
    }

    public int StageIndex
    {
        get => stageIndex;
        set
        {
            stageIndex = value; 
            if(stageIndex == 0)
            {
                leftArrow.gameObject.SetActive(false);  
            }
            else if(stageIndex == maxIndex) // 최대스테이지면
            {
                rightArrow.gameObject.SetActive(false);
            }
            else
            {
                leftArrow.gameObject.SetActive(true);
                rightArrow.gameObject.SetActive(true);
            }            
            DataManager.instance.currentStageData = DataManager.instance.stageDataArr[stageIndex]; // 현재 스테이지를 해당인덱스의 스테이지로 변경
            UpdatedText();
        }
    } 
    private int stageIndex; 

    public void ShowNextStage()
    {
        StageIndex++;
    }

    public void ShowPrevStage()
    {
        StageIndex--;
    }

    public void UpdatedText()
    {
        stageText.text = $"{DataManager.instance.currentStageData.stageName}";
        rewardText.text = $"GOLD +{DataManager.instance.currentStageData.rewardGold}\nEXP +{DataManager.instance.currentStageData.rewardExp}";
    }
}
