using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameVariables.dragable && other.CompareTag("Player"))
        {
            Rigidbody2D rbd = other.GetComponent<Rigidbody2D>();
            rbd.velocity = new Vector2(rbd.velocity.x * -1, rbd.velocity.y);
        }
    }
}
