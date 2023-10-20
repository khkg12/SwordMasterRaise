using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 몬스터에는 슈퍼아머 컴포넌트를 가진 엘리트몬스터가 존재
public class Monster : MonoBehaviour, IHitable
{
    // 프리팹으로 존재하고
    // id를 주고 몬스터를 탐색할 수 있게함    
    // json에서 stageData = [{id:0, monsterList : {{monsterId : 0, count : 3}, {monsterId : 1, count : 5}} ] // 0번째 스테이지(id : 0)이고 id가 0인 몬스터 3마리, id가 1인 몬스터 5마리
    // 그렇다면 List<MonsterData>가 존재하고 위 monsterData를 저장 -> 프리팹은?  
    // stageData를 토대로 MonsterSpawner가 소환할 몬스터 목록을 스테이지 진입 시 세팅하고 소환
    // 프리팹으로 몬스터 모두 구현해두고 스탯에 대한 정보만 JSON으로 파싱해서 스탯값초기화    

    static Player target; // 싱글턴에서 가져오는 형식이 아닌 static으로 해두면 몬스터클래스가 공용으로 사용, 즉 한번만 Find로 찾아두면 되기 때문에 static으로 변수지정
 
    [SerializeField] int id;    
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <= 0)
            {
                // Die()함수실행
                Destroy(gameObject);
            }
        }
    }
    private float hp = 100; // 실험용
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
    }

    public void Hit(IAttackable attackable)
    {        
        Hp -= attackable.Atk;
        // HitSet();
        // animator.SetTrigger("IsHit");
    }

    //public virtual void HitSet() // 맞았을 때의 세팅
    //{
    //    // 필요한 놈들만 사용
    //}

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable이라 플레이어스킬에도 맞을것같다. 레이어를 사용? -> IAttackable이나 IHitable에 LayerMask를 추가해야하나
        {            
            if (attackable.TargetLayerMask == myLayerMask) // 공격하는 놈의 타겟레이어와 맞는놈의 레이어가 동일한 경우에만 Attack
            {                
                animator.SetTrigger("HitTrigger");            
                if(this is ShortRangeMonster)
                {
                    ShortRangeMonster mon = (ShortRangeMonster)this; // 얕은복사니까 괜찮을듯
                    mon.WeaponDisable();
                }                
                attackable.Attack(this);
            }            
        }
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
