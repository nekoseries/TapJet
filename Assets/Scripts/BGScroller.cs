using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private BGScrollerManager manager;
    [SerializeField] private GameObject nextTopPosition;
    [SerializeField] private GameObject nextDownPosition;

    private float speedScroll;
    [SerializeField] private Rigidbody2D rbd;
    
    // Start is called before the first frame update
    void Start()
    {
        speedScroll = manager.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.pause) return;
        /*rbd.velocity = new Vector2( 0, -speedScroll + -GameVariables.yVelocity);*/
        rbd.velocity = new Vector2(0, -1  - GameVariables.yVelocity) * speedScroll;
    }

    public Vector2 GetNextTopPosition()
    {
        return nextTopPosition.transform.position;
    }
    
    public Vector2 GetNextDownPosition()
    {
        return nextDownPosition.transform.position;
    }
}
