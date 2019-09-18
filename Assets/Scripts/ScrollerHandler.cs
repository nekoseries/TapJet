using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingStuff = new List<GameObject>();
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if ((!GameVariables.dragable && player.transform.position.y > 5) || (!GameVariables.dragable && player.transform.position.y < -5))
        {
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
