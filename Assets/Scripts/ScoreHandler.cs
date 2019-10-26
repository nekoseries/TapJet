using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private int score;

    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int multiplier = 0;

    private void Update()
    {
        if (!player) return;

        score = Mathf.CeilToInt(player.transform.position.y) + multiplier;

        scoreText.text = ""+score;
    }

    public void AddMultiplier(int multiply)
    {
        multiplier += (5 * multiply);
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateDBScore()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tapjet-2019.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        IDictionary playerValue = GetUsers();

        if (playerValue != null)
        {
            if (playerValue.Contains(PlayerPrefs.GetInt("key")))
            {
                
            }
            else
            {
                AddUser();
            }
        }
        else
        {
            AddUser();
        }
    }

    void CheckScore()
    {
        
    }
    
    public void AddUser(){
        Player player = new Player(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("key"), score);
        string json = JsonUtility.ToJson(player);
        DatabaseReference dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("player").Push().SetRawJsonValueAsync(json);
    }

    public IDictionary GetUsers()
    {
        DatabaseReference dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("player").Child("score").OrderByValue();


        IDictionary dict = null;
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        
        dbInstance.GetReference("player").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                /*foreach (DataSnapshot player in snapshot.Children)
                {
                    dict = (IDictionary) player.Value;
                    //Debug.Log("" + playerValue["playerName"] + " - " + playerValue["key"]+ " - " + playerValue["score"]);
                }*/
                
                dict = (IDictionary) snapshot.Value;
            }
        });

        return dict;
    }
}

public class Player {


    public string playerName;
    public int key;
    public int score;

    public Player (string playerName, int key, int score) {
        this.playerName = playerName;
        this.key = key;
        this.score = score;
    }
}
