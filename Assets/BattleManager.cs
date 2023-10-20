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

    public void Init() // Player가 가지고있는게 더 좋을지도? 
    {
        player.Hp = GameManager.instance.hp;
        player.Atk = GameManager.instance.atk;        
    }
}
