using UnityEngine;
using System.Collections;
public class TrapTrigger : MonoBehaviour
{
    [SerializeField] LeafSpawner spawner;

    private bool activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        spawner.StartSpawn();
    }
}