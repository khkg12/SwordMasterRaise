using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //DataManager.instance.SaveItemData();
        //DataManager.instance.SavePlayerData();
        //DataManager.instance.SaveSoldierData();
        GameManager.instance.IsGameStop = false;
        switch (sceneName)
        {
            case "Battle":
                SoundManager.instance.ChangeBgmScene = BgmScene.Battle;
                break;
            case "Main":
                SoundManager.instance.ChangeBgmScene = BgmScene.Main;
                break;            
        }        
        SceneManager.LoadScene(sceneName);        
    }  
    
    public void StartScene()
    {
        SceneManager.LoadScene("Main");
    }
}
