using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 몬스터에는 슈퍼아머 컴포넌트를 가진 엘리트몬스터가 존재
public class Monster : MonoBehaviour, IHitable
{   
    static Player target; // 싱글턴에서 가져오는 형식이 아닌 static으로 해두면 몬스터클래스가 공용으로 사용, 즉 한번만 Find로 찾아두면 되기 때문에 static으로 변수지정    
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
            if(damage > 0) // 대미지가 있을 때
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
    [SerializeField] private float hp; // 실험용
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
        monsterInit(); // maxHp 세팅 임시, 나중에 json파싱할것
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
        if (targetDis() < range) // 범위안이라면 공격, range로 빼기
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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f); // moveSpeed로 빼기
    }

    public void SetForward()
    {
        Vector3 dir = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        dir = dir.normalized;
        transform.forward = dir;
    }

    public virtual void AttackStart()
    {
        // 화살공격이면 같은 애니메이터에서 isArrowAttack 이렇게 분기나눠서 한개의 애니메이터로 관리
        // ColorChangeEnd(); // 공격시 빨간색 해제
        SetForward();        
    } 

    IEnumerator AttackCo()
    {
        while (true)
        {
            while (isAttack)
            {                                
                AttackStart(); // 템플릿 메소드 패턴적용
                yield return new WaitForSeconds(3); // 변수로 빼기 공속으로
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
        //if(id == MONSTERdATA[id]) // id를 가져와서 체크
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
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable이라 플레이어스킬에도 맞을것같다. 레이어를 사용? -> IAttackable이나 IHitable에 LayerMask를 추가해야하나
        {            
            if (attackable.TargetLayerMask == myLayerMask) // 공격하는 놈의 타겟레이어와 맞는놈의 레이어가 동일한 경우에만 Attack
            {
                // if(!gameObject.TryGetComponent<>(out SuperArmor superArmor)) // 슈퍼아머 몬스터가 아닐때만 힛트리거
                if (!isSuperArmor) // 슈퍼아머가 아니라면
                {
                    animator.SetTrigger("HitTrigger");
                    if (this is ShortRangeMonster)
                    {
                        ShortRangeMonster mon = (ShortRangeMonster)this; // 얕은복사니까 괜찮을듯
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

    // event함수
    public void ColorChangeStart()
    {
        IsHit = true;
    }

    public void ColorChangeEnd() 
    {
        IsHit = false;
    }
}
