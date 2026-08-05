using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Move Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Move Speed")]
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}