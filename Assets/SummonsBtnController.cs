using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item����Ʈ 
    public ItemInventoryUI itemInven;
    [SerializeField] Image summonUI;
    public void Summons()
    {
        // Ȯ�� ����� � �������� ������ ����
        // item�� ���
        // ���߿� �ڷ�ƾ���� �ٲٰ� �����߰��ϱ�
        summonUI.gameObject.SetActive(true);
        // GetItem(DataManager.instance.itemDataArr[0]); // �ӽü���
    }    
}
