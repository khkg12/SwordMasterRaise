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
    public SkillInventoryUI skillInven;

    new void Start()
    {
        base.Start();
        StatusInit();
        setRotationComponent = GetComponent<SetRotationComponent>();
        attackCo = AttackCo();
        weapon.SetAttack(Atk, TargetLayerMask); // ���� ���� ����
        attackerCol = weapon.transform.GetComponent<Collider>();
    }

    public void StatusInit()
    {
        Hp = DataManager.instance.playerData.hp.stat;
        MoveSpeed = DataManager.instance.playerData.speed.stat;
        if (GameManager.instance.equipItemInfo.atkRate != 0) // �����ϱ�
            Atk = (int)(DataManager.instance.playerData.atk.stat * (GameManager.instance.equipItemInfo.atkRate / 100)); // �������� ���ݷ�������ŭ ���        
        else
            Atk = DataManager.instance.playerData.atk.stat;        
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
        Range = 5; // �����غ����� �ӽð�������
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
            AniTag = AnimationTag.Attack; // ���ݾִ� �����Ű��           
            yield return new WaitForSeconds(AttackSpeed); // �����ѹ��� ����
        }        
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
    }

    public void WeaponDisable()
    {
        attackerCol.enabled = false;
    }
}
