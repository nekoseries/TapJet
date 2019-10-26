using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
    public GameObject namePanel;
    public GameObject leaderboardPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            namePanel.SetActive(false);
        }
        else
        {
            namePanel.SetActive(true);
        }
    }

    public void ConfirmName(TMP_InputField input)
    {
        PlayerPrefs.SetString("name", input.text);
        PlayerPrefs.SetInt("key", Random.Range(0,1000));
        namePanel.SetActive(false);
    }

    public void Leaderboard()
    {
        //open connection
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        //close connection
        leaderboardPanel.SetActive(false);
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GAME");
    }
}
