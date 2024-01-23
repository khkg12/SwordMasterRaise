using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using TMPro;

[AttributeUsage(AttributeTargets.Field)]
public class BossUiViewAttribute : Attribute { }

public class BossStatusUI : MonoBehaviour
{    
    [SerializeField] GameObject slotPrefab;
    List<FieldInfo> fields;    
    public static Monster nowMonster;
    
    void Start()
    {        
        fields = new List<FieldInfo>(); 
        Type mType = typeof(Monster);
        var fieldInfos = mType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic); // private �������� ������

        foreach(FieldInfo fieldInfo in fieldInfos) // ������ �����鿡 Ŀ������ ��Ʈ����Ʈ�� �پ������� ����Ʈ�� �߰�
        {
            BossUiViewAttribute bossUiView = fieldInfo.GetCustomAttribute<BossUiViewAttribute>();       
            if(bossUiView != null)
                fields.Add(fieldInfo);
        }
        foreach (FieldInfo fieldInfo in fields)
        {
            GameObject slotObj = Instantiate(slotPrefab, transform); // ���� ���� �������� �����ѵ� ������ �̸��� ������ ����
            slotObj.GetComponent<StatusSlot>().SetSlot(fieldInfo.Name, fieldInfo.GetValue(nowMonster));            
        }
        StartCoroutine(DisappearCo());
    }    

    IEnumerator DisappearCo()
    {        
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
