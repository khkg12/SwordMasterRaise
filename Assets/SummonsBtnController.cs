using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonsBtnController : MonoBehaviour
{
    // item����Ʈ 
    public ItemInventoryUI itemInven;
    [SerializeField] SummonsUI summonUI;
    public void WeaponSummons()
    {
        // Ȯ�� ����� � �������� ������ ����
        // item�� ���
        // ���߿� �ڷ�ƾ���� �ٲٰ� �����߰��ϱ�
        // ���� ������ ����Ʈ ���⼭ ��������
        
        summonUI.gameObject.SetActive(true);
        // GetItem(DataManager.instance.itemDataArr[0]); // �ӽü���
    }    

}
