using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordBaldo : FixSkill
{
    public override void Use(Character character)
    {        
        // GameObject so = PoolManager.instance.objectPoolDic[skillObj.name].PopObj(character.transform.position, character.transform.rotation);
        GameObject so = Instantiate(skillObj, character.transform.position, character.transform.rotation);        
        so.GetComponent<InPlaceObj>().SetAttack((int)(character.Atk * rate), character.TargetLayerMask, attackNum); 
    }
}
