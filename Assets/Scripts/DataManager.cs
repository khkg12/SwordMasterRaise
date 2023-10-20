using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;



public class DataManager : Singleton<DataManager>
{    
    [SerializeField] private TextAsset playerDataFile;
    public PlayerData playerData;
    string path;

    new void Awake()
    {
        base.Awake();
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // ���� ��� ����
        DataSet(); // json���� �ҷ��� playerData�� ���� �ʱ�ȭ
    }    
    
    public void DataSet()
    {
        // json�� ���� ��� �ʱ�ȭ���ִ� �� �ֱ�
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json������ ������ȭ�� ����������
    }

    public void DataSave(PlayerData playerData) // PlayerTable�� �����͸� json���� ���� 
    {
        var jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);        
        File.WriteAllText(path, jsonData);        
    }


}
