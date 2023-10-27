using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    const float lifeTime = 0.5f;
    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            damageText.text = damage.ToString();
        }
    }
    float damage;

    private void OnEnable()
    {
        StartCoroutine(DisappearCo());
    }    

    IEnumerator DisappearCo() // 닷트윈으로 바꿀수있음 바꾸기
    {
        float nowTime = 0;
        while (nowTime < lifeTime)
        {
            nowTime += Time.deltaTime;
            Color tempColor = damageText.color;
            tempColor.a = (lifeTime - nowTime) / lifeTime;
            damageText.color = tempColor;
            yield return null;
        }
        PoolManager.instance.objectPoolDic["DamageText"].ReturnPool(gameObject); // 풀에 돌려줌
    }
}
