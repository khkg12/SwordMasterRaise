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
        var fieldInfos = mType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic); // private 변수들을 가져옴

        foreach(FieldInfo fieldInfo in fieldInfos) // 가져온 변수들에 커스텀한 어트리뷰트가 붙어있으면 리스트에 추가
        {
            BossUiViewAttribute bossUiView = fieldInfo.GetCustomAttribute<BossUiViewAttribute>();       
            if(bossUiView != null)
                fields.Add(fieldInfo);
        }
        foreach (FieldInfo fieldInfo in fields)
        {
            GameObject slotObj = Instantiate(slotPrefab, transform); // 스탯 슬롯 프리팹을 생성한뒤 변수의 이름과 값으로 세팅
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
