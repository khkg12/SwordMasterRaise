using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlaceObj : MonoBehaviour, IAttackable
{
    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;
        set
        {
            targetLayerMask = value;
        }
    }
    private LayerMask targetLayerMask;

    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    private float atk;

    public int AttackNum
    {
        get => attackNum;
        set
        {
            attackNum = value;
        }
    }
    private int attackNum;

    [SerializeField] private float lifeTime;

    protected void OnEnable()
    {
        StartCoroutine(startLifeTimeCo());
        // ��Ʈ���� �ڷ�ƾ �����ϴ������� ��ƼŬ���� ����ȣ���ϱ⶧��
    }    

    public void Attack(IHitable hitable)
    {
        for (int i = 0; i < AttackNum; i++) // �ڷ�ƾ���� �ٲٱ�
        {            
            hitable.Hit(this);
        }
    }

    public void SetRotate(Transform userTrans)
    {
        transform.forward = userTrans.transform.forward;
    }

    public void SetAttack(float atk, LayerMask targetLayerMask, int attackNum)
    {
        Atk = atk;
        TargetLayerMask = targetLayerMask;
        AttackNum = attackNum;  
    }

    IEnumerator startLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // Ǯ�� ������
    }
}
