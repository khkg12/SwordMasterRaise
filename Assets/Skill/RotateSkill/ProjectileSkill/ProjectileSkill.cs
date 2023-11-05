using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileSkill : RotateSkill
{
    const float Y_OFFSET = 0.5f;
    public override void Use(Character character)
    {
        Vector3 spawnPos = new Vector3(character.transform.position.x, character.transform.position.y + Y_OFFSET, character.transform.position.z); // 소드스트라이크 처리처럼 하기
        GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(spawnPos, character.transform.rotation);
        so.GetComponent<ProjectileObj>().SetRotate(character.transform); // 위에서 방향까지 처리해주니까 필요없을듯?
        so.GetComponent<ProjectileObj>().SetAttack((int)(character.Atk * rate), character.TargetLayerMask, skillNum);
    }
}
