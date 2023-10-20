using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour // 추상클래스는 미완성이기 때문에 객체로 찍어낼수없다. 즉 실수로라도 item을 넣는 것을 방지한다.
{
    // 아이템은 사용할수있는 놈과 없는 놈이 구분될 수 있다. 
    public Sprite sprite;
    public string name;
    public int price;

    public abstract void Use(PlayerSc player);    
}
