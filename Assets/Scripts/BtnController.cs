using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.instance.IsGameStop = false;
        SceneManager.LoadScene(sceneName);
    }   
}
