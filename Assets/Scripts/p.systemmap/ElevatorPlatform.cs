using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 2f;

    private Vector3 startPoint;
    private Vector3 targetPoint;

    private bool isActivated = false;
    private bool movingToEnd = true;

    private void Start()
    {
        // Lưu vị trí ban đầu của platform
        startPoint = transform.position;

        // Ban đầu đi tới endPoint
        targetPoint = endPoint.position;
    }

    private void Update()
    {
        if (!isActivated)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            speed * Time.deltaTime
        );

        // Khi tới điểm đích thì đổi hướng
        if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
        {
            movingToEnd = !movingToEnd;

            targetPoint = movingToEnd
                ? endPoint.position
                : startPoint;
        }
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