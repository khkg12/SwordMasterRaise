using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum AnimationTag
{
    Idle,
    Move,
    Attack,
}

public enum StateTag
{
    Idle,
    Move,
    Attack,
    Skill
}

public class Character : MonoBehaviour, IHitable
{
    public float Range
    {
        get => range;
        set
        {
            range = value;
        }
    }
    private float range;
    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    private float atk = 10;
    public float Hp
    {
        get => hp;
        set
        {
            float damage = hp - value;
            hp = value;            
            GameObject damageText = PoolManager.instance.objectPoolDic["DamageText"].PopObj(transform.position, Quaternion.identity);
            damageText.GetComponent<FloatingText>().Damage = damage;
            damageText.GetComponent<FloatingText>().Color = Color.white;
            if (hp <= 0)
            {
                // Die()�Լ�����
                // Destroy(gameObject);
            }
        }
    }
    [SerializeField] private float hp;

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
        }
    }
    private float moveSpeed = 0.02f;

    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            attackSpeed = value;    
        }
    }
    private float attackSpeed = 0.1f;

    protected Fsm fsm;    
    private Animator animator;
    [SerializeField] private Renderer characterRenderer;
    Color orginColor;

    public AnimationTag AniTag
    {
        get => aniTag;
        set
        {
            aniTag = value;
            switch (aniTag)
            {
                case AnimationTag.Idle:                    
                    animator.SetBool("IsMove", false);
                    break;
                case AnimationTag.Move:
                    animator.SetBool("IsMove", true);
                    break;
                case AnimationTag.Attack:
                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) // Idle�϶���
                        animator.SetTrigger("AttackTrigger");
                    // �ִϸ��̼� �����ϴ� ������Ʈ�� has a�� ������ ������Ʈ������ �����ų�� ����غ���
                    // ex) AnimationComponent.Attack(); <- �ִϸ��̼� ������Ʈ�� Animator�� ����ְ� �ű⼭ �̰�����
                    break;
            }
        }
    }
    AnimationTag aniTag;

    public StateTag ChangeStateTag
    {
        get => changeStateTag;
        set
        {
            changeStateTag = value;
            switch (changeStateTag)
            {
                case StateTag.Idle:
                    fsm.ChangeState(StateTag.Idle);
                    break;
                case StateTag.Move:
                    fsm.ChangeState(StateTag.Move);
                    break;
                case StateTag.Attack:
                    fsm.ChangeState(StateTag.Attack);
                    break;
                case StateTag.Skill:
                    fsm.ChangeState(StateTag.Skill);
                    break;
            }
        }
    }
    StateTag changeStateTag;

    public bool IsHit
    {
        get => isHit;
        set
        {
            isHit = value;  
            // 
        }
    }
    private bool isHit;

    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;        
    }
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] protected LayerMask myLayerMask;           
    public Skill currentSkill;
    public GameObject target;    

    protected void Start()
    {
        animator = GetComponent<Animator>();
        fsm = new Fsm();
        Init();
        fsm.ChangeState(StateTag.Idle);
    }
 
    protected void Update()
    {        
        fsm.Update();
    }

    protected virtual void Init()
    {
        Debug.LogError("�θ��� Init�� ����! �ڽĿ��� �������ϼ���");    
    }   

    public virtual void AttackStart()
    {
        Debug.Log("���� ����");
        // ���⼭ �⺻ĳ������ �����Լ��� �����ų���� �ְ� Player�� ������ ĳ���Ͱų�, �ű�ĳ�����ϼ��� �ְ�
    }

    public virtual void AttackEnd()
    {
        Debug.Log("���� ����");
    }

    public void SetForward()
    {
        Vector3 dir = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        dir = dir.normalized;
        transform.forward = dir;
    }

    public void Hit(IAttackable attackable)
    {
        Hp -= attackable.Atk;        
    }  
}
