using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour // �߻�Ŭ������ �̿ϼ��̱� ������ ��ü�� ��������. �� �Ǽ��ζ� item�� �ִ� ���� �����Ѵ�.
{
    // �������� ����Ҽ��ִ� ��� ���� ���� ���е� �� �ִ�. 
    public Sprite sprite;
    public string name;
    public int price;

    public abstract void Use(PlayerSc player);    
}
