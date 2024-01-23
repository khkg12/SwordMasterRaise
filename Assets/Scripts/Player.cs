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
        awakeningComponent.AwakeLevel = DataManager.instance.playerData.awakeLevel; // �÷��̾��� awakeLevel�� ����
        attackCo = AttackCo();
        weapon.SetAttack(Atk, TargetLayerMask); // ���� ���� ����
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
                ChangeStateTag = StateTag.Skill; // ��� ����
        }
    }

    protected override void Init() // Player���� �ʿ��� ���¸� �־��ֱ�
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));
        fsm.AddState(StateTag.Skill, new SkillState(this));
        fsm.AddState(StateTag.Dodge, new DodgeState(this));
        Range = 5; // �����غ����� �ӽð�������
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
            // SoundManager.instance.EffectPlay(���� Ŭ��); // �÷��̾ ������ �ִ� ����Ŭ��
            AniTag = AnimationTag.Attack; // ���ݾִ� �����Ű��           
            yield return new WaitForSeconds(AttackSpeed); // �����ѹ��� ����
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

    private void OnTriggerEnter(Collider other) // �÷��̾ ���� �� ������ ���� ������ ����, character�� �Űܵ� ���������� �ű��
    {        
        if (other.gameObject.TryGetComponent(out IAttackable attackable)) // attackable�̶� �÷��̾ų���� �����Ͱ���. ���̾ ���? -> IAttackable�̳� IHitable�� LayerMask�� �߰��ؾ��ϳ�
        {            
            if (attackable.TargetLayerMask == myLayerMask) // �����ϴ� ���� Ÿ�ٷ��̾�� �´³��� ���̾ ������ ��쿡�� Attack
            {                
                attackable.Attack(this);
            }            
        }             
    }    

    // �ִϸ��̼� event �Լ�
    // ��Ʈ������ ������ ���� �ݶ��̴� ���� �״�     
    // Weapon���� ������ ��������غ���, ������Ʈ����? 
    public void WeaponEnable()
    {
        attackerCol.enabled = true;
        if (awakeningComponent.enabled) // ����������Ʈ�� �������� ����
            awakeningComponent.AttackNum++;        
    }

    public void WeaponDisable()
    {
        attackerCol.enabled = false;
    }
}
