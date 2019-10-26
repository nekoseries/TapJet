using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBound : MonoBehaviour
{
    [SerializeField] private BGScroller lastBG;
    [SerializeField] private bool topBound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (topBound && other.CompareTag("BG"))
        {
            other.gameObject.transform.position = lastBG.GetNextDownPosition(); 
            lastBG = other.GetComponent<BGScroller>();
        } else if (!topBound && other.CompareTag("BG"))
        {
            other.gameObject.transform.position = lastBG.GetNextTopPosition(); 
            lastBG = other.GetComponent<BGScroller>();
        }
    }
}
