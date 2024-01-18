using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethod
{
    public static void SetDirection(this Transform ownerTrans, Transform targetTrans)
    {
        Vector3 dirVec = (new Vector3(targetTrans.position.x, ownerTrans.position.y, targetTrans.position.z) - ownerTrans.position).normalized;
        ownerTrans.forward = dirVec;        
    }

    public static void SetDirection(this Transform ownerTrans, Vector3 targetPos) // 오버로딩
    {
        Vector3 dirVec = (new Vector3(targetPos.x, ownerTrans.position.y, targetPos.z) - ownerTrans.position).normalized;
        ownerTrans.forward = dirVec;
    }

    public static float GetRandomDamageOffset(this float atk)
    {
        const float OFFSET_RATE = 0.1f;
        float offset = atk * OFFSET_RATE;
        atk = (int)Random.Range(atk - offset, atk + offset);
        return atk;
    }    
}
