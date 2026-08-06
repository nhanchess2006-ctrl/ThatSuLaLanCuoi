using UnityEngine;

public class Vine : MonoBehaviour
{
    [Header("Vine Points")]
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;

    public Transform TopPoint => topPoint;
    public Transform BottomPoint => bottomPoint;


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerClimb playerClimb = other.GetComponent<PlayerClimb>();

        if (playerClimb != null)
        {
            playerClimb.EnterVine(this);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerClimb playerClimb = other.GetComponent<PlayerClimb>();

        if (playerClimb != null)
        {
            playerClimb.ExitVine(this);
        }
    }
}