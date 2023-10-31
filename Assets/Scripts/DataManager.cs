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

public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    [SerializeField] private TextAsset stageDataFile;
    [SerializeField] private TextAsset awakeStageDataFile;
    [SerializeField] private TextAsset itemDataFile;
    [SerializeField] private Sprite[] itemSprite = new Sprite[16];    
    string path;

    public PlayerData playerData;        
    public StageData[] stageDataArr;
    public StageData currentStageData;
    public AwakeStageData[] awakeStageDataArr;    
    public AwakeStageData currentAwakeStageData;
    public ItemInfo[] itemDataArr;    

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
                playerData.exp -= playerData.maxExp; // �������ϰ� ���� �� ä���
                playerData.level++; // ������
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
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // ���� ��� ����
        SetData(); // json���� �ҷ��� playerData�� ���� �ʱ�ȭ
        SetStageData();
        SetItemData();        
    }    
    
    public void SetData()
    {
        // json�� ���� ��� �ʱ�ȭ���ִ� �� �ֱ�
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json������ ������ȭ�� ����������        
    }

    public void SetItemData()
    {        
        itemDataArr = JsonConvert.DeserializeObject<ItemInfo[]>(itemDataFile.text); // �����۵����� ����
        for(int i = 0; i < itemDataArr.Length; i++) // ��ųʸ��� ����
        {                        
            itemSpriteDic[itemDataArr[i].itemName] = itemSprite[i];
        }
    }

    public void SaveData(PlayerData playerData) // PlayerTable�� �����͸� json���� ���� 
    {
        var jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);        
        File.WriteAllText(path, jsonData);        
    }

    public void SetStageData()
    {
        stageDataArr = JsonConvert.DeserializeObject<StageData[]>(stageDataFile.text);
        awakeStageDataArr = JsonConvert.DeserializeObject<AwakeStageData[]>(awakeStageDataFile.text);
    }

    public void GetStageData(int stageId)
    {
        currentStageData = stageDataArr[stageId];
        // �������������͸� ��ųʸ��� ���� �ƴ� �� ����Ʈ��
        // ���ξ����� �������� ������ Ŭ�� �� -> �� �������� �̹����� ��ư�� �ε����� ������ �ְ� �� �Լ��� ����
        // �׷� currentStageData�� �ش� �ε����� �������� �����͸� ����
        // ��Ʋ���� monsterSpawner���� currentStageData�� ������ ���� ��ȯ
        // playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json������ ������ȭ�� ����������
    }

}

