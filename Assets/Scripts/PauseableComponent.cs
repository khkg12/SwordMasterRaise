using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseableComponent : MonoBehaviour, IPauseable
{    
    [SerializeField] List<MonoBehaviour> enabledComponentList; // 꺼줄 목록
    [SerializeField] UnityEvent onPause;
    public void Pause()
    {
        foreach(MonoBehaviour component in enabledComponentList)
        {
            component.enabled = false;
        }
        onPause?.Invoke();
    }
    
    void Start()
    {
        PauseManager.instance.onPause += Pause; // 일시정지 매니저에 엮어줌
    }
}
