using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    bool isAttack;
    SetRotationComponent setRotationComponent;
    IEnumerator attackCo;
    int level;
    [SerializeField] Weapon weapon;
    Collider attackerCol;

    new void Start()
    {
        base.Start();
        setRotationComponent = GetComponent<SetRotationComponent>();
        attackCo = AttackCo();
        weapon.SetAttack(Atk, TargetLayerMask); // ���� ���� ����
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
                ChangeStateTag = StateTag.Skill; // ��� ����
        }
        base.Update();
    }

    protected override void Init() // Player���� �ʿ��� ���¸� �־��ֱ�
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));
        fsm.AddState(StateTag.Skill, new SkillState(this));
        Range = 5; // �����غ����� �ӽð�������
    }

    public override void AttackStart()
    {        
        isAttack = true;        
        StartCoroutine(attackCo);        
    }

    public override void AttackEnd()
    {        
        isAttack = false;
        StopCoroutine(attackCo);
    }


    IEnumerator AttackCo()
    {
        while(isAttack)
        {            
            AniTag = AnimationTag.Attack; // ���ݾִ� �����Ű��                    
            yield return new WaitForSeconds(1); // �����ѹ��� ����
        }        
    }

    private void OnTriggerEnter(Collider other) // �÷��̾ ���� �� ������ ���� ������ ����
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
    }

    public void WeaponDisable()
    {
        attackerCol.enabled = false;
    }
}
