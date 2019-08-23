using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal anotherPortal;
    public bool teleport;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameVariables.dragable && other.CompareTag("Player") && !teleport)
        {
            anotherPortal.teleport = true;
            other.transform.position = new Vector2(anotherPortal.transform.position.x , other.transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            teleport = false;
        }
    }
}
