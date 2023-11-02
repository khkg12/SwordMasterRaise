using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int awakeLevel;
    public int level;
    public int exp;
    public int maxExp;
    public int gold;
    public StatInfo hp;
    public StatInfo atk;
    public StatInfo speed;
}

// 스탯에 대한 정보를 정리 -> ex) hp, 필요골드, 증가량
[System.Serializable]
public class StatInfo
{
    public string name;
    public float stat;
    public int requireGold;
    public float increaseAmount;
}

[System.Serializable]
public class StageData
{
    public string stageName;
    public int id; // 스테이지 아이디    
    public int[] idArr;
    public int[] countArr;
    public int rewardGold;
    public int rewardExp;
}

[System.Serializable]
public class AwakeStageData
{
    public string stageName;
    public int id; // 스테이지 아이디    
    public int bossId;
    public int count; // 보스 수
    public string explanation;
}

[System.Serializable]
public class ItemInfo
{
    public string itemName;
    public bool isHave;
    public float atkRate; // 공격력 증가율
    public float upgradeGold; // 강화 시 필요한 골드
    public int upgradeCount; // 업그레이드 수치
    public int itemCount;
    public float itemWeight; // 뽑기확률을 위한 아이템 가중치
}

[System.Serializable]
public class SoldierInfo
{
    public string soldierName;
    public string explanation;
    public bool isHave;
    public int requireGold;
    public int id;
    public void Set()
    {
        Debug.Log("adasd");
    }
}

public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    [SerializeField] private TextAsset stageDataFile;
    [SerializeField] private TextAsset awakeStageDataFile;
    [SerializeField] private TextAsset itemDataFile;
    [SerializeField] private TextAsset soldierDataFile;
    [SerializeField] private Sprite[] itemSprite = new Sprite[16];    
    string playerDataPath;
    string itemDataPath;
    string soldierDataPath;

    public PlayerData playerData;        
    public StageData[] stageDataArr;
    public StageData currentStageData;
    public AwakeStageData[] awakeStageDataArr;    
    public AwakeStageData currentAwakeStageData;
    public ItemInfo[] itemDataArr;
    public SoldierInfo[] soldierDataArr;

    public Dictionary<string, Sprite> itemSpriteDic = new Dictionary<string, Sprite>();
    

    public int Exp
    {
        get => playerData.exp;
        set
        {
            playerData.exp = value;
            if(playerData.exp >= playerData.maxExp)
            {
                playerData.maxExp = (int)(1.1f * playerData.maxExp);
                playerData.exp -= playerData.maxExp; // 레벨업하고 남은 양 채우기
                playerData.level++; // 레벨업
            }
        }
    }

    public int Gold
    {
        get => playerData.gold;
        set
        {
            playerData.gold = value;    
        }
    }    

    new void Awake()
    {
        base.Awake();
        playerDataPath = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // 저장 경로 설정
        itemDataPath = Path.Combine(Application.dataPath + "/Resources/", "itemData.json"); // 저장 경로 설정
        soldierDataPath = Path.Combine(Application.dataPath + "/Resources/", "soldierData.json"); // 저장 경로 설정
        SetData(); // json에서 불러와 playerData에 저장 초기화
        SetStageData();
        SetItemData();
        SetSoldierData();
    }    
    
    public void SetData()
    {
        // json이 없을 경우 초기화해주는 것 넣기
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json파일을 역직렬화로 데이터저장        
    }

    public void SetItemData()
    {        
        itemDataArr = JsonConvert.DeserializeObject<ItemInfo[]>(itemDataFile.text); // 아이템데이터 저장
        for(int i = 0; i < itemDataArr.Length; i++) // 딕셔너리도 세팅
        {                        
            itemSpriteDic[itemDataArr[i].itemName] = itemSprite[i];
        }
    }

    public void SetSoldierData()
    {
        soldierDataArr = JsonConvert.DeserializeObject<SoldierInfo[]>(soldierDataFile.text); // 솔져 데이터 저장
    }

    public void SetStageData()
    {
        stageDataArr = JsonConvert.DeserializeObject<StageData[]>(stageDataFile.text);
        awakeStageDataArr = JsonConvert.DeserializeObject<AwakeStageData[]>(awakeStageDataFile.text);
    }

    public void SavePlayerData() // PlayerTable의 데이터를 json으로 저장 
    {
        var jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);        
        File.WriteAllText(playerDataPath, jsonData);        
    }

    public void SaveItemData()
    {
        var jsonData = JsonConvert.SerializeObject(itemDataArr, Formatting.Indented);        
        File.WriteAllText(itemDataPath, jsonData);
    }

    public void SaveSoldierData()
    {
        var jsonData = JsonConvert.SerializeObject(soldierDataArr, Formatting.Indented);
        File.WriteAllText(soldierDataPath, jsonData);
    }   
}

