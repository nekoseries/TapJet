using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


}