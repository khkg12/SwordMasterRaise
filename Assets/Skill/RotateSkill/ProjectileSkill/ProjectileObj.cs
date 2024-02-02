using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileObj : MonoBehaviour, IAttackable, IPauseable
{
    // 고민 이유 : 플레이어이나 몬스터가 맞을 때 trigger를 체크하려면 그 놈이 IAttackable이여야 하는데, 그렇다면 projectileObj도 IAttackable을 상속해야한다    
    
    // 스킬중 다수타격이 있을 시 맞을 때 여러번의 대미지를 주어야하는데 그 처리를 어떻게 해야할까? -> 다수타격 스킬의 콜라이더를 대미지를 주는 텀마다 껏다켰다?
    // -> 위처럼 모든 공격객체들 또한 IAttackable을 상속받는다면 Attack()을 각각 설정해줄수있다. 즉 맞을 때 IAttackable의 Attack을 실행하면 각기 다른 기능이 호출될 것    

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

    public int AttackNum
    {
        get => attackNum;
        set
        {
            attackNum = value;
        }
    }
    private int attackNum;

    // 스킬에서 날아가는 속도와 생존시간을 넘겨줄지, 아니면 날아가는 속도와 생존시간을 projectileObj에서 정해둘지
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        RegistHandler();
    }

    private void OnEnable()
    {        
        // 몇초뒤 사라지는 코루틴, 사라질 때 풀링에 다시 담는것까지
        StartCoroutine(startLifeTimeCo());
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed); // 매직변수 처리, 세팅해줄것 투사체마다 속도가 다를테니
    }
    
    public void SetRotate(Transform userTrans)
    {
        transform.forward = userTrans.transform.forward;
    }    

    public void Attack(IHitable hitable)
    {        
        for(int i = 0; i < AttackNum; i++) // 코루틴으로 바꾸기
        {            
            hitable.Hit(this);
        }        
    }    

    public void SetAttack(float atk, LayerMask targetLayerMask, int attackNum)
    {
        Atk = atk;
        TargetLayerMask = targetLayerMask;
        AttackNum = attackNum;  
    }

    IEnumerator startLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);        
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // 풀에 돌려줌
    }

    public void RegistHandler()
    {
        PauseManager.instance.onPause += () => { enabled = false; };
        PauseManager.instance.onResume += () => { enabled = true; };
    }
}

