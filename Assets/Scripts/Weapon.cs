using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IAttackable
{
    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;
        set
        {
            targetLayerMask = value;
        }
    }
    [SerializeField] private LayerMask targetLayerMask;
    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    private float atk;

    public void Attack(IHitable hitable)
    {        
        hitable.Hit(this);
    }

    public void SetAttack(float atk, LayerMask targetLayerMask, int attackNum = 1) // default°ªÁ¤ÇØµÒ
    {
        Atk = atk;
        TargetLayerMask = targetLayerMask;
    }
}
