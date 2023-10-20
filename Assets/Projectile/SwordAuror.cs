using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordAuror : RotateSkill
{
    public override void Use(Character character)
    {
        Debug.Log("�ҵ���� ����");
        GameObject so = Instantiate(skillObj, character.transform.position, Quaternion.identity);        
        so.GetComponent<ProjectileObj>().SetRotate(character.transform);
        so.GetComponent<ProjectileObj>().SetAttack(character.Atk, character.TargetLayerMask);
    }    
}
