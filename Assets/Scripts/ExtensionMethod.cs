using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethod
{
    public static void SetDirection(this Transform ownerTrans, ref Transform targetTrans)
    {
        Vector3 dirVec = (new Vector3(ownerTrans.position.x, targetTrans.position.y, ownerTrans.position.z) - targetTrans.position).normalized;
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
