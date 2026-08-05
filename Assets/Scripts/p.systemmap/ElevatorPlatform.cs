using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 2f;

    private bool isActivated = false;

    void Update()
    {
        if (!isActivated)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            endPoint.position,
            speed * Time.deltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;

            // Player đi theo platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}