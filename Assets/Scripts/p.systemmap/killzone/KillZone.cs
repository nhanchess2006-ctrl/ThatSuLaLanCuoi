using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private float killDamage = 999999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        Entity_Health health = other.GetComponent<Entity_Health>();

        if (health != null)
        {
            health.TakeDamage(
                killDamage,
                0,
                ElementType.None,
                transform
            );
        }
    }
}