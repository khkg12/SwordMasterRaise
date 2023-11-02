using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameObject parent;
    public GameObject prefab;
    public Queue<GameObject> queue;
    public int initSize;    

    public ObjectPool(int size, GameObject obj, GameObject parentObj)
    {
        parent = Instantiate(parentObj);
        int index = parent.name.IndexOf("(Clone)");
        if (index > 0)
        {
            parent.name = parent.name.Substring(0, index); // Clone �ؽ�Ʈ ���� 
            parent.name = obj.name + parent.name;
        }            
        queue = new Queue<GameObject>();
        initSize = size;
        prefab = obj;        
    }

    public GameObject PopObj(Vector3 pos, Quaternion rot) // Ǯ�� �ִ� ������Ʈ�� �̾Ƴ��� �Լ�
    {
        if (queue.Count == 0) // ������Ʈ Ǯ�� ����ִٸ�
        {
            Debug.Log("ť�����");
            CreatePool(initSize / 3);
        }        
        GameObject dequeObj = queue.Dequeue();
        dequeObj.transform.position = pos; // Ȱ��ȭ�Ǳ� �� ��ġ�� ȸ�� ����
        dequeObj.transform.rotation = rot;
        dequeObj.SetActive(true);
        return dequeObj;
    }

    public GameObject PopMonsterObj(Vector3 pos, Quaternion rot) // Ǯ�� �ִ� ���� ������Ʈ�� �̾Ƴ��� �Լ�
    {
        if (queue.Count == 0) // ������Ʈ Ǯ�� ����ִٸ�
        {            
            CreatePool(initSize / 3);
        }
        GameObject dequeObj = queue.Dequeue();
        dequeObj.transform.position = pos; // Ȱ��ȭ�Ǳ� �� ��ġ�� ȸ�� ����
        dequeObj.transform.rotation = rot;
        dequeObj.SetActive(true);
        GameManager.instance.monsterCount++;
        return dequeObj;
    }

    public void ReturnPool(GameObject returnObj) // �پ����� Ǯ�� �ǵ����ִ� �Լ�
    {
        returnObj.SetActive(false);
        queue.Enqueue(returnObj);        
        // ��ġ�ʱ�ȭ�� �����ʿ����, ������ ���ֱ⶧����
    }    

    public void CreatePool(int size) // Ǯ�� �����ϴ� �Լ�
    {
        for (int i = 0; i < size; i++)
        {
            GameObject temp = null;
            temp = Instantiate(prefab, parent.transform);
            int index = temp.name.IndexOf("(Clone)");
            if (index > 0)
                temp.name = temp.name.Substring(0, index); // Clone �ؽ�Ʈ ����            
            // temp.transform.parent = transform; // ���ӿ�����Ʈ �ڽ�����            
            temp.SetActive(false); // ��Ȱ��ȭ            
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
    [SerializeField] GameObject parentObj;
    public static PoolManager instance;
    public Dictionary<string, ObjectPool> objectPoolDic = new Dictionary<string, ObjectPool>();    
    [SerializeField] List<PoolProperty> poolPropertyList;
    const int SKILL_POOL_SIZE = 5;

    void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }
        InitObjectPool();
        InitSkillPool(GameManager.instance.playerSkillList);
    }   
    
    public void InitObjectPool()
    {
        foreach (PoolProperty poolProperty in poolPropertyList) // ����ü
        {
            objectPoolDic.Add(poolProperty.prefab.name, new ObjectPool(poolProperty.size, poolProperty.prefab, parentObj));
            objectPoolDic[poolProperty.prefab.name].CreatePool(poolProperty.size);
        }
    }

    public void InitSkillPool(Skill[] skillList) // �÷��̾� ���� ��ų������Ʈ Ǯ ����
    {        
        foreach(Skill skill in skillList)
        {
            if(skill != null)
            {                                
                objectPoolDic.Add(skill.skillObj.name, new ObjectPool(SKILL_POOL_SIZE, skill.skillObj, parentObj));
                objectPoolDic[skill.skillObj.name].CreatePool(SKILL_POOL_SIZE);
            }            
        }        
    }

    public void InitMonsterPool(StageData stageData, List<GameObject> monsterList) // �Ϲݴ��� �����͸� ���� Ǯ ����
    {
        for (int i = 0; i < stageData.idArr.Length; i++)
        {
            int index = stageData.idArr[i];
            objectPoolDic.Add(monsterList[index].name, new ObjectPool(stageData.countArr[i], monsterList[index], parentObj));
            objectPoolDic[monsterList[index].name].CreatePool(stageData.countArr[i]);
        }
    }

    public void InitAwakeMonsterPool(AwakeStageData stageData, List<GameObject> monsterList) // �������� 
    {
        int index = stageData.bossId;
        objectPoolDic.Add(monsterList[index].name, new ObjectPool(stageData.count, monsterList[index], parentObj));
        objectPoolDic[monsterList[index].name].CreatePool(stageData.count);
    }
}
