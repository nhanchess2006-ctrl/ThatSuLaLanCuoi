using UnityEngine;

public class Flower : MonoBehaviour
{
    [Header("Unlock Condition")]
    [SerializeField] private bool requireBoss = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (requireBoss && !QuestManager.Instance.bossDefeated)
        {
            Debug.Log("Phải đánh bại Boss trước!");
            return;
        }

        QuestManager.Instance.CollectFlower();
        Destroy(gameObject);
    }
}