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
            yield return new WaitForSeconds(1f); // 캐릭터 초기화를 start에서 해주기때문에 체력이 0이여서 바로죽는오류생김, 나중에 awake에서하거나 이걸수정하거나 해야할듯
            character.RecoveryHp(recoveryRate);
        }        
    }
}
