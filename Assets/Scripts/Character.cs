using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using System;
using Random = UnityEngine.Random;

public enum AnimationTag
{
    Idle,
    Move,
    Attack,
    Skill,
    Dodge
}

public enum StateTag
{
    Idle,
    Move,
    Attack,
    Skill,
    Dodge
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
            float damage = hp - value;
            hp = value;
            if (hp >= maxHp)
                hp = maxHp;
            if (hp <= 0)
                Die();
            hpBar.fillAmount = hp / maxHp;
            // 대미지텍스트
            GameObject damageText = PoolManager.instance.objectPoolDic["DamageText"].PopObj(transform.position, Quaternion.identity);            
            if (damage >= 0)
                damageText.GetComponent<FloatingText>().Color = Color.black;
            else
            {
                damageText.GetComponent<FloatingText>().Color = Color.green; // 회복이면 초록색 글씨            
                damage = -damage;                
            }
            damageText.GetComponent<FloatingText>().Damage = damage;                                         
        }
    }
    [SerializeField] protected float hp;

    public float MaxHp => maxHp;

    protected float maxHp;

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
        }
    }
    private float moveSpeed;

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
                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) // Idle일때만
                        animator.SetTrigger("AttackTrigger");
                    // 애니메이션 실행하는 컴포넌트를 has a로 가지고 컴포넌트딴에서 실행시킬지 고민해보기
                    // ex) AnimationComponent.Attack(); <- 애니메이션 컴포넌트가 Animator를 들고있고 거기서 이것저것
                    break;
                case AnimationTag.Skill:
                    animator.SetTrigger("SkillTrigger");
                    break;
                case AnimationTag.Dodge:
                    animator.SetTrigger("DodgeTrigger");
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
            fsm.ChangeState(value);
            /*switch (changeStateTag)
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
                    case StateTag.Dodge:
                        fsm.ChangeState(StateTag.Dodge);
                        break;
            }*/
        }
    }
    StateTag changeStateTag;

    public bool IsHit // 몬스터에서만 써서 굳이 인터페이스가 가지고있을 필요없을듯
    {
        get => isHit;
        set
        {
            isHit = value;              
        }
    }
    private bool isHit;

    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;        
    }
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] protected LayerMask myLayerMask;
    [SerializeField] Image hpBar;
    [SerializeField] Image shadowHpBar;
    [NonSerialized] public Skill currentSkill;
    [NonSerialized] public Collider targetCol;
    private Rigidbody rb;
    private Collider col;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>(); 
        fsm = new Fsm();
        Init();
        StatusInit();
        ChangeStateTag = StateTag.Idle;
        // fsm.ChangeState(StateTag.Idle);
    }
 
    protected void Update()
    {
        hpBar.rectTransform.eulerAngles = new Vector3(90, 0, 0);
        shadowHpBar.rectTransform.eulerAngles = new Vector3(90, 0, 0);
        fsm.Update();      
    }

    public virtual void StatusInit()
    {
        maxHp = DataManager.instance.playerData.hp.stat;
        hp = maxHp;
        MoveSpeed = DataManager.instance.playerData.speed.stat;
        if (GameManager.instance.equipItemInfo.atkRate != 0) // 수정하기
            Atk = (int)(DataManager.instance.playerData.atk.stat * (GameManager.instance.equipItemInfo.atkRate / 100)); // 장착무기 공격력증가만큼 상승        
        else
            Atk = DataManager.instance.playerData.atk.stat;
    }

    protected virtual void Init()
    {
        Debug.LogError("부모의 Init이 실행! 자식에서 재정의하세요");    
    } 
    
    protected virtual void Die()
    {
        Debug.LogError("부모의 Die가 실행! 자식에서 재정의하세요");
        // Destroy(gameObject);
        // 동료가 character를 상속받는 구조일 시 virtual 템플릿메소드패턴 써서 Player가 죽었을 때만 게임종료되게 할 것
        // 그리고 Player사망시에만 GameManager.instance.monsterCount 0으로 초기화
        //SceneManager.LoadScene("Main"); // Dead로 바꾸기, 아니면 버튼하나 띄우고 버튼클릭 시 Main씬으로 UiManager만들어야할듯 이거쓰려면
        // UIManager.instance.ShowDefeatUI();
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

    public void SetForward() // 확장메소드 리팩토링 어필
    {
        Vector3 dir = new Vector3(targetCol.transform.position.x, transform.position.y, targetCol.transform.position.z) - transform.position;
        dir = dir.normalized;        
        transform.forward = dir;
    }

    public void Hit(IAttackable attackable)
    {
        // Hp -= GetRandomDamageOffset(attackable.Atk);
        Hp -= attackable.Atk.GetRandomDamageOffset();        
    }

    //float GetRandomDamageOffset(float atk)
    //{
    //    const float OFFSET_RATE = 0.1f;
    //    float offset = atk * OFFSET_RATE;
    //    atk = (int)Random.Range(atk - offset, atk + offset);
    //    return atk;
    //}

    public void RecoveryHp(float recoveryRate) // 비율로 체력회복, 수치체력회복 필요하면 만들기
    {
        Hp += (int)(recoveryRate * maxHp);
    }
    
    public void Invincibility(bool isEnabled)
    {
        col.enabled = isEnabled;
        rb.useGravity = isEnabled;
    }
}
