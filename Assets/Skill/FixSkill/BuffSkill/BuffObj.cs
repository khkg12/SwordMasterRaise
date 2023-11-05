using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj : MonoBehaviour
{
    [SerializeField] private float lifeTime; // const로 다른스크립트애들도    
    Character character;
    float recovery;
    bool isReady;
    int recoveryNum;
    protected void OnEnable()
    {
        if (isReady)
        {            
            StartCoroutine(startLifeTimeCo());            
        }
        isReady = true;
    }

    //private void Update() // 지속시간이 긴 회복스킬일 경우 플레이어를 따라다녀야하니 이거나중에 수정
    //{        
    //    transform.position = character.transform.position;
    //}

    public void SetRecovery(float recovery, Character character, int recoveryNum)
    {
        this.character = character;
        this.recovery = recovery;
        this.recoveryNum = recoveryNum;        
    }

    public void StartRecoveryCo()
    {
        StartCoroutine(RecoveryCo());
    }

    IEnumerator startLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // 풀에 돌려줌
    }
    IEnumerator RecoveryCo() // 회복텀은 1초, 회복횟수에 따라 3번이면 3초 1번이면 1초로
    {        
        for (int i = 0; i < recoveryNum; i++)
        {            
            character.Hp += recovery;            
            yield return new WaitForSeconds(1f);
        }                
    }
}
