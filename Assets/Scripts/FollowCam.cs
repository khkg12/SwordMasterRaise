using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Button changeVisionBtn;
    [SerializeField] Transform target;
    const float TOPVIEW_DISTANCE_Y = 4;
    const float TOPVIEW_DISTANCE_Z = 3;
    const float TOPVIEW_ANGLE_X = 50;
    const float FIRSTPERSON_VIEW_DISTANCE_Y = 1.5f;
    const float FIRSTPERSON_VIEW_DISTANCE_Z = 2f;
    const float FIRSTPERSON_VIEW_ANGLE_X = 25;    

    public bool IsEnabled
    {
        get => enabled;
        set
        {
            enabled = value;
        }
    }
    
    private bool isToggle = true;

    void Update() // �̰� �׼ǽᵵ�Ǵ��� �ùٸ� ��������� �����
    {
        if (isToggle)
            TopViewView();
        else
            FirstPersonView();
    }

    public void ChangeView() // ��ư Ŭ�� �� ����� ���
    {
        isToggle = !isToggle;
    }

    public void TopViewView()
    {
        transform.position = new Vector3(target.position.x, TOPVIEW_DISTANCE_Y, target.position.z - TOPVIEW_DISTANCE_Z);
        transform.eulerAngles = new Vector3(TOPVIEW_ANGLE_X, 0, 0);
    }

    public void FirstPersonView()
    {
        transform.position = new Vector3(target.position.x, FIRSTPERSON_VIEW_DISTANCE_Y, target.position.z - FIRSTPERSON_VIEW_DISTANCE_Z);
        transform.eulerAngles = new Vector3(FIRSTPERSON_VIEW_ANGLE_X, 0, 0);
    }
}
