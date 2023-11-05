using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    Vector3 spawnPos = new Vector3(1,0,1);
    void Start()
    {        
        SoldierSpawn();
    }
    
    public void SoldierSpawn()
    {
        GameObject soldier = GameManager.instance.soldierObj;
        if (soldier != null)
        {
            Instantiate(soldier, spawnPos, Quaternion.identity);            
        }
    }
}
