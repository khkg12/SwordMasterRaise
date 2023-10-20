using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{    
    SetRotationComponent setRotationComponent;
    IEnumerator attackCo;    
    [SerializeField] Weapon weapon;
    Collider attackerCol;

    new void Start()
    {

        base.Start();       
        setRotationComponent = GetComponent<SetRotationComponent>();
        attackCo = AttackCo();
        weapon.SetAttack(Atk, TargetLayerMask); // 웨폰 스탯 셋팅
        attackerCol = weapon.transform.GetComponent<Collider>();
    }

    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentSkill == null)
        {
            currentSkill = skillList[0];
            if(currentSkill is RotateSkill)
            {
                setRotationComponent.enabled = true;
            }                        
            else
                ChangeStateTag = StateTag.Skill; // 즉시 실행
        }
        base.Update();
    }

    protected override void Init() // Player한테 필요한 상태만 넣어주기
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));
        fsm.AddState(StateTag.Skill, new SkillState(this));
        Range = 5; // 시험해보려고 임시값넣은것
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
            Debug.Log("asd");
            AniTag = AnimationTag.Attack; // 공격애니 실행시키고           
            yield return new WaitForSeconds(AttackSpeed); // 매직넘버는 공속
        }        
    }

    private void OnTriggerEnter(Collider other) // 플레이어가 맞을 때 때리는 놈의 정보를 참조
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable이라 플레이어스킬에도 맞을것같다. 레이어를 사용? -> IAttackable이나 IHitable에 LayerMask를 추가해야하나
        {            
            if (attackable.TargetLayerMask == myLayerMask) // 공격하는 놈의 타겟레이어와 맞는놈의 레이어가 동일한 경우에만 Attack
            {                
                attackable.Attack(this);
            }            
        }             
    }

    // 애니메이션 event 함수
    // 쇼트레인지 몬스터의 무기 콜라이더 껐다 켰다     
    // Weapon에서 통합할 방법생각해보기, 컴포넌트패턴? 
    public void WeaponEnable()
    {
        attackerCol.enabled = true;
    }

    public void WeaponDisable()
    {
        attackerCol.enabled = false;
    }
}
