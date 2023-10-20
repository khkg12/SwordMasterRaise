using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    [SerializeField] private TextAsset stageDataFile;
    public PlayerData playerData;
    public StageData[] stageDataArr;
    public StageData currentStageData;
    string path;

    new void Awake()
    {
        base.Awake();
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // ���� ��� ����
        SetData(); // json���� �ҷ��� playerData�� ���� �ʱ�ȭ
        SetStageData(); 
        currentStageData = stageDataArr[0]; // �����
    }    
    
    public void SetData()
    {
        // json�� ���� ��� �ʱ�ȭ���ִ� �� �ֱ�
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json������ ������ȭ�� ����������
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
