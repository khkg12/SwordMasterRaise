using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : Character
{    
    SetRotationComponent setRotationComponent;
    AwakeningComponent awakeningComponent;   
    IEnumerator attackCo;    
    [SerializeField] Weapon weapon;
    [SerializeField] Image dodgeCoolTimeImage;
    Collider attackerCol;
    public SkillInventoryUI skillInven;
    bool isDodge => (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && setRotationComponent.enabled == false) ? true : false;
    bool isUse;
    const float DODGE_COOL_TIME = 3f;

    new void Start()
    {
        base.Start();
        playerInit();        
    }

    new void Update()
    {
        base.Update();        
        if (isDodge && isUse)
        {
            StartCoroutine(DodgeCoolTimeCo());
            ChangeStateTag = StateTag.Dodge;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.SetDirection(hit.point);                
                //vector3 dir = (new vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
                //transform.forward = dir;
            }
        }
    }

    void playerInit()
    {
        setRotationComponent = GetComponent<SetRotationComponent>();
        awakeningComponent = GetComponent<AwakeningComponent>();
        awakeningComponent.AwakeLevel = DataManager.instance.playerData.awakeLevel; // 플레이어의 awakeLevel로 세팅
        attackCo = AttackCo();
        weapon.SetAttack(Atk, TargetLayerMask); // 웨폰 스탯 셋팅
        attackerCol = weapon.transform.GetComponent<Collider>();
        isUse = true;
    }
    
    public void ExecuteSkill(Skill skill)
    {
        if (currentSkill == null)
        {
            currentSkill = skill;
            if (currentSkill is RotateSkill)
            {
                setRotationComponent.enabled = true;
            }
            else
                ChangeStateTag = StateTag.Skill; // 즉시 실행
        }
    }

    protected override void Init() // Player한테 필요한 상태만 넣어주기
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));
        fsm.AddState(StateTag.Skill, new SkillState(this));
        fsm.AddState(StateTag.Dodge, new DodgeState(this));
        Range = 5; // 시험해보려고 임시값넣은것
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameLose(); 
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
            // SoundManager.instance.EffectPlay(공격 클립); // 플레이어가 가지고 있는 공격클립
            AniTag = AnimationTag.Attack; // 공격애니 실행시키고           
            yield return new WaitForSeconds(AttackSpeed); // 매직넘버는 공속
        }        
    }    
    IEnumerator DodgeCoolTimeCo()
    {
        isUse = false;
        float nowTime = 0;
        while (nowTime < DODGE_COOL_TIME) 
        {
            nowTime += Time.deltaTime;
            dodgeCoolTimeImage.fillAmount = nowTime / DODGE_COOL_TIME;
            yield return null;
        }
        isUse = true;
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

    // 애니메이션 event 함수
    // 쇼트레인지 몬스터의 무기 콜라이더 껐다 켰다     
    // Weapon에서 통합할 방법생각해보기, 컴포넌트패턴? 
    public void WeaponEnable()
    {
        attackerCol.enabled = true;
        if (awakeningComponent.enabled) // 각성컴포넌트가 켜져있을 때만
            awakeningComponent.AttackNum++;        
    }

    public void WeaponDisable()
    {
        attackerCol.enabled = false;
    }
}
