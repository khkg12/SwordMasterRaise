using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeMonster : Monster
{
    // shortRangeMonster�� ���⿡ ���� Ŭ������ has a�� ������ �ִ´�.
    // ����� IAttackable�� ��ӹ޴´�. -> ������ ������ �͸��� IAtackable�� ��ӹ޴´ٰ� �����Ѵٸ�
    // �÷��̾ has a�� ������ �ִ� Weapon�� �״�λ���ص� �� ��
    
    [SerializeField] Weapon weapon;
    Collider attackerCol;

    protected void Start()
    {        
        attackerCol = weapon.transform.GetComponent<Collider>(); 
    }

    new void OnEnable()
    {
        base.OnEnable();
        weapon.SetAttack(Atk, TargetLayerMask, 1); // ����� �ִ� ������ ���� ���Ѵ����� ������ ���Ͱ� �������µ� weapon�� ���� �ʱ�ȭ ��������ؼ�
    }

    public override void AttackStart()
    {
        base.AttackStart();
        animator.SetTrigger("ShortAttackTrigger");        
    }

    // �ִϸ��̼� event �Լ�
    // ��Ʈ������ ������ ���� �ݶ��̴� ���� �״�     
    public void WeaponEnable()
    {        
        attackerCol.enabled = true;
    }

    public void WeaponDisable() // �������� �̺�Ʈ�߰����ֱ�
    {
        attackerCol.enabled = false;
    }    
}
