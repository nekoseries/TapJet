using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInGameManager : MonoBehaviour
{
    public GameObject pausePanel;
    
    public void Pause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        GameVariables.pause = pausePanel.activeSelf;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("HOME");
    }
}
