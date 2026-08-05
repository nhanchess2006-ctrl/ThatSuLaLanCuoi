using UnityEngine;
using System.Collections;

public class LeafSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    [Header("Leaf")]
    [SerializeField] private GameObject leafPrefab;

    [SerializeField] private float spawnInterval = 2f;

    Coroutine spawnRoutine;

    public void StartSpawn()
    {
        if (spawnRoutine == null)
            spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnLeaf();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnLeaf()
    {
        float randomX = Random.Range(
            leftPoint.position.x,
            rightPoint.position.x);

        Vector3 spawnPos = new Vector3(
            randomX,
            transform.position.y,
            0);

        Instantiate(leafPrefab, spawnPos, Quaternion.identity);
    }
}