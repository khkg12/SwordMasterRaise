using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{        
    public List<Skill> skillList; // 모든스킬 담고있는 스킬리스트
    public Skill[] playerSkillList; // 플레이어가 배틀 시 사용할 스킬배열
    public ItemInfo equipItemInfo; // 플레이어가 장착할 아이템, 프로퍼티로 처리해야할수도, 데이터저장하려면 DataManager가 가지고있어야하나?
    public int monsterCount;

    public int BossCount
    {
        get => BossCount;
        set
        {
            bossCount = value;
            if(bossCount == 0) // 보스를 다잡았다면
            {
                UpgradeAwaken(); // 각성레벨올리고                      
            }
        }
    }
    private int bossCount;

    public void UpgradeAwaken()
    {
        DataManager.instance.playerData.awakeLevel++;
    }

    new void Awake()
    {
        base.Awake();        
    }                
}
