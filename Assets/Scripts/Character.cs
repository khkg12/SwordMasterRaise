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
    private float atk;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    [SerializeField] private float hp;

    protected Fsm fsm;    
    private Animator animator;    

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
                    animator.SetTrigger("AttackTrigger");
                    // 애니메이션 실행하는 컴포넌트를 has a로 가지고 컴포넌트딴에서 실행시킬지 고민해보기
                    // ex) AnimationComponent.Attack(); <- 애니메이션 컴포넌트가 Animator를 들고있고 거기서 이것저것
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

    StateTag changeStateTag;

    public List<Skill> skillList;
    public Skill currentSkill;
    public Collider targetCol;
    public LayerMask targetLayer;

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
        Debug.LogError("부모의 Init이 실행! 자식에서 재정의하세요");    
    }   

    public virtual void AttackStart()
    {
        Debug.Log("공격 실행");
        // 여기서 기본캐릭터의 공격함수를 실행시킬수도 있고 Player가 각성한 캐릭터거나, 신규캐릭터일수도 있고
    }

    public virtual void AttackEnd()
    {
        Debug.Log("공격 종료");
    }

    public void SetForward()
    {
        Vector3 dir = new Vector3(targetCol.transform.position.x, transform.position.y, targetCol.transform.position.z) - transform.position;
        dir = dir.normalized;
        transform.forward = dir;
    }

    public void Hit(IAttackable attackable)
    {
        Hp -= attackable.Atk;
        // animator.SetTrigger("HitTrigger");
    }  
}
