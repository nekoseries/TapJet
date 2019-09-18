using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private BGScrollerManager manager;
    [SerializeField] private GameObject nextPosition;
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
        /*rbd.velocity = new Vector2( 0, -speedScroll + -GameVariables.yVelocity);*/
        rbd.velocity = Vector2.down;
    }

    public Vector2 GetNextPosition()
    {
        return nextPosition.transform.position;
    }
}
