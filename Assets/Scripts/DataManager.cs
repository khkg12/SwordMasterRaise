using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    [SerializeField] private TextAsset stageDataFile;
    [SerializeField] private TextAsset itemDataFile;
    string path;
    public PlayerData playerData;        
    public StageData[] stageDataArr;
    public StageData currentStageData;

    public ItemInfo[] itemDataArr;    
    public Dictionary<string, int> itemDic = new Dictionary<string, int>();
    public Dictionary<string, Sprite> itemSpriteDic = new Dictionary<string, Sprite>();    
    public Sprite[] itemSprite = new Sprite[16];
    

    new void Awake()
    {
        base.Awake();
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // ���� ��� ����
        SetData(); // json���� �ҷ��� playerData�� ���� �ʱ�ȭ
        SetStageData();
        SetItemData();
        currentStageData = stageDataArr[0]; // �����
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
            itemDic[itemDataArr[i].itemName] = itemDataArr[i].itemCount;
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

