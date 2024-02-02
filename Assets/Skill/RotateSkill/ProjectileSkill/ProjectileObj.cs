using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileObj : MonoBehaviour, IAttackable, IPauseable
{
    // ��� ���� : �÷��̾��̳� ���Ͱ� ���� �� trigger�� üũ�Ϸ��� �� ���� IAttackable�̿��� �ϴµ�, �׷��ٸ� projectileObj�� IAttackable�� ����ؾ��Ѵ�    
    
    // ��ų�� �ټ�Ÿ���� ���� �� ���� �� �������� ������� �־���ϴµ� �� ó���� ��� �ؾ��ұ�? -> �ټ�Ÿ�� ��ų�� �ݶ��̴��� ������� �ִ� �Ҹ��� �����״�?
    // -> ��ó�� ��� ���ݰ�ü�� ���� IAttackable�� ��ӹ޴´ٸ� Attack()�� ���� �������ټ��ִ�. �� ���� �� IAttackable�� Attack�� �����ϸ� ���� �ٸ� ����� ȣ��� ��    

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

    // ��ų���� ���ư��� �ӵ��� �����ð��� �Ѱ�����, �ƴϸ� ���ư��� �ӵ��� �����ð��� projectileObj���� ���ص���
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        RegistHandler();
    }

    private void OnEnable()
    {        
        // ���ʵ� ������� �ڷ�ƾ, ����� �� Ǯ���� �ٽ� ��°ͱ���
        StartCoroutine(startLifeTimeCo());
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed); // �������� ó��, �������ٰ� ����ü���� �ӵ��� �ٸ��״�
    }
    
    public void SetRotate(Transform userTrans)
    {
        transform.forward = userTrans.transform.forward;
    }    

    public void Attack(IHitable hitable)
    {        
        for(int i = 0; i < AttackNum; i++) // �ڷ�ƾ���� �ٲٱ�
        {            
            hitable.Hit(this);
        }        
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

    public void RegistHandler()
    {
        PauseManager.instance.onPause += () => { enabled = false; };
        PauseManager.instance.onResume += () => { enabled = true; };
    }
}

