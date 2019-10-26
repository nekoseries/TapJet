using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScrollerHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingStuff = new List<GameObject>();
    [SerializeField] private GameObject player;

    [SerializeField] private ScoreHandler scoreHandler;

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.pause) return;
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (!player) return;
        
        if ((!GameVariables.dragable && player.transform.position.y > 5) || (!GameVariables.dragable && player.transform.position.y < -5))
        {
            if (player.transform.position.y > 5) scoreHandler.AddMultiplier(1);
            else if (player.transform.position.y < -5)  scoreHandler.AddMultiplier(-1);
                
            foreach (GameObject objectGame in movingStuff)
            {
                objectGame.transform.parent = gameObject.transform;
            }
            
            gameObject.transform.position = new Vector2(0, -player.transform.position.y);
            
            foreach (GameObject objectGame in movingStuff)
            {
                objectGame.transform.parent = null;
            }
            
            gameObject.transform.position = Vector3.zero;

            
        }
    }
}
