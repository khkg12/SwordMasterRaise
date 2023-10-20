using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���Ϳ��� ���۾Ƹ� ������Ʈ�� ���� ����Ʈ���Ͱ� ����
public class Monster : MonoBehaviour, IHitable
{
    // ���������� �����ϰ�
    // id�� �ְ� ���͸� Ž���� �� �ְ���    
    // json���� stageData = [{id:0, monsterList : {{monsterId : 0, count : 3}, {monsterId : 1, count : 5}} ] // 0��° ��������(id : 0)�̰� id�� 0�� ���� 3����, id�� 1�� ���� 5����
    // �׷��ٸ� List<MonsterData>�� �����ϰ� �� monsterData�� ���� -> ��������?  
    // stageData�� ���� MonsterSpawner�� ��ȯ�� ���� ����� �������� ���� �� �����ϰ� ��ȯ
    // ���������� ���� ��� �����صΰ� ���ȿ� ���� ������ JSON���� �Ľ��ؼ� ���Ȱ��ʱ�ȭ    

    static Player target; // �̱��Ͽ��� �������� ������ �ƴ� static���� �صθ� ����Ŭ������ �������� ���, �� �ѹ��� Find�� ã�Ƶθ� �Ǳ� ������ static���� ��������
 
    [SerializeField] int id;    
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <= 0)
            {
                // Die()�Լ�����
            }
        }
    }
    private float hp;

    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    private float atk;

    public bool IsMove
    {
        get => isMove;
        set
        {
            isMove = value;
            if (isMove)
            {
                animator.SetBool("IsMove", true);
            }
            else
                animator.SetBool("IsMove", false);
        }
    }

    public bool IsHit
    {
        get => isHit;
        set
        {
            isHit = value;
            // 
        }
    }

    public LayerMask TargetLayerMask => targetLayerMask;
    [SerializeField] private LayerMask targetLayerMask;

    private bool isHit;

    private bool isMove;    

    [SerializeField] private float range;
    bool isAttack;
    protected Animator animator;
    [SerializeField] private LayerMask myLayerMask;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>();
        }
        animator = GetComponent<Animator>();    
        StartCoroutine(AttackCo());
    }
    
    void Update()
    {        
        if (targetDis() < range) // �������̶�� ����, range�� ����
        {            
            isAttack = true;
            IsMove = false;
        }
        else
        {
            isAttack = false;
            IsMove = true;
            ChaseTarget();            
        }        
    }

    public void ChaseTarget()
    {
        SetForward();
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f); // moveSpeed�� ����
    }

    public void SetForward()
    {
        Vector3 dir = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        dir = dir.normalized;
        transform.forward = dir;
    }

    public virtual void AttackStart()
    {
        // ȭ������̸� ���� �ִϸ����Ϳ��� isArrowAttack �̷��� �б⳪���� �Ѱ��� �ִϸ����ͷ� ����
        SetForward();        
    } 

    IEnumerator AttackCo()
    {
        while (true)
        {
            while (isAttack)
            {
                AttackStart(); // ���ø� �޼ҵ� ��������               
                yield return new WaitForSeconds(1); // ������ ���� ��������
            }
            yield return null;  
        }
    }

    float targetDis()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }

    void monsterInit()
    {
        //if(id == MONSTERdATA[id]) // id�� �����ͼ� üũ
        //{
        //    Hp = mosnterData[id].hp;
        //}
    }

    public void Hit(IAttackable attackable)
    {
        Hp -= attackable.Atk;
        // animator.SetTrigger("IsHit");
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable�̶� �÷��̾ų���� �����Ͱ���. ���̾ ���? -> IAttackable�̳� IHitable�� LayerMask�� �߰��ؾ��ϳ�
        {            
            if (attackable.TargetLayerMask == myLayerMask) // �����ϴ� ���� Ÿ�ٷ��̾�� �´³��� ���̾ ������ ��쿡�� Attack
            {                
                attackable.Attack(this);
            }            
        }
    }
}
