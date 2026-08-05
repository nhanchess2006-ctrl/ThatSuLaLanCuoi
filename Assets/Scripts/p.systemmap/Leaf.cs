using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Entity_Health health = other.GetComponent<Entity_Health>();

        if (health != null)
        {
            health.TakeDamage(damage , 0 , ElementType.None ,transform);
        }

        Destroy(gameObject);
    }
}