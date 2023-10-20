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

    private void Start()
    {
        weapon.SetAttack(Atk, TargetLayerMask);
        attackerCol = weapon.transform.GetComponent<Collider>(); 
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
