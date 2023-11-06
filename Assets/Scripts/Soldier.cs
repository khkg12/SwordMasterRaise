using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Character
{    
    IEnumerator attackCo;
    [SerializeField] float attackIntervalTime;
    public Skill skill;
    float attackDelayTime;
    const float ATK_RATE = 0.1f;
    const float HP_RATE = 0.1f; // �̵����͵� ��ȭ�������ϰ� �ʱ�ȭ�� �� �����ͷ� �ʱ�ȭ
    const float MOVE_RATE = 0.5f;

    new void Start()
    {
        attackCo = AttackCo();
        base.Start();  
        StartCoroutine(DodgeCo());  
    }    

    IEnumerator DodgeCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (Physics.OverlapSphere(transform.position, 1f, TargetLayerMask).Length >= 5)
            {
                // 4���� �� ���� ���� ���� ������ �ٶ󺸰��ѵ� 
                ChangeStateTag = StateTag.Dodge;
            }
        }        
    }

    protected override void Init() // Soldier���� �ʿ��� ���¸� �־��ֱ�
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));
        fsm.AddState(StateTag.Dodge, new DodgeState(this));
        Range = 10; // �����غ����� �ӽð�������
    }

    public override void StatusInit()
    {
        base.StatusInit();
        maxHp *= HP_RATE;
        hp = maxHp;
        MoveSpeed *= MOVE_RATE;
        Atk *= ATK_RATE;
    }

    protected override void Die()
    {
        Destroy(gameObject); // ������ �׾ �׳� �ı��� ��
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
            while (attackDelayTime <= attackIntervalTime) // ���͹�Ÿ�Ӻ��� ���ð��� Ŀ����
            {
                attackDelayTime += Time.deltaTime;
                yield return null;
            }
            attackDelayTime = 0;
            AniTag = AnimationTag.Attack; 
            skill.Use(this);
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
}
