using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileSkill : RotateSkill
{
    const float Y_OFFSET = 0.5f;
    public override void Use(Character character)
    {
        Vector3 spawnPos = new Vector3(character.transform.position.x, character.transform.position.y + Y_OFFSET, character.transform.position.z); 
        GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(spawnPos, character.transform.rotation);        
        so.GetComponent<ProjectileObj>().SetAttack((int)(character.Atk * rate), character.TargetLayerMask, skillNum);
    }
}

