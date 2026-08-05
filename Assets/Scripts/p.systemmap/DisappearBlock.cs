using System.Collections;
using UnityEngine;

public class DisappearBlock : MonoBehaviour
{
    [Header("Time")]
    public float disappearDelay = 0f;
    public float respawnTime = 5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D blockCollider;

    private bool isRunning = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blockCollider = GetComponent<Collider2D>();
    }

    public void StartDisappear()
    {
        if (!isRunning)
            StartCoroutine(DisappearRoutine());
    }

    IEnumerator DisappearRoutine()
    {
        isRunning = true;

        yield return new WaitForSeconds(disappearDelay);

        spriteRenderer.enabled = false;
        blockCollider.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        spriteRenderer.enabled = true;
        blockCollider.enabled = true;

        isRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartDisappear();
        }
    }
}