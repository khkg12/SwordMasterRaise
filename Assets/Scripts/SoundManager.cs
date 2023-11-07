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
    // ���� �Ŵ����� ����
    // �� ������ AudioSource�� ������������ ����ȯ �� �ش� ���� �´� bgm�� Ʋ���ִ� ����
    // 

}
