using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollerManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<BGScroller> list = new List<BGScroller>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BG") && other.gameObject == list[0].gameObject)
        {
            other.gameObject.transform.position = list[list.Count - 1].GetNextTopPosition(); 
            list.Add(other.GetComponent<BGScroller>());
            list.RemoveAt(0);
        } else if (other.CompareTag("BG") && other.gameObject == list[list.Count - 1].gameObject)
        {
            other.gameObject.transform.position = list[0].GetNextDownPosition(); 
            list.Insert(0, other.GetComponent<BGScroller>());
            list.RemoveAt(list.Count-1);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
