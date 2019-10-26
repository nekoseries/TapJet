using UnityEngine;

public class Blackhole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameVariables.dragable && other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
