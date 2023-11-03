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

// ���ȿ� ���� ������ ���� -> ex) hp, �ʿ���, ������
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
    public int id; // �������� ���̵�    
    public int[] idArr;
    public int[] countArr;
    public int rewardGold;
    public int rewardExp;
}

[System.Serializable]
public class AwakeStageData
{
    public string stageName;
    public int id; // �������� ���̵�    
    public int bossId;
    public int count; // ���� ��
    public string explanation;
}

[System.Serializable]
public class ItemInfo
{
    public string itemName;
    public bool isHave;
    public float atkRate; // ���ݷ� ������
    public float upgradeGold; // ��ȭ �� �ʿ��� ���
    public int upgradeCount; // ���׷��̵� ��ġ
    public int itemCount;
    public float itemWeight; // �̱�Ȯ���� ���� ������ ����ġ
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

[System.Serializable]
public class SpiritData
{
    public int level;
    public float rate;
    public float increaseAmount;
    public int requireGold;
}

public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    [SerializeField] private TextAsset spiritDataFile;
    [SerializeField] private TextAsset stageDataFile;
    [SerializeField] private TextAsset awakeStageDataFile;
    [SerializeField] private TextAsset itemDataFile;
    [SerializeField] private TextAsset soldierDataFile;
    [SerializeField] private Sprite[] itemSprite = new Sprite[16];    
    string playerDataPath;
    string itemDataPath;
    string soldierDataPath;
    string spiritDataPath;

    public PlayerData playerData;  
    public SpiritData spiritData;
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
            while(playerData.exp > playerData.maxExp) // �ѹ��� ���������� �ø� �� �ִ°�쵵 ������
            {
                playerData.exp -= playerData.maxExp; // �������ϰ� ���� �� ä���
                playerData.level++; // ������
                playerData.maxExp = (int)(1.5f * playerData.maxExp);                
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
        playerDataPath = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // ���� ��� ����
        itemDataPath = Path.Combine(Application.dataPath + "/Resources/", "itemData.json"); 
        soldierDataPath = Path.Combine(Application.dataPath + "/Resources/", "soldierData.json"); 
        spiritDataPath = Path.Combine(Application.dataPath + "/Resources/", "spiritData.json"); 
        SetData(); // json���� �ҷ��� playerData�� ���� �ʱ�ȭ
        SetStageData();
        SetItemData();
        SetSoldierData();
    }    
    
    public void SetData()
    {
        // json�� ���� ��� �ʱ�ȭ���ִ� �� �ֱ�
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json������ ������ȭ�� ����������
        spiritData = JsonConvert.DeserializeObject<SpiritData>(spiritDataFile.text); 
    }

    public void SetItemData()
    {        
        itemDataArr = JsonConvert.DeserializeObject<ItemInfo[]>(itemDataFile.text); // �����۵����� ����
        for(int i = 0; i < itemDataArr.Length; i++) // ��ųʸ��� ����
        {                        
            itemSpriteDic[itemDataArr[i].itemName] = itemSprite[i];
        }
    }

    public void SetSoldierData()
    {
        soldierDataArr = JsonConvert.DeserializeObject<SoldierInfo[]>(soldierDataFile.text); // ���� ������ ����
    }

    public void SetStageData()
    {
        stageDataArr = JsonConvert.DeserializeObject<StageData[]>(stageDataFile.text);
        awakeStageDataArr = JsonConvert.DeserializeObject<AwakeStageData[]>(awakeStageDataFile.text);
    }

    public void SavePlayerData() // PlayerTable�� �����͸� json���� ���� 
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

    public void SaveSpiritData()
    {
        var jsonData = JsonConvert.SerializeObject(spiritData, Formatting.Indented);
        File.WriteAllText(spiritDataPath, jsonData);
    }
}

