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
   
    
    // ���� �Ŵ����� ����
    // �� ������ AudioSource�� ������������ ����ȯ �� �ش� ���� �´� bgm�� Ʋ���ִ� ����    
}
