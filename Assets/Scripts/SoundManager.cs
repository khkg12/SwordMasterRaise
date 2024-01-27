using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BgmScene
{
    Main,
    Battle
}

public class SoundManager : Singleton<SoundManager> 
{
    AudioSource bgmSource;    
    public AudioClip[] bgms;   
    public BgmScene ChangeBgmScene
    {
        get => bgmScene;
        set
        {
            bgmScene = value;
            bgmSource.clip = bgms[(int)bgmScene];
            bgmSource.Play();            
        }
    }
    private BgmScene bgmScene;

    public Dictionary<string, BgmScene> bgmSceneDic;

    new void Awake()
    {
        base.Awake();
        bgmSceneDic = new Dictionary<string, BgmScene>
        {
            { "Main", BgmScene.Main },
            { "Battle", BgmScene.Battle },
            { "AwakeStage", BgmScene.Battle },
            { "War", BgmScene.Battle }
        };
        bgmSource = GetComponent<AudioSource>();    
        ChangeBgmScene = BgmScene.Main;

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode lsm) => { ChangeBgmScene = bgmSceneDic[scene.name]; };
    }
   
    
    // 사운드 매니저의 역할
    // 각 씬들의 AudioSource를 가지고있으며 씬전환 시 해당 씬에 맞는 bgm을 틀어주는 역할    
}
