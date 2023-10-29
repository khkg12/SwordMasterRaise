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
    [SerializeField] Image defeatImage;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void ShowWaveUI(int waveRound)
    {
        waveImage.gameObject.SetActive(true);
        waveImage.GetComponentInChildren<TextMeshProUGUI>().text = $"WAVE {waveRound + 1} START";
        StartCoroutine(DurationTimeCo(3f));
    }

    public void ShowVictoryUI(int rewardGold, int rewardExp)
    {
        victoryRewardText.text = $"+{rewardGold}gold\n+{rewardExp}exp";
        victoryImage.gameObject.SetActive(true);
    }

    public void ShowDefeatUI()
    {
        defeatImage.gameObject.SetActive(true);
    }

    IEnumerator DurationTimeCo(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);
        waveImage.gameObject.SetActive(false);
    }
    
}
