using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowStage : MonoBehaviour
{
    [SerializeField] private Image Lobby;
    [SerializeField] private Canvas StageCanvas;
    [SerializeField] private TextMeshProUGUI btnText;
    private Button btn;
    const float MOVE_DISTANCE = 5;
    const float MOVE_SPEED = 0.1f;
    Vector3 moveVec = Vector3.forward;
    public bool IsChange
    {
        get => isChange;
        set
        {
            isChange = value;
            moveVec *= -1;
            if (isChange)
            {
                btnText.text = "���ư���";
            }                
            else
            {
                btnText.text = "�� ��";
            }                
        }
    }
    bool isChange = false;
    bool isStageShow = true;
    bool isLobbyShow = false;

    private void Start()
    {
        btn = GetComponent<Button>();
    }

    public void ShowStageUI()
    {
        StartCoroutine(CameraMoveCo()); // ��� ����ġ
    }

    IEnumerator CameraMoveCo()
    {        
        float dis = 0;
        btn.interactable = false;
        SelectActiveUI(IsChange); // IsChange�� ó���� false�̹Ƿ� 
        while(dis <= MOVE_DISTANCE)
        {
            dis += MOVE_SPEED;                 
            Camera.main.transform.Translate(moveVec * MOVE_SPEED);
            yield return null;
        }        
        SelectActiveUI(!IsChange);
        IsChange = !IsChange;
        btn.interactable = true;
    }    

    public void SelectActiveUI(bool isActive)
    {
        if (isActive)
        {
            StageCanvas.gameObject.SetActive(isStageShow);
            isStageShow = !isStageShow;
        }
        else
        {
            Lobby.gameObject.SetActive(isLobbyShow);
            isLobbyShow = !isLobbyShow;
        }            
    }
}
