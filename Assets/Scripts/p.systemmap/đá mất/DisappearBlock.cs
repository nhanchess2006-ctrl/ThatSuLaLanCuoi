using UnityEngine;

public class DisappearBlock : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Hide()
    {
        sr.enabled = false;
        col.enabled = false;
    }

    public void Show()
    {
        sr.enabled = true;
        col.enabled = true;
    }
}