using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStage : MonoBehaviour
{
    [SerializeField] private Image Lobby;
    [SerializeField] private GameObject StageCanvas;
    const float MOVE_DISTANCE = 5;        


    public void ShowStageUI()
    {
        StartCoroutine(CameraMoveCo());
    }

    IEnumerator CameraMoveCo()
    {
        float dis = 0;
        Lobby.gameObject.SetActive(false);
        while(dis <= MOVE_DISTANCE)
        {
            dis += 0.1f;
            Debug.Log("asdasd");
            // Camera.main.transform.position += (Vector3.forward * 0.1f);
            Camera.main.transform.Translate(Vector3.forward * 0.1f);
            yield return null;
        }
        //StageCanvas.gameObject.SetActive(true);
    }    
}
