using System.Collections;
using UnityEngine;

public class DisappearPlatformGroup : MonoBehaviour
{
    [SerializeField] private float respawnTime = 5f;

    private DisappearBlock[] blocks;

    private bool isRunning = false;

    private void Awake()
    {
        blocks = GetComponentsInChildren<DisappearBlock>();
    }

    public void Activate()
    {
        if (isRunning) return;

        StartCoroutine(GroupRoutine());
    }

    IEnumerator GroupRoutine()
    {
        isRunning = true;

        foreach (DisappearBlock block in blocks)
        {
            block.Hide();
        }

        yield return new WaitForSeconds(respawnTime);

        foreach (DisappearBlock block in blocks)
        {
            block.Show();
        }

        isRunning = false;
    }
}