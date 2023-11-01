using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{    
    LayerMask TargetLayerMask { get; set; }
    float Atk {  get; set; }
    void Attack(IHitable hitable);
    void SetAttack(float atk, LayerMask targetLayerMask, int attackNum);
}

public interface IHitable
{
    bool IsHit { get; set; }
    float Hp {  get; set; }
    void Hit(IAttackable attackable); 
}