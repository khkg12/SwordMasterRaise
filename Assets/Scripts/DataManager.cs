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
        path = Path.Combine(Application.dataPath + "/Resources/", "playerData.json"); // 저장 경로 설정
        SetData(); // json에서 불러와 playerData에 저장 초기화
        SetStageData(); 
        currentStageData = stageDataArr[0]; // 실험용
    }    
    
    public void SetData()
    {
        // json이 없을 경우 초기화해주는 것 넣기
        playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json파일을 역직렬화로 데이터저장
    }

    public void SaveData(PlayerData playerData) // PlayerTable의 데이터를 json으로 저장 
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
        // 스테이지데이터를 딕셔너리로 저장 아님 걍 리스트로
        // 메인씬에서 스테이지 선택후 클릭 시 -> 그 스테이지 이미지나 버튼이 인덱스를 가지고 있고 이 함수를 실행
        // 그럼 currentStageData에 해당 인덱스의 스테이지 데이터를 저장
        // 배틀씬의 monsterSpawner에서 currentStageData의 정보를 토대로 소환
        // playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataFile.text); // json파일을 역직렬화로 데이터저장
    }

}
