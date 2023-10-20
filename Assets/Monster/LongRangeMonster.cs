using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeMonster : Monster
{
    [SerializeField] GameObject projectileObj;
    public override void AttackStart()
    {
        SetForward();
        animator.SetTrigger("AttackTrigger");        
        GameObject po = Instantiate(projectileObj, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
        po.GetComponent<ProjectileObj>().SetRotate(transform);
        po.GetComponent<ProjectileObj>().SetAttack(Atk, TargetLayerMask);
    }
}
