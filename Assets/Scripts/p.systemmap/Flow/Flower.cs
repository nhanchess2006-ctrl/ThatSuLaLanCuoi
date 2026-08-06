using UnityEngine;

public class Flower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        QuestManager.Instance.CollectFlower();

        Destroy(gameObject);
    }
}