using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] Image hitImage;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] float y_offset;
    const float lifeTime = 0.5f;
    public float Damage // 지금 보석이랑 같이쓰고있기 때문에 이름 알맞게 바꿔주기
    {
        get => damage;
        set
        {
            damage = value;
            damageText.text = damage.ToString();
        }
    }
    float damage;

    public Color Color
    {
        get => damageText.color;
        set
        {
            damageText.color = value;
        }
    }

    Vector3 textPos => new Vector3(transform.position.x, y_offset, transform.position.z);
    

    private void OnEnable()
    {
        transform.position = textPos; // 활성화할 때 위치정해줌, 랜덤값넣기
        hitImage.color = new Color(1, 1, 1, 1);
        StartCoroutine(DisappearCo());
    }

    IEnumerator DisappearCo() // 닷트윈으로 바꿀수있음 바꾸기, 수정할것
    {
        float nowTime = 0;
        while (nowTime < lifeTime)
        {
            nowTime += Time.deltaTime;
            Color tempColor = damageText.color;
            tempColor.a = (lifeTime - nowTime) / lifeTime;
            damageText.color = tempColor;
            tempColor = hitImage.color;
            tempColor.a = (lifeTime - nowTime) / lifeTime;
            hitImage.color = tempColor;
            yield return null;
        }        
        PoolManager.instance.objectPoolDic[gameObject.name].ReturnPool(gameObject); // 풀에 돌려줌
    }

    
}
