using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public float speed = 5f;
    public float smoothTime = 0.1f;

    [SerializeField] private Rigidbody2D rbd;

    /*void Update()
    {
        rbd.velocity = new Vector2(0, -GameVariables.yVelocity);
    }*/
    
    void Update()
    {
        Vector3 velocity = Vector3.zero;
        /*if (Camera.main.WorldToScreenPoint(player.transform.position).y < 1 / 3f * Camera.main.pixelHeight || Camera.main.WorldToScreenPoint(player.transform.position).y > 2 / 3f * Camera.main.pixelHeight)
        {*/
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0f, player.transform.position.y, -10f), ref velocity, smoothTime);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, player.transform.position.y, -10f), speed * Time.deltaTime);
        /*}*/
        
        
    }
    
    
}
