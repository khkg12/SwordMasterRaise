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
        range = character.Range; // Idle���¿� ���� �� ���׷��̵� �ǰų� ����� �� ������ (����������) �����Ҷ� �ʱ�ȭ
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
        // �ݶ��̴� �迭 �޸𸮰� �������� ����Ҵ���ٵ� ��� ó���ؾ� �ұ�?
        Collider[] cols = Physics.OverlapSphere(character.transform.position, range, character.TargetLayerMask); // IDLE������ �� ���� Ž��
        //Debug.Log(character.targetLayer);
        if (cols.Length > 0 && NearEnemySearch(cols)) // ���� �����ϸ�
        {            
            character.ChangeStateTag = StateTag.Move; // ������¸� IDLE���� MOVE �� ����
        }
    }

    bool NearEnemySearch(Collider[] cols)
    {
        float minDis = float.MaxValue;
        Collider targetCol = null;
        foreach (Collider col in cols)
        {
            if (col.transform.TryGetComponent(out IHitable hitable)) // �Ÿ��� ���尡���鼭 �������ִ� �������̽��� ��ӹ��� ��
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
        if(character.targetCol == null) // Ÿ���� �׾���.
        {
            character.ChangeStateTag = StateTag.Idle; // ���¸� IDLE�� ����
        }
        else if(Vector3.Distance(character.transform.position, character.targetCol.transform.position) <= TARGET_DISTANCE)
        {
            character.ChangeStateTag = StateTag.Attack; // ���¸� attack���� ����
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
        character.AniTag = AnimationTag.Idle; // �������� �� Idle
        character.AttackStart(); // ���� �ڷ�ƾ ���� 
    }

    public override void Exit()
    {        
        character.AttackEnd(); // ���� �ڷ�ƾ ����
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

// �÷��̾� �ڵ����� fsm
public class Fsm
{
    State currentState;
    Dictionary<StateTag, State> dicState = new Dictionary<StateTag, State>();    
    
    
    public void Update()
    {
        currentState.Update();
    }

    public void AddState(StateTag tag, State state) // ���¸ӽ� dicState�� ���� �߰�(����) �Լ�
    {
        dicState[tag] = state;
    }

    public void ChangeState(StateTag tag)
    {
        currentState?.Exit();        
        currentState = dicState[tag]; // ������ tag�� ��ųʸ� state�� ������¸� ����
        currentState.Enter(); // ����Ǿ����� �� ������ ���Ա�� ���� 
    }
}
