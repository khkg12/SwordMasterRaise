using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Spirit : MonoBehaviour
{
    public float recoveryRate;
    [SerializeField] Character character;
    
    void Start()
    {
        recoveryRate = DataManager.instance.spiritData.rate;
        StartCoroutine(RecoveryCo());
    }
    
    IEnumerator RecoveryCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // ĳ���� �ʱ�ȭ�� start���� ���ֱ⶧���� ü���� 0�̿��� �ٷ��״¿�������, ���߿� awake�����ϰų� �̰ɼ����ϰų� �ؾ��ҵ�
            character.RecoveryHp(recoveryRate);
        }        
    }
}
