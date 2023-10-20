using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileObj : MonoBehaviour, IAttackable
{
    // 고민 이유 : 플레이어이나 몬스터가 맞을 때 trigger를 체크하려면 그 놈이 IAttackable이여야 하는데, 그렇다면 projectileObj도 IAttackable을 상속해야한다    
    
    // 스킬중 다수타격이 있을 시 맞을 때 여러번의 대미지를 주어야하는데 그 처리를 어떻게 해야할까? -> 다수타격 스킬의 콜라이더를 대미지를 주는 텀마다 껏다켰다?
    // -> 위처럼 모든 공격객체들 또한 IAttackable을 상속받는다면 Attack()을 각각 설정해줄수있다. 즉 맞을 때 IAttackable의 Attack을 실행하면 각기 다른 기능이 호출될 것

    Vector3 rotateVec;

    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;
        set
        {
            targetLayerMask = value;
        }
    }
    private LayerMask targetLayerMask;

    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    private float atk;

    private void Start()
    {
        transform.forward = rotateVec;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * 0.1f); // 매직변수 처리, 세팅해줄것 투사체마다 속도가 다를테니
    }
    
    public void SetRotate(Transform userTrans)
    {        
        rotateVec = userTrans.transform.forward;
    }    

    public void Attack(IHitable hitable)
    {
        hitable.Hit(this);
    }

    public void SetAttack(float atk, LayerMask targetLayerMask)
    {
        Atk = atk;
        TargetLayerMask = targetLayerMask;
    }
}

