using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
                Destroy(gameObject);
            }
        }
    }
    private float hp = 100; // �����
    private float MaxHp = 100;

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
    private bool isMove;

    public bool IsHit 
    {
        get => isHit;
        set
        {
            isHit = value;              
            monsterRenderer.material.color = isHit ? Color.red : orginColor;
        }
    }
    private bool isHit;

    public LayerMask TargetLayerMask => targetLayerMask;

    [SerializeField] private LayerMask targetLayerMask;        
    [SerializeField] private float range;    
    [SerializeField] private LayerMask myLayerMask;
    [SerializeField] private Renderer monsterRenderer;    
    [SerializeField] private Image hpBar;
    private Color orginColor;
    private bool isAttack;
    protected Animator animator;


    private void Awake()
    {        
        if (target == null)
        {
            target = FindObjectOfType<Player>();
        }
        animator = GetComponent<Animator>();
        orginColor = monsterRenderer.material.color;
        StartCoroutine(AttackCo());    
        
    }
    
    void Update()
    {
        hpBar.fillAmount = hp / MaxHp;
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
        // ColorChangeEnd(); // ���ݽ� ������ ����
        SetForward();        
    } 

    IEnumerator AttackCo()
    {
        while (true)
        {
            while (isAttack)
            {                                
                AttackStart(); // ���ø� �޼ҵ� ��������
                yield return new WaitForSeconds(3); // ������ ���� ��������
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
        // HitSet();
        // animator.SetTrigger("IsHit");
    }

    //public virtual void HitSet() // �¾��� ���� ����
    //{
    //    // �ʿ��� ��鸸 ���
    //}

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable�̶� �÷��̾ų���� �����Ͱ���. ���̾ ���? -> IAttackable�̳� IHitable�� LayerMask�� �߰��ؾ��ϳ�
        {            
            if (attackable.TargetLayerMask == myLayerMask) // �����ϴ� ���� Ÿ�ٷ��̾�� �´³��� ���̾ ������ ��쿡�� Attack
            {                
                animator.SetTrigger("HitTrigger");            
                if(this is ShortRangeMonster)
                {
                    ShortRangeMonster mon = (ShortRangeMonster)this; // ��������ϱ� ��������
                    mon.WeaponDisable();
                }                
                attackable.Attack(this);
            }            
        }
    }    

    // event�Լ�
    public void ColorChangeStart()
    {
        IsHit = true;
    }

    public void ColorChangeEnd() 
    {
        IsHit = false;
    }
}
