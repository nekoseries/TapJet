using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    [SerializeField] private ScoreHandler score;
    [SerializeField] private int score2Portal;
    [SerializeField] private int score2Blackhole;

    [SerializeField] private Rigidbody2D wallRigidbody;

    [SerializeField] private GameObject wall;

    [SerializeField] private float speedScroll;

    private bool transition;

    private int lastOffset, offset;
    

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.pause) return;
        CheckingScore();
        if (transition) Transition();
    }

    void CheckingScore()
    {
        if (!transition)
        {
            lastOffset = offset;
            if (score.GetScore() > score2Blackhole)
            {
                offset = -20;
            }
            else if (score.GetScore() > score2Portal)
            {
                offset = -10;
            }
            else
            {
                offset = 0;
            }

            if (lastOffset != offset) transition = true;
        }
    }

    void Transition()
    {
        if (lastOffset > offset)
        {
            wallRigidbody.velocity = new Vector2(0, (-1 - GameVariables.yVelocity) * speedScroll);
            if (wall.transform.localPosition.y < offset)
            {
                wallRigidbody.velocity = Vector2.zero;
                transition = false;
                wall.transform.localPosition = new Vector2(0, offset);
            }
        }
        else
        {
            wallRigidbody.velocity = new Vector2(0, (1 + GameVariables.yVelocity) * speedScroll);
            if (wall.transform.localPosition.y > offset)
            {
                wallRigidbody.velocity = Vector2.zero;
                transition = false;
                wall.transform.localPosition = new Vector2(0, offset);
            }
        }
    }
}
