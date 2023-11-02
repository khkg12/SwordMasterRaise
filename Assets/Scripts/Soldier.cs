using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Character
{    
    IEnumerator attackCo;
    [SerializeField] float attackIntervalTime;
    public Skill skill;
    float attackDelayTime;

    new void Start()
    {
        attackCo = AttackCo();
        base.Start();        
    }    

    protected override void Init() // Soldier한테 필요한 상태만 넣어주기
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));        
        Range = 10; // 시험해보려고 임시값넣은것
    }

    protected override void Die()
    {
        Destroy(gameObject); // 솔져는 죽어도 그냥 파괴될 뿐
    }

    public override void AttackStart()
    {
        StartCoroutine(attackCo);
    }

    public override void AttackEnd()
    {
        StopCoroutine(attackCo);
    }    

    IEnumerator AttackCo()
    {
        while (true)
        {
            while (attackDelayTime <= attackIntervalTime) // 인터벌타임보다 대기시간이 커지면
            {
                attackDelayTime += Time.deltaTime;
                yield return null;
            }
            attackDelayTime = 0;
            AniTag = AnimationTag.Attack; 
            skill.Use(this);
        }                
    }

    private void OnTriggerEnter(Collider other) // 플레이어가 맞을 때 때리는 놈의 정보를 참조, character로 옮겨도 문제없으면 옮기기
    {
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable이라 플레이어스킬에도 맞을것같다. 레이어를 사용? -> IAttackable이나 IHitable에 LayerMask를 추가해야하나
        {
            if (attackable.TargetLayerMask == myLayerMask) // 공격하는 놈의 타겟레이어와 맞는놈의 레이어가 동일한 경우에만 Attack
            {                
                attackable.Attack(this);
            }
        }
    }    
}
