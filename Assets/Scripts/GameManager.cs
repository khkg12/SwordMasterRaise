using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int atk;
    public int hp;
    public int level;
    public int gold;    
}

public class GameManager : Singleton<GameManager>
{    
    public int atk;
    public int hp;
    public int Level
    {
        get => level;
        set
        {
            level = value;
            if(level >= 10)
            {
                // 스킬개방
            }
            if (level >= 20)
            {

            }
            if (level >= 30)
            {

            }
            if (level >= 40)
            {

            }
        }
    }
    private int level;
    public int gold;
    public int upgradeHpGold;
    public int upgradeAtkGold;
    public int upgradeCritical;

    new void Awake()
    {
        base.Awake();        
    }
    private void Start()
    {
        DataInit(); // 데이터 초기화
    }

    public void DataInit()
    {
        atk = DataManager.instance.playerData.atk;
        hp = DataManager.instance.playerData.hp;
        Level = DataManager.instance.playerData.level;
        gold = DataManager.instance.playerData.gold;
    }

}
