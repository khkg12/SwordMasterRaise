using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LongRangeMonster : Monster
{
    [SerializeField] GameObject projectileObj;
    public override void AttackStart()
    {
        SetForward();        
        animator.SetTrigger("AttackTrigger");        
        GameObject po = PoolManager.instance.objectPoolDic[projectileObj.name].PopObj(transform.position, transform.rotation);        
        po.GetComponent<ProjectileObj>().SetRotate(transform);
        po.GetComponent<ProjectileObj>().SetAttack(Atk, TargetLayerMask, 1); // 매직변수 처리
    }
}
