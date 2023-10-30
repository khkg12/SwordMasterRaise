using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordAuror : RotateSkill
{
    public override void Use(Character character)
    {                
        GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(character.transform.position, character.transform.rotation);        
        so.GetComponent<ProjectileObj>().SetRotate(character.transform); // ������ ������� ó�����ִϱ� �ʿ������?
        so.GetComponent<ProjectileObj>().SetAttack((int)(character.Atk * rate), character.TargetLayerMask);
    }        
}
