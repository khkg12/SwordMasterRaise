using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Player player;
    void Awake()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Init() // Player�� �������ִ°� �� ��������? 
    {
        player.Hp = GameManager.instance.hp;
        player.Atk = GameManager.instance.atk;        
    }
}
