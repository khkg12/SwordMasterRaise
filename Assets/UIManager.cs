using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{    
    public static UIManager instance;    
    [SerializeField] Image waveImage;
    [SerializeField] Image victoryImage;
    [SerializeField] TextMeshProUGUI victoryRewardText;
    [SerializeField] TextMeshProUGUI monsterCountText;
    [SerializeField] Image defeatImage;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void ShowWaveUI(int waveRound)
    {
        waveImage.gameObject.SetActive(true);
        waveImage.GetComponentInChildren<TextMeshProUGUI>().text = $"���̺� {waveRound + 1}�ܰ� ����!";
        StartCoroutine(DurationTimeCo(3f));
    }

    public void ShowVictoryUI(int rewardGold, int rewardExp)
    {
        victoryRewardText.text = $"+{rewardGold}gold\n+{rewardExp}exp";
        victoryImage.gameObject.SetActive(true);
    }

    public void ShowVictoryUI() // �����ε�
    {
        victoryRewardText.text = $"�����մϴ�! ����!";
        victoryImage.gameObject.SetActive(true);        
    }

    public void ShowDefeatUI()
    {
        defeatImage.gameObject.SetActive(true);
    }

    public void UpdateMonsterCount(int count)
    {
        monsterCountText.text = $"{count}";
    }

    IEnumerator DurationTimeCo(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);
        waveImage.gameObject.SetActive(false);
    }  
}
