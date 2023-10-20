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
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // 저장 경로 설정
        DataSet(); // json에서 불러와 playerData에 저장 초기화
    }    
    
    public void DataSet()
    {
        // json이 없을 경우 초기화해주는 것 넣기
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json파일을 역직렬화로 데이터저장
    }

    public void DataSave(PlayerData playerData) // PlayerTable의 데이터를 json으로 저장 
    {
        var jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);        
        File.WriteAllText(path, jsonData);        
    }


}
