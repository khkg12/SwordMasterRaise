using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{    
    void Start()
    {        
        SoldierSpawn();
    }
    
    public void SoldierSpawn()
    {
        GameObject soldier = GameManager.instance.SoliderObj;
        if (soldier != null)
        {
            Instantiate(soldier, new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
        }
    }
}
