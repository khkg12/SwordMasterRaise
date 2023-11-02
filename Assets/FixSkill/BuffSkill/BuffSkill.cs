using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : FixSkill
{
    public override void Use(Character character)
    {
        GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(character.transform.position, Quaternion.identity);        
        so.GetComponent<BuffObj>().SetRecovery((int)(character.MaxHp * rate), character, skillNum); // 세팅 후
        so.GetComponent<BuffObj>().StartRecoveryCo(); // 코루틴 시작
    }
}
