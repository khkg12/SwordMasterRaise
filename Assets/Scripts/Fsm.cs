using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class State
{
    protected Character character;
    public State(Character character)
    {
        this.character = character;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : State
{
    float range;
    public IdleState(Character character) : base(character) { }

    public override void Enter()
    {        
        range = character.Range; // Idle상태에 들어올 때 업그레이드 되거나 변경될 수 있으니 (감지범위가) 진입할때 초기화
        character.AniTag = AnimationTag.Idle;        
    }

    public override void Exit()
    {        
    }

    public override void Update()
    {
        DetectEnemy();
    }    

    void DetectEnemy()
    {
        // 콜라이더 배열 메모리가 힙영역에 계속할당될텐데 어떻게 처리해야 할까?
        Collider[] cols = Physics.OverlapSphere(character.transform.position, range, character.TargetLayerMask); // IDLE상태일 때 적을 탐지
        //Debug.Log(character.targetLayer);
        if (cols.Length > 0 && NearEnemySearch(cols)) // 적이 존재하면
        {            
            character.ChangeStateTag = StateTag.Move; // 현재상태를 IDLE에서 MOVE 로 변경
        }
    }

    bool NearEnemySearch(Collider[] cols)
    {
        float minDis = float.MaxValue;
        Collider targetCol = null;
        foreach (Collider col in cols)
        {
            if (col.transform.TryGetComponent(out IHitable hitable)) // 거리가 가장가까우면서 맞을수있는 인터페이스를 상속받은 놈만
            {                
                float dis = (character.transform.position - col.transform.position).magnitude;
                if (minDis > dis) 
                {
                    minDis = dis;
                    targetCol = col;
                }                
            }            
        }
        if(targetCol != null)
        {            
            character.targetCol = targetCol;
            return true;
        }
        return false;
    }
}

public class MoveState : State
{
    float moveSpeed;
    const float TARGET_DISTANCE = 2;
    public MoveState(Character character) : base(character) { }    

    public override void Enter()
    {
        moveSpeed = character.MoveSpeed;
        character.AniTag = AnimationTag.Move;
    }

    public override void Exit()
    {
      
    }

    public override void Update()
    {
        if(character.targetCol == null) // 타겟이 죽었다.
        {
            character.ChangeStateTag = StateTag.Idle; // 상태를 IDLE로 변경
        }
        else if(Vector3.Distance(character.transform.position, character.targetCol.transform.position) <= TARGET_DISTANCE)
        {
            character.ChangeStateTag = StateTag.Attack; // 상태를 attack으로 변경
        }
        else
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, character.targetCol.transform.position, moveSpeed);
            character.SetForward();
        }        
    }
}

public class AttackState : State
{
    const float NONE_TARGET_DISTANCE = 2.5f;
    public AttackState(Character character) : base(character) { }    

    public override void Enter()
    {        
        character.AniTag = AnimationTag.Idle; // 진입했을 때 Idle
        character.AttackStart(); // 공격 코루틴 시작 
    }

    public override void Exit()
    {        
        character.AttackEnd(); // 공격 코루틴 종료
    }

    public override void Update()
    {        
        if (character.targetCol == null)
        {
            character.ChangeStateTag = StateTag.Idle;            
        }
        else
        {
            character.SetForward();
            float distance = Vector3.Distance(character.transform.position, character.targetCol.transform.position);
            if (distance > NONE_TARGET_DISTANCE)
            {                
                character.ChangeStateTag = StateTag.Idle;
            }
        }        
    }
}

public class SkillState : State
{
    float skillDamageAmount;
    float durationTime;
    public SkillState(Character character) : base(character) { }    

    public override void Enter()
    {
        durationTime = 0;
        character.currentSkill.Use(character);                
    }

    public override void Exit()
    {        
        character.currentSkill = null;  
    }

    public override void Update()
    {
        durationTime += Time.deltaTime;
        if(durationTime > 0.5f)
        {
            character.ChangeStateTag = StateTag.Idle;
        }        
    } 
}

// 플레이어 자동전투 fsm
public class Fsm
{
    State currentState;
    Dictionary<StateTag, State> dicState = new Dictionary<StateTag, State>();    
    
    
    public void Update()
    {
        currentState.Update();
    }

    public void AddState(StateTag tag, State state) // 상태머신 dicState에 상태 추가(정의) 함수
    {
        dicState[tag] = state;
    }

    public void ChangeState(StateTag tag)
    {
        currentState?.Exit();        
        currentState = dicState[tag]; // 변경할 tag의 딕셔너리 state로 현재상태를 변경
        currentState.Enter(); // 변경되었으니 그 상태의 진입기능 실행 
    }
}
