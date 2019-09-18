using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollerManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private BGScroller lastBG;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BG"))
        {
            other.gameObject.transform.position = lastBG.GetNextPosition(); 
            lastBG = other.GetComponent<BGScroller>();
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
