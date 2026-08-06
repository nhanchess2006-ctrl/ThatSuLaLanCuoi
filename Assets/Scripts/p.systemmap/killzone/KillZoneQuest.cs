using UnityEngine;

public class KillZoneQuest : MonoBehaviour
{
    [Header("Quest Condition")]
    [SerializeField] private int requiredFlower = 10;

    [Header("Kill")]
    [SerializeField] private float killDamage = 999999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Đủ số hoa -> cho đi qua
        if (QuestManager.Instance.currentFlower >= requiredFlower)
        {
            Debug.Log("Đủ hoa, được phép đi qua.");
            return;
        }

        // Chưa đủ hoa -> chết
        Debug.Log("Chưa thu thập đủ " + requiredFlower + " bông hoa!");

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