using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileObj : MonoBehaviour, IAttackable
{
    // ��� ���� : �÷��̾��̳� ���Ͱ� ���� �� trigger�� üũ�Ϸ��� �� ���� IAttackable�̿��� �ϴµ�, �׷��ٸ� projectileObj�� IAttackable�� ����ؾ��Ѵ�    
    
    // ��ų�� �ټ�Ÿ���� ���� �� ���� �� �������� ������� �־���ϴµ� �� ó���� ��� �ؾ��ұ�? -> �ټ�Ÿ�� ��ų�� �ݶ��̴��� ������� �ִ� �Ҹ��� �����״�?
    // -> ��ó�� ��� ���ݰ�ü�� ���� IAttackable�� ��ӹ޴´ٸ� Attack()�� ���� �������ټ��ִ�. �� ���� �� IAttackable�� Attack�� �����ϸ� ���� �ٸ� ����� ȣ��� ��

    Vector3 rotateVec;

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

    // ��ų���� ���ư��� �ӵ��� �����ð��� �Ѱ�����, �ƴϸ� ���ư��� �ӵ��� �����ð��� projectileObj���� ���ص���
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    
    private void Start()
    {
        transform.forward = rotateVec;
        // ���ʵ� ������� �ڷ�ƾ, ����� �� Ǯ���� �ٽ� ��°ͱ���
        StartCoroutine(startLifeTimeCo());
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed); // �������� ó��, �������ٰ� ����ü���� �ӵ��� �ٸ��״�
    }
    
    public void SetRotate(Transform userTrans)
    {        
        rotateVec = userTrans.transform.forward;
    }    

    public void Attack(IHitable hitable)
    {
        hitable.Hit(this);
    }

    public void SetAttack(float atk, LayerMask targetLayerMask)
    {
        Atk = atk;
        TargetLayerMask = targetLayerMask;
    }

    IEnumerator startLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);        
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // Ǯ�� ������
    }
}

