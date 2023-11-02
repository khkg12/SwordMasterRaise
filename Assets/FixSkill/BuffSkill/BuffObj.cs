using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj : MonoBehaviour
{
    [SerializeField] private float lifeTime; // const�� �ٸ���ũ��Ʈ�ֵ鵵    
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

    //private void Update() // ���ӽð��� �� ȸ����ų�� ��� �÷��̾ ����ٳ���ϴ� �̰ų��߿� ����
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
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // Ǯ�� ������
    }
    IEnumerator RecoveryCo() // ȸ������ 1��, ȸ��Ƚ���� ���� 3���̸� 3�� 1���̸� 1�ʷ�
    {        
        for (int i = 0; i < recoveryNum; i++)
        {            
            character.Hp += recovery;            
            yield return new WaitForSeconds(1f);
        }                
    }
}
