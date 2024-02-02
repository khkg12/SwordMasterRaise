using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseManager : Singleton<PauseManager>
{
    public event Action onPause;
    public event Action onResume;

    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            if (isPaused)
                Pause();
            else
                Resume();
        }
    }
    bool isPaused;

    public void Pause()
    {
        Debug.Log("adasd");
        onPause?.Invoke();
    }

    public void Resume()
    {
        onResume?.Invoke();
    }
}
