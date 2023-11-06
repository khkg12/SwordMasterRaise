using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChaseSkill : FixSkill
{
    public override void Use(Character character)
    {
        Transform targetTrans = character.targetCol ? character.targetCol.transform : character.transform;
        GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(targetTrans.position, targetTrans.rotation);
        so.GetComponent<InPlaceObj>().SetAttack((int)(character.Atk * rate), character.TargetLayerMask, skillNum);
    }
}
