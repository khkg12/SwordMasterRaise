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
            // ������ؽ�Ʈ
            GameObject damageText = PoolManager.instance.objectPoolDic["DamageText"].PopObj(transform.position, Quaternion.identity);            
            if (damage >= 0)
                damageText.GetComponent<FloatingText>().Color = Color.black;
            else
            {
                damageText.GetComponent<FloatingText>().Color = Color.green; // ȸ���̸� �ʷϻ� �۾�            
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
                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) // Idle�϶���
                        animator.SetTrigger("AttackTrigger");
                    // �ִϸ��̼� �����ϴ� ������Ʈ�� has a�� ������ ������Ʈ������ �����ų�� ����غ���
                    // ex) AnimationComponent.Attack(); <- �ִϸ��̼� ������Ʈ�� Animator�� ����ְ� �ű⼭ �̰�����
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

    public bool IsHit // ���Ϳ����� �Ἥ ���� �������̽��� ���������� �ʿ������
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
        if (GameManager.instance.equipItemInfo.atkRate != 0) // �����ϱ�
            Atk = (int)(DataManager.instance.playerData.atk.stat * (GameManager.instance.equipItemInfo.atkRate / 100)); // �������� ���ݷ�������ŭ ���        
        else
            Atk = DataManager.instance.playerData.atk.stat;
    }

    protected virtual void Init()
    {
        Debug.LogError("�θ��� Init�� ����! �ڽĿ��� �������ϼ���");    
    } 
    
    protected virtual void Die()
    {
        Debug.LogError("�θ��� Die�� ����! �ڽĿ��� �������ϼ���");
        // Destroy(gameObject);
        // ���ᰡ character�� ��ӹ޴� ������ �� virtual ���ø��޼ҵ����� �Ἥ Player�� �׾��� ���� ��������ǰ� �� ��
        // �׸��� Player����ÿ��� GameManager.instance.monsterCount 0���� �ʱ�ȭ
        //SceneManager.LoadScene("Main"); // Dead�� �ٲٱ�, �ƴϸ� ��ư�ϳ� ���� ��ưŬ�� �� Main������ UiManager�������ҵ� �̰ž�����
        // UIManager.instance.ShowDefeatUI();
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

    public void SetForward() // Ȯ��޼ҵ� �����丵 ����
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

    public void RecoveryHp(float recoveryRate) // ������ ü��ȸ��, ��ġü��ȸ�� �ʿ��ϸ� �����
    {
        Hp += (int)(recoveryRate * maxHp);
    }
    
    public void Invincibility(bool isEnabled)
    {
        col.enabled = isEnabled;
        rb.useGravity = isEnabled;
    }
}
