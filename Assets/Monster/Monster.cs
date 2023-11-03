using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ���Ϳ��� ���۾Ƹ� ������Ʈ�� ���� ����Ʈ���Ͱ� ����
public class Monster : MonoBehaviour, IHitable
{   
    static Player target; // �̱��Ͽ��� �������� ������ �ƴ� static���� �صθ� ����Ŭ������ �������� ���, �� �ѹ��� Find�� ã�Ƶθ� �Ǳ� ������ static���� ��������    
    [SerializeField] int id;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float range;
    [SerializeField] private LayerMask myLayerMask;
    [SerializeField] private Renderer monsterRenderer;
    [SerializeField] private Image hpBar;
    [SerializeField] private bool isSuperArmor;
    bool isDead;
    public float Hp
    {
        get => hp;
        set
        {
            float damage = hp - value;
            if(damage > 0) // ������� ���� ��
            {
                GameObject damageText = PoolManager.instance.objectPoolDic["DamageText"].PopObj(transform.position, Quaternion.identity);
                damageText.GetComponent<FloatingText>().Damage = damage;
                damageText.GetComponent<FloatingText>().Color = Color.red;
            }
            hp = value;
            hpBar.fillAmount = hp / maxHp;
            if (hp <= 0 && !isDead)
            {                
                isDead = true;                
                DIe();
            }
        }
    }
    [SerializeField] private float hp; // �����
    private float maxHp;

    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    [SerializeField] private float atk;

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
        monsterInit(); // maxHp ���� �ӽ�, ���߿� json�Ľ��Ұ�
    }

    private void OnEnable()
    {                
        StartCoroutine(AttackCo());
        IsHit = false;
        isDead = false;
        hp = maxHp;
    }

    protected void Update()
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
        maxHp = hp;
    }

    public void Hit(IAttackable attackable)
    {
        const float OFFSET_RATE = 0.1f;
        float atk = attackable.Atk;
        float offset = atk * OFFSET_RATE;
        atk = (int)Random.Range(atk - offset, atk + offset);
        Hp -= atk;
    }   

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable�̶� �÷��̾ų���� �����Ͱ���. ���̾ ���? -> IAttackable�̳� IHitable�� LayerMask�� �߰��ؾ��ϳ�
        {            
            if (attackable.TargetLayerMask == myLayerMask) // �����ϴ� ���� Ÿ�ٷ��̾�� �´³��� ���̾ ������ ��쿡�� Attack
            {
                // if(!gameObject.TryGetComponent<>(out SuperArmor superArmor)) // ���۾Ƹ� ���Ͱ� �ƴҶ��� ��Ʈ����
                if (!isSuperArmor) // ���۾ƸӰ� �ƴ϶��
                {
                    animator.SetTrigger("HitTrigger");
                    if (this is ShortRangeMonster)
                    {
                        ShortRangeMonster mon = (ShortRangeMonster)this; // ��������ϱ� ��������
                        mon.WeaponDisable();
                    }
                }                                              
                attackable.Attack(this);               
            }            
        }
    }    

    public virtual void DIe()
    {        
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject);
        GameManager.instance.MonsterCount--;
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
