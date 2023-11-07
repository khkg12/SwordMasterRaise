using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BgmScene
{
    Main,
    Battle
}

public class SoundManager : Singleton<SoundManager> 
{
    AudioSource bgmSource;
    public AudioClip[] bgms;    
    public BgmScene BgmScene
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

    new void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
        BgmScene = BgmScene.Main;
    }        
    // 사운드 매니저의 역할
    // 각 씬들의 AudioSource를 가지고있으며 씬전환 시 해당 씬에 맞는 bgm을 틀어주는 역할
    // 

}
