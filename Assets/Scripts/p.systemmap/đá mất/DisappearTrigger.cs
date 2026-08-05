using UnityEngine;

public class DisappearTrigger : MonoBehaviour
{
    [SerializeField] private DisappearPlatformGroup group;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            group.Activate();
        }
    }
}