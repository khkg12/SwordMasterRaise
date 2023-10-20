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

    private void Start()
    {
        transform.forward = rotateVec;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * 0.1f); // �������� ó��, �������ٰ� ����ü���� �ӵ��� �ٸ��״�
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
}

