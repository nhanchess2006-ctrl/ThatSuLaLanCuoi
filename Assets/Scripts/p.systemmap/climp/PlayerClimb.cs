using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed = 3f;

    public bool CanClimb { get; private set; }
    public bool IsClimbing { get; private set; }

    public Vine CurrentVine { get; private set; }

    public float ClimbSpeed => climbSpeed;


    public void EnterVine(Vine vine)
    {
        CanClimb = true;
        CurrentVine = vine;

        Debug.Log("Player đã vào Vine");
    }


    public void ExitVine(Vine vine)
    {
        // Tránh trường hợp có 2 Vine chồng Trigger lên nhau
        if (CurrentVine != vine)
            return;

        CanClimb = false;
        IsClimbing = false;
        CurrentVine = null;

        Debug.Log("Player đã rời Vine");
    }


    public void StartClimbing()
    {
        if (!CanClimb)
            return;

        IsClimbing = true;
    }


    public void StopClimbing()
    {
        IsClimbing = false;
    }
}