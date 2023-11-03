using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    Queue<Vector3> playerPosQueue = new Queue<Vector3>();       

    void Update()
    {
        if ((target.position - transform.position).magnitude >= 2f) playerPosQueue.Enqueue(target.position);
        if (playerPosQueue.Count >= 10) // 10�� ��������, �ǹ��ϴ� �ٴ� Ÿ�ٰ��� �����Ÿ�
        {
            Vector3 pos = playerPosQueue.Dequeue();
            pos.y = transform.position.y;
            // transform.forward = (pos - transform.position);
            transform.LookAt(pos);
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.01f);
        }
    }
}
