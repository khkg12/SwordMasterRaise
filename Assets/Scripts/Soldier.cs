using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Character
{    
    IEnumerator attackCo;
    [SerializeField] float attackIntervalTime;
    [SerializeField] Skill skill;

    new void Start()
    {
        attackCo = AttackCo();
        base.Start();        
    }   

    protected override void Init() // Soldier���� �ʿ��� ���¸� �־��ֱ�
    {
        fsm.AddState(StateTag.Idle, new IdleState(this));
        fsm.AddState(StateTag.Move, new MoveState(this));
        fsm.AddState(StateTag.Attack, new AttackState(this));        
        Range = 10; // �����غ����� �ӽð�������
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
            AniTag = AnimationTag.Attack; // ���ݾִ� �����Ű��
            skill.Use(this);            
            yield return new WaitForSeconds(attackIntervalTime); 
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
