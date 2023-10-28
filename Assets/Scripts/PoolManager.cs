using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // public GameObject parent;
    public GameObject prefab;
    public Queue<GameObject> queue;
    public int initSize;    

    public ObjectPool(int size, GameObject obj)
    {
        queue = new Queue<GameObject>();
        initSize = size;
        prefab = obj;        
    }

    public GameObject PopObj(Vector3 pos, Quaternion rot) // 풀에 있는 오브젝트를 뽑아내는 함수
    {
        if (queue.Count == 0) // 오브젝트 풀이 비어있다면
        {
            Debug.Log("큐비었다");
            CreatePool(initSize / 3);
        }        
        GameObject dequeObj = queue.Dequeue();
        dequeObj.transform.position = pos; // 활성화되기 전 위치와 회전 세팅
        dequeObj.transform.rotation = rot;
        dequeObj.SetActive(true);
        return dequeObj;
    }

    public GameObject PopMonsterObj(Vector3 pos, Quaternion rot) // 풀에 있는 몬스터 오브젝트를 뽑아내는 함수
    {
        if (queue.Count == 0) // 오브젝트 풀이 비어있다면
        {            
            CreatePool(initSize / 3);
        }
        GameObject dequeObj = queue.Dequeue();
        dequeObj.transform.position = pos; // 활성화되기 전 위치와 회전 세팅
        dequeObj.transform.rotation = rot;
        dequeObj.SetActive(true);
        GameManager.instance.monsterCount++;
        return dequeObj;
    }

    public void ReturnPool(GameObject returnObj) // 다쓴놈을 풀에 되돌려주는 함수
    {
        returnObj.SetActive(false);
        queue.Enqueue(returnObj);        
        // 위치초기화는 해줄필요없음, 꺼낼때 해주기때문에
    }    

    public void CreatePool(int size) // 풀에 생성하는 함수
    {
        for (int i = 0; i < size; i++)
        {
            GameObject temp = null;
            temp = Instantiate(prefab);
            int index = temp.name.IndexOf("(Clone)");
            if (index > 0)
                temp.name = temp.name.Substring(0, index); // Clone 텍스트 제거            
            // temp.transform.parent = transform; // 게임오브젝트 자식으로            
            temp.SetActive(false); // 비활성화            
            queue.Enqueue(temp);
        }
    }
}

[System.Serializable]
public class PoolProperty
{
    public GameObject prefab;
    public int size;    
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public Dictionary<string, ObjectPool> objectPoolDic = new Dictionary<string, ObjectPool>();    
    [SerializeField] List<PoolProperty> poolPropertyList;

    void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }

        InitObjectPool();
    }   
    
    public void InitObjectPool()
    {
        foreach (PoolProperty poolProperty in poolPropertyList) // 투사체
        {
            objectPoolDic.Add(poolProperty.prefab.name, new ObjectPool(poolProperty.size, poolProperty.prefab));
            objectPoolDic[poolProperty.prefab.name].CreatePool(poolProperty.size);
        }
    }

    public void InitMonsterPool(StageData stageData, List<GameObject> monsterList)
    {
        for (int i = 0; i < stageData.idArr.Length; i++)
        {
            int index = stageData.idArr[i];
            objectPoolDic.Add(monsterList[index].name, new ObjectPool(stageData.countArr[i], monsterList[index]));
            objectPoolDic[monsterList[index].name].CreatePool(stageData.countArr[i]);
        }
    }
}
