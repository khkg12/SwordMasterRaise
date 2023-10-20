using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeMonster : Monster
{
    // shortRangeMonster는 무기에 대한 클래스를 has a로 가지고 있는다.
    // 무기는 IAttackable을 상속받는다. -> 공격이 가능한 것마다 IAtackable을 상속받는다고 가정한다면
    // 플레이어가 has a로 가지고 있는 Weapon을 그대로사용해도 될 듯
    
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

    // 애니메이션 event 함수
    // 쇼트레인지 몬스터의 무기 콜라이더 껐다 켰다     
    public void WeaponEnable()
    {        
        attackerCol.enabled = true;
    }

    public void WeaponDisable() // 맞을때도 이벤트추가해주기
    {
        attackerCol.enabled = false;
    }    
}
