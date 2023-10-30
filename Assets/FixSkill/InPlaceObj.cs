using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlaceObj : MonoBehaviour, IAttackable
{
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
    
    [SerializeField] private float lifeTime;

    private void Start()
    {        
        // StartCoroutine(startLifeTimeCo());
        // 디스트로이 코루틴 사용안하는이유는 파티클에서 삭제호출하기때문
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

    IEnumerator startLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
