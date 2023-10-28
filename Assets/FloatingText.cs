using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
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

    public Color Color
    {
        get => damageText.color;
        set
        {
            damageText.color = value;
        }
    }

    Vector3 textPos => new Vector3(transform.position.x, 1.4f, transform.position.z);

    private void OnEnable()
    {
        transform.position = textPos; // Ȱ��ȭ�� �� ��ġ������, �������ֱ�
        StartCoroutine(DisappearCo());
    }    

    IEnumerator DisappearCo() // ��Ʈ������ �ٲܼ����� �ٲٱ�
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
        PoolManager.instance.objectPoolDic["DamageText"].ReturnPool(gameObject); // Ǯ�� ������
    }
}
